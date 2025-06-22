using Actor;
using Buff;
using Bullet;
using Const;
using QFramework;
using UnityEngine;

namespace Model.Skill.Impl.Ultimate
{
    public class UltimateSkill : AbstractSkill
    {
        public override void UseSkill(ActorObj userObj)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var item in gameObjects)
            {
                // 获取子弹大小，子弹位置等基本信息
                ActorObj actorObj = item.GetComponent<ActorObj>();
                BaseBuffObj baseBuffObj = actorObj.SearchBuffObj(BuffAssets.炎烬标记);
                if (baseBuffObj == null)
                {
                    continue;
                }
                int nowCount = baseBuffObj.nowCount;
                Vector3 position = item.transform.position;

                // 移除炎烬标记
                baseBuffObj.TriggerRemoveBuff();

                // 生成子弹
                BulletData bulletData = this.SendQuery(new BulletDataQuery(BulletModelAssets.原地爆炸子弹));
                BulletObj bulletObj = BulletFactory.Instance.CreateBullet(bulletData, userObj, this);
                bulletObj.gameObject.transform.localScale = Vector3.one * nowCount * 5f;
                bulletObj.gameObject.transform.position = position;

            }
        }
    }
}