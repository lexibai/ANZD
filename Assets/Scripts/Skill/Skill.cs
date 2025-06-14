using System;
using Actor;
using QFramework;

namespace Model.Skill
{
    public interface Skill:ICloneable
    {
        public SkillData skillData { get; set; }
        public void UseSkill(ActorObj userObj);
        
    }
}