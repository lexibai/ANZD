using System;

namespace Model.Skill
{
    public interface Skill:ICloneable
    {
        public SkillData skillData { get; set; }
        public void UseSkill(PlayerObj playerObj);
    }
}