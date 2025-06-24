using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using SkillModule.Condition;

namespace Model.Skill
{
    
    public class SkillData : ICloneable
    {
        public int id;

        /// <summary>
        /// 技能名称
        /// </summary>
        [LabelText("技能名称")]
        public string name;

        /// <summary>
        /// 技能绑定的按键
        /// </summary>
        [LabelText("绑定按键")]
        public string bindKey;

        /// <summary>
        /// 技能字面量
        /// </summary>
        [LabelText("技能字面量")]
        public string literalQuantity;

        /// <summary>
        /// 技能冷却时间
        /// </summary>
        [LabelText("技能冷却时间")]
        public float cd;

        /// <summary>
        /// 基础伤害
        /// </summary>
        [LabelText("基础伤害")]
        public int baseDamage = 10;

        /// <summary>
        /// 技能条件器
        /// </summary>
        public List<SkillCondition> skillConditions;

        /// <summary>
        /// 黑板数据
        /// </summary>
        [LabelText("技能黑板数据")]
        public Dictionary<string, object> blackboard = new();



        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}