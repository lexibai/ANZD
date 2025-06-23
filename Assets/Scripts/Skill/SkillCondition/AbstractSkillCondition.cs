namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件抽象实现
    /// </summary>
    public abstract class AbstractSkillCondition : ISkillCondition
    {
        public virtual bool Check(bool prevCheck)
        {
            return true;
        }
    }
}