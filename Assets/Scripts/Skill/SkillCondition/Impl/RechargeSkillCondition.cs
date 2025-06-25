using System.ComponentModel;
using Actor;
using LogTool;
using Model.Skill;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件抽象实现
    /// </summary>
    [DisplayName("充能条件器")]
    public class RechargeSkillCondition : DefSkillCondition
    {
        [LabelText("充能时间")]
        public float RechargeTime = 3;

        private float nowRechargeTime = 0;
        public override bool Check(ActorObj userObj, Skill skill, bool prevCheck)
        {
            base.Check(userObj, skill, prevCheck);
            nowRechargeTime += Time.deltaTime;
            XLog.Instance.debug($"蓄力进度{nowRechargeTime}");
            if (nowRechargeTime >= RechargeTime)
            {
                nowRechargeTime = 0;
                return true;
            }
            return false;
        }

    
    }
}