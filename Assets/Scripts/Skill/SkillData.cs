using System;

namespace Model.Skill
{
    public class SkillData:ICloneable
    {
        //基础伤害
        public int baseDamage = 10;
        
        
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}