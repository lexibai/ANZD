using System;
using System.Resources;
using Bullet;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Model.Skill.Impl
{
    public class fireSkill : Skill
    {
        public SkillData skillData { get; set; } = new SkillData();

        public void UseSkill(PlayerObj playerObj)
        {
            ActionKit.Sequence()
                .Delay(0.1f)
                .Callback(() =>
                {
                    //这里实例化子弹
                    var gameObject = Resources.Load<GameObject>("bullet");
                    var but = Object.Instantiate(gameObject, playerObj.GetFireTransform().position, Quaternion.identity);
                    but.AddComponent<CircleCollider2D>();
                    but.GetComponent<BulletObj>().skill = this.Clone() as Skill;
                    but.GetComponent<BulletObj>().attacker = playerObj;
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
                .Start(playerObj);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}