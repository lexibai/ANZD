using Actor;
using Model.Skill;
using QFramework;

namespace Combat.Command
{
    /// <summary>
    /// 使用技能命令
    /// </summary>
    public class UseSkillCommand : AbstractCommand<bool>
    {
        private readonly ActorObj userObj;
        private readonly Skill skill;

        public UseSkillCommand(ActorObj userObj, Skill skill)
        {
            this.userObj = userObj;
            this.skill = skill;
        }

        protected override bool OnExecute()
        {
            var combatMgr = this.GetSystem<CombatMgr>();
            if (combatMgr.CheckSkillCanUse(userObj, skill))
            {
                combatMgr.UseSkill(userObj, skill);
                return true;
            }
            return false;
        }
    }
}