using System;
using System.Collections;
using Actor;
using Buff.Command;
using Combat.Command;
using DefaultNamespace;
using Model.Skill;
using QFramework;
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
        /// 销毁协程
        /// </summary>
        private Coroutine destroyRoutine;

        /// <summary>
        /// 子弹数据
        /// </summary>
        public BulletData bulletData = new BulletData();

        /// <summary>
        /// 来源对象池
        /// </summary>
        [NonSerialized]
        public SimpleObjectPool<GameObject> soPool;

        private void Awake()
        {
            var spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
        }


        private void Start()
        {
        }

        private void OnEnable()
        {
            destroyRoutine = StartCoroutine(DestroyAfterDelay(bulletData.lifeTime));
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
                bulletData.trackingStartTime -= Time.deltaTime;
                if (bulletData.trackingStartTime < 0)
                {
                    bulletData.trackingTime -= Time.deltaTime;
                }
            }

            if (bulletData.canTracking && !bulletData.selectTarget && bulletData.trackingTime > 0 && bulletData.trackingStartTime < 0)
            {
                //圆形检测
                var overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, 25f);


                // 可视化调试
                // 绘制检测圆球（未命中时也可见）
                // 绘制圆形检测范围
                foreach (var hit in overlapCircleAll)
                {
                    foreach (var bulletDataTargetTag in bulletData.targetTags)
                    {
                        if (hit.transform.gameObject.CompareTag(bulletDataTargetTag))
                        {
                            bulletData.selectTarget = hit.transform.gameObject;
                            break;
                        }
                    }
                }
            }

            if (bulletData.selectTarget && bulletData.trackingTime > 0 && bulletData.trackingStartTime < 0)
            {
                Vector2 targetPos = bulletData.selectTarget.transform.position;
                Vector2 dir = targetPos - (Vector2)transform.position;

                // 计算目标旋转角度（让 X 轴指向目标）
                Quaternion targetRotation = Quaternion.FromToRotation(Vector2.right, dir);

                // 使用 Slerp 做平滑插值旋转（可调 speed 控制速度）
                float trackingSpeed = bulletData.trackingSpeed; // 你可以定义一个 trackingSpeed 字段控制旋转速度
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, trackingSpeed * Time.deltaTime);
            }



            #endregion
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsEnemyTag(other))
            {
                HandleEnemyEnter(other);
            }

        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (gameObject != null)
            {
                soPool.Recycle(gameObject);
            }
        }

        private void OnDisable()
        {
            if (destroyRoutine != null)
            {
                StopCoroutine(destroyRoutine);
            }
        }

        private void OnDestroy()
        {

        }
        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

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
                this.SendCommand<AddBuffCommand>(new AddBuffCommand(enemyObj, buffItem.Key, buffItem.Value));
            }

            // 伤害敌人
            this.SendCommand<DamageCommand>(
                new DamageCommand(attacker, other.gameObject.GetComponent<ActorObj>(), skill, this)
            );
        }




    }
}