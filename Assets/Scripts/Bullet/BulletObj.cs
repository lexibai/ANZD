using System;
using System.Collections;
using System.Collections.Generic;
using Actor;
using Combat.Command;
using DefaultNamespace;
using Model.Skill;
using QFramework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullet
{
    public class BulletObj:MonoBehaviour, IController
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
        
        
        
        private void Awake()
        {
            var spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
        }

        private void Start()
        {
            destroyRoutine = StartCoroutine(DestroyAfterDelay(bulletData.lifeTime));
            hitCollider = this.gameObject.GetComponent<Collider2D>();
            hitCollider.isTrigger = true;
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (Time.deltaTime * bulletData.moveSpeed));

            #region 追踪

            if (bulletData.canTracking && !bulletData.selectTarget && bulletData.trackingTime > 0)
            {
                bulletData.trackingTime -= Time.deltaTime;
                    
                // 定义三个方向：正前方、上方30度、下方30度
                Vector2 directionCenter = transform.right;
                Vector2 directionUp = Quaternion.AngleAxis(30, Vector3.forward) * directionCenter;
                Vector2 directionDown = Quaternion.AngleAxis(-30, Vector3.forward) * directionCenter;

                // 射线检测范围
                var raycastHit2DsCenter = Physics2D.RaycastAll(transform.position, directionCenter, 20f);
                var raycastHit2DsUp = Physics2D.RaycastAll(transform.position, directionUp, 20f);
                var raycastHit2DsDown = Physics2D.RaycastAll(transform.position, directionDown, 20f);

                // 合并所有碰撞结果
                List<RaycastHit2D> raycastHit2Ds = new List<RaycastHit2D>();
                raycastHit2Ds.AddRange(raycastHit2DsCenter);
                raycastHit2Ds.AddRange(raycastHit2DsUp);
                raycastHit2Ds.AddRange(raycastHit2DsDown);

                // 可视化调试
                // 绘制原始射线方向（未命中时也可见）
                Debug.DrawRay(transform.position, directionCenter * 20f, Color.blue);   // 正前方蓝色射线
                Debug.DrawRay(transform.position, directionUp * 20f, Color.green);      // 上偏移绿色射线
                Debug.DrawRay(transform.position, directionDown * 20f, Color.yellow);   // 下偏移黄色射线
                foreach (var hit in raycastHit2Ds)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red, 0.1f);
                    foreach (var bulletDataTargetTag in bulletData.targetTags)
                    {
                        if (hit.transform.gameObject.CompareTag(bulletDataTargetTag))
                        {
                            bulletData.selectTarget = hit.transform.gameObject;
                        }
                    }
                }
            }

            if (bulletData.selectTarget && bulletData.trackingTime > 0)
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
        
        private void OnTriggerStay2D(Collider2D other)
        {
            foreach (var bulletDataTargetTag in bulletData.targetTags)
            {
                if (other.CompareTag(bulletDataTargetTag))
                {
                    Destroy(this.gameObject);

                    var enemyObj = other.gameObject.GetComponent<EnemyObj>();
                    //伤害敌人
                    this.SendCommand<DamageCommand>(
                        new DamageCommand(attacker, other.gameObject.GetComponent<ActorObj>(), skill, this)
                        );
                    return;
                }
            }

           
        }
        
        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (destroyRoutine != null)
            {
                StopCoroutine(destroyRoutine);
            }
        }
        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
        
        
    }
}