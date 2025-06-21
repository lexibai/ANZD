using System.Collections.Generic;
using Actor;
using Buff;
using Buff.BuffList;
using Bullet;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Model.Skill.Impl.MartialSkill
{
    public class ShockSkill : AbstractSkill
    {
        public override void UseSkill(ActorObj userObj)
        {
            ActionKit.Sequence()
               .Delay(0.1f)
               .Callback(() =>
               {
                   //实例化子弹
                   var but = BulletFactory.Instance.CreateBullet(new BulletData()
                   {
                       color = Color.red,
                       spriteAss = QAssetBundle.Bulletsprite.ATLAS_50_刀气,
                       hitNum = 9999,
                       moveSpeed = 100,
                       force = 10f,
                       addBuffs = new Dictionary<BuffData, System.Type>()
                       {
                        {this.GetModel<BuffModel>().SelectBuffData(BuffAssets.炎烬标记) , typeof(MarkBuff)}
                       }
                   }, userObj, this);
                   var positionValue = Mouse.current.position.value;
                   var screenToWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(positionValue.x, positionValue.y, 0));
                   screenToWorldPoint.z = 0;
                   var toWorldPoint = screenToWorldPoint - but.transform.position;
                   //计算角度
                   var angle = Mathf.Atan2(toWorldPoint.y, toWorldPoint.x) * Mathf.Rad2Deg;
                   //but旋转
                   but.transform.rotation = Quaternion.Euler(0, 0, angle);
               }).Delay(3f).Callback(() =>
               {
               })
               .Start(userObj);
        }
    }
}