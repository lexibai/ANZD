using System.ComponentModel;
using Model.Skill;
using Sirenix.OdinInspector;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件器添加对象
    /// </summary>
    public class AddSkillConditionInfo
    {
        [GUIColor("#dd33cc")]
        [LabelText("条件器")]
        [ValueDropdown("@Const.ConstGet.GetAllClassName<DefSkillCondition>()")]
        public string className;

    }
}