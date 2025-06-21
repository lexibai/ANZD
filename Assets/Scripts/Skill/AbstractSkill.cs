using Actor;
using DefaultNamespace;
using QFramework;

namespace Model.Skill
{
    public abstract class AbstractSkill : IController, Skill
    {
        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public SkillData skillData { get; set; } = new SkillData();
        public float nowCd { get; set; }
        public abstract void UseSkill(ActorObj userObj);
        public virtual void Init(SkillData skillData)
        {
            this.skillData = skillData;
            nowCd = 0;
        }

        /// <summary>
        /// 是否可以使用技能, 特化的判断, 即便返回true, 也要根据战斗管理器得出的最终结果确定
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public virtual bool CanUse(ActorObj userObj)
        {

            return true;
        }
    }
}