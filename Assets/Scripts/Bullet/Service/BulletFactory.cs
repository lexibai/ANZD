﻿using Actor;
using Model.Skill;
using QFramework;
using UnityEngine;

namespace Bullet
{
    /// <summary>
    /// 子弹工厂：
    /// - 可以生产子弹数据
    /// - 可以生产子弹实体
    /// </summary>
    public class BulletFactory : Singleton<BulletFactory>
    {
        private GameObject bulletPrefab;
        private SimpleObjectPool<GameObject> bulletPool;
        private ResLoader rl;

        private BulletFactory()
        {
            rl = ResLoader.Allocate();
            bulletPrefab = rl.LoadSync<GameObject>(QAssetBundle.Bullet_prefab.BULLET);
            //bulletPrefab = Resources.Load<GameObject>("bullet");
            bulletPool = new SimpleObjectPool<GameObject>(() =>
            {
                var bullet = Object.Instantiate(bulletPrefab);
                bullet.SetActive(false);
                return bullet;
            }, o =>
            {
                //移除碰撞组件
                Object.Destroy(o.GetComponent<Collider2D>());

                //重置变换
                o.transform.Identity();
                var bulletObj = o.GetComponent<BulletObj>();
                bulletObj.StopCoroutine(bulletObj.coroutine);

                

                o.SetActive(false);
            }, 50);
        }

        /// <summary>
        /// 创建子弹
        /// </summary>
        /// <param name="data">子弹数据</param>
        /// <param name="attacker">发射者，可为空</param>
        /// <param name="skill"></param>
        /// <returns></returns>
        public BulletObj CreateBullet(BulletData data, ActorObj attacker = null, Skill skill = null)
        {
            var but = bulletPool.Allocate();

            // 子弹运行时类设置
            var bulletObj = but.GetComponent<BulletObj>();
            bulletObj.skill = skill?.Clone() as Skill;
            bulletObj.attacker = attacker;
            bulletObj.bulletData = data.Clone() as BulletData;
            bulletObj.soPool = bulletPool;
            // 子弹实体设置, 必须先设置精灵图片再设置碰撞器，否则碰撞会使用上次的精灵碰撞
            but.transform.position = attacker?.GetFireTransform()?.position ?? Vector3.zero;
            var sprite = but.GetComponent<SpriteRenderer>();
            sprite.sprite = rl.LoadSync<Sprite>(data.spriteAss);
            sprite.color = data.color;
            but.AddComponent<PolygonCollider2D>();
            but.SetActive(true);
            return bulletObj;
        }

        public void RecycleBullet(BulletObj bullet)
        {
            bulletPool.Recycle(bullet.gameObject);
        }

        public override void Dispose()
        {
            base.Dispose();
            rl.Recycle2Cache();
            rl = null;
        }
    }
}