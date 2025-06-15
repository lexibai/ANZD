using System;
using Actor;
using QFramework;

namespace Model.Skill
{
    public interface Skill:ICloneable
    {
        public SkillData skillData { get; set; }
        
        /// <summary>
        /// 当前的cd
        /// </summary>
        public float nowCd { get; set; }
        
        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="userObj"></param>
        void UseSkill(ActorObj userObj);

        /// <summary>
        /// 检查技能是否可用
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public bool CanUse(ActorObj userObj);
        
        void Init(SkillData skillData);
        
        
    }
}