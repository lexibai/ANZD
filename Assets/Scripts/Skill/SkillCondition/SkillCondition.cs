using Model.Skill;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件接口
    /// </summary>
    public interface SkillCondition
    {
        bool Check(Skill skill, bool prevCheck);
    }
}