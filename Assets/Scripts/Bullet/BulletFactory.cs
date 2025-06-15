using Actor;
using Model.Skill;
using QFramework;
using UnityEngine;

namespace Bullet
{
    public class BulletFactory:Singleton<BulletFactory>
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
                o.SetActive(false);
            }, 50);
        }
        
        public BulletObj CreateBullet(BulletData data, ActorObj attacker=null, Skill skill=null)
        { 
            //var but = Object.Instantiate(bulletPrefab);
            var but = bulletPool.Allocate();
            but.transform.position = ReferenceEquals(attacker, null) ? Vector3.zero : attacker.GetFireTransform().position;
            but.AddComponent<CircleCollider2D>();
            var bulletObj = but.GetComponent<BulletObj>();
            bulletObj.skill = skill?.Clone() as Skill;
            bulletObj.attacker = attacker;
            bulletObj.bulletData = data.Clone() as BulletData;
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