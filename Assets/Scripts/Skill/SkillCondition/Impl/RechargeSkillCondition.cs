using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Model.Skill;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

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
        public override bool Check(Skill skill, bool prevCheck)
        {
            base.Check(skill, prevCheck);
            nowRechargeTime += Time.deltaTime;
            if (nowRechargeTime >= RechargeTime)
            {
                nowRechargeTime = 0;
                return true;
            }
            return false;
        }

    
    }
}