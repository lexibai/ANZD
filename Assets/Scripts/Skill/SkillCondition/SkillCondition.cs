using Actor;
using Model.Skill;
using UnityEngine.InputSystem;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件接口
    /// </summary>
    public interface SkillCondition
    {
        bool Check(ActorObj userObj, Skill skill, bool prevCheck);
    }
}