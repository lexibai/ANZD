using System;
using System.Collections;
using Actor;
using Buff.Command;
using Combat.Command;
using DefaultNamespace;
using LogTool;
using Model.Skill;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace Bullet
{
    public class BulletObj : MonoBehaviour, IController
    {
        /// <summary>
        /// 命中碰撞
        /// </summary>
        public Collider2D hitCollider;

        /// <summary>
        /// 来源技能, 可为空
        /// </summary>
        public Skill skill;

        /// <summary>
        /// 来源攻击者, 可为空
        /// </summary>
        public ActorObj attacker;

        /// <summary>
        /// 子弹数据
        /// </summary>
        public BulletData bulletData = new BulletData();

        /// <summary>
        /// 来源对象池
        /// </summary>
        [NonSerialized]
        public SimpleObjectPool<GameObject> soPool;

        /// <summary>
        /// 管理延时回收的协程，由子弹工厂回收时停止
        /// </summary>
        public Coroutine coroutine = null;

        private void Awake()
        {
            var spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
        }

        private void OnEnable()
        {
            coroutine = StartCoroutine(DelayRecycle());

            hitCollider = this.gameObject.GetComponent<Collider2D>();
            if (hitCollider != null)
            {
                hitCollider.isTrigger = true;
            }
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (Time.deltaTime * bulletData.moveSpeed));

            #region 追踪
            if (bulletData.canTracking)
            {
                // 追踪开始时间和追踪时间计时
                TrackTimer();

                if (!(bulletData.trackingTime > 0 && bulletData.trackingStartTime < 0))
                {
                    return;
                }

                // 检测一定范围内的敌人选择为追踪对象
                if (bulletData.selectTarget == null)
                {
                    //圆形检测
                    var overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 25f);


                    // 检索敌人
                    foreach (var hit in overlapCircleAll)
                    {
                        if (IsEnemyTag(hit))
                        {
                            bulletData.selectTarget = hit.transform.gameObject;
                            break;
                        }
                    }
                }
                else
                {
                    TrackRotation();
                }
            }
            #endregion
        }

        // 追踪计时
        public void TrackTimer()
        {
            bulletData.trackingStartTime -= Time.deltaTime;
            if (bulletData.trackingStartTime < 0)
            {
                bulletData.trackingTime -= Time.deltaTime;
            }
        }

        /// <summary>
        /// 追踪旋转
        /// </summary>
        public void TrackRotation()
        {
            // 控制子弹移动
            Vector2 targetPos = bulletData.selectTarget.transform.position;
            Vector2 dir = targetPos - (Vector2)transform.position;

            // 计算目标旋转角度（让 X 轴指向目标）
            Quaternion targetRotation = Quaternion.FromToRotation(Vector2.right, dir);

            // 使用 Slerp 做平滑插值旋转（可调 speed 控制速度）
            float trackingSpeed = bulletData.trackingSpeed; // 你可以定义一个 trackingSpeed 字段控制旋转速度
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, trackingSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 进入碰撞事件
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsEnemyTag(other))
            {
                HandleEnemyEnter(other);
            }

        }

        /// <summary>
        /// 判断是否有敌人的tag
        /// </summary>
        /// <param name="other">敌人的碰撞体</param>
        /// <returns>是否有tag</returns>
        private bool IsEnemyTag(Collider2D other)
        {
            foreach (var bulletDataTargetTag in bulletData.targetTags)
            {
                if (other.CompareTag(bulletDataTargetTag))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 处理命中的敌人
        /// </summary>
        /// <param name="other"></param>
        private void HandleEnemyEnter(Collider2D other)
        {
            if (--bulletData.hitNum <= 0)
            {
                soPool.Recycle(gameObject);
            }

            var enemyObj = other.gameObject.GetComponent<EnemyObj>();

            // 对敌人施加力
            Rigidbody2D enemyRb = enemyObj.GetComponent<Rigidbody2D>();
            enemyRb.AddForce(transform.right * bulletData.force, ForceMode2D.Impulse);

            // 添加buff
            foreach (var buffItem in bulletData.addBuffs)
            {
                this.SendCommand<AddBuffCommand>(new AddBuffCommand(enemyObj, buffItem.Item1, buffItem.Item2));
            }

            // 伤害敌人
            this.SendCommand<DamageCommand>(
                new DamageCommand(attacker, other.gameObject.GetComponent<ActorObj>(), skill, this)
            );
        }

        public IEnumerator DelayRecycle()
        {
            yield return new WaitForSeconds(bulletData.lifeTime);
            soPool.Recycle(gameObject);
        }


        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }


    }
}