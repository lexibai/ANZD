using System;
using Sirenix.OdinInspector;

namespace Model.Skill
{
    [Serializable]
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
        [ValueDropdown("@Const.ConstGet.GetAllPublicFieldNames<PlayerAction.DefMapsActions>()")]
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


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}