using System.ComponentModel;
using Model.Skill;
using Sirenix.OdinInspector;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件抽象实现
    /// </summary>
    [DisplayName("默认条件器")]
    public class DefSkillCondition : SkillCondition
    {
        [LabelText("默认数据")]
        public int defData = 10;
        public virtual bool Check(Skill skill, bool prevCheck)
        {
            return true;
        }
    }
}