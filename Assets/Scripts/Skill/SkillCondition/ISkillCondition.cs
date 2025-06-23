namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件接口
    /// </summary>
    public interface ISkillCondition
    {
        bool Check(bool prevCheck);
    }
}