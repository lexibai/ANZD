using Actor;
using Model.Skill;
using QFramework;
using UnityEngine;

namespace Bullet
{
    public class BulletFactory:Singleton<BulletFactory>
    {
        private GameObject bulletPrefab;
        private BulletFactory()
        {
            bulletPrefab = Resources.Load<GameObject>("bullet");
        }
        
        public BulletObj CreateBullet(BulletData data, ActorObj attacker=null, Skill skill=null)
        { 
            var but = Object.Instantiate(bulletPrefab);
            but.transform.position = ReferenceEquals(attacker, null) ? Vector3.zero : attacker.GetFireTransform().position;
            but.AddComponent<CircleCollider2D>();
            var bulletObj = but.GetComponent<BulletObj>();
            bulletObj.skill = skill?.Clone() as Skill;
            bulletObj.attacker = attacker;
            bulletObj.bulletData = data.Clone() as BulletData;
            return bulletObj;
        }
    }
}