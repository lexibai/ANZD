using Actor;
using DefaultNamespace;
using QFramework;

namespace Model.Skill
{
    public abstract class AbstractSkill:IController, Skill
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
        public abstract void UseSkill(ActorObj userObj);
        public virtual void Init(SkillData skillData)
        {
            this.skillData = skillData;
        }
    }
}