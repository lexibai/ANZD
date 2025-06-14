using System;
using Actor;
using QFramework;

namespace Model.Skill
{
    public interface Skill:ICloneable
    {
        public SkillData skillData { get; set; }
        
        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="userObj"></param>
        void UseSkill(ActorObj userObj);

        void Init(SkillData skillData);
    }
}