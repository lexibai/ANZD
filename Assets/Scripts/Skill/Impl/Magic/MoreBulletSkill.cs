﻿using Actor;
using Bullet;
using Const;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Model.Skill.Impl.Magic
{
    public class MoreBulletSkill : AbstractSkill
    {
        public override void UseSkill(ActorObj userObj)
        {
            BulletData bulletData = this.SendQuery(new BulletDataQuery(BulletModelAssets.八方飞弹));
            for (int i = 0; i < 8; i++)
            {
                //实例化八颗
                var but = BulletFactory.Instance.CreateBullet(bulletData, userObj, this);
                var positionValue = Mouse.current.position.value;
                var screenToWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(positionValue.x, positionValue.y, 0));
                screenToWorldPoint.z = 0;
                var toWorldPoint = screenToWorldPoint - but.transform.position;
                //计算角度
                var angle = Mathf.Atan2(toWorldPoint.y, toWorldPoint.x) * Mathf.Rad2Deg + ((i - 1) * 45);
                //but旋转
                but.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

        }
    }
}