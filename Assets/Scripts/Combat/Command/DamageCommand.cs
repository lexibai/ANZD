using Actor;
using Bullet;
using Model.Skill;
using QFramework;

namespace Combat.Command
{
    public class DamageCommand:AbstractCommand
    {
        public ActorObj attacker;
        public ActorObj target;
        public Skill skill;
        public BulletObj bullet;
        
        public DamageCommand(ActorObj attacker, ActorObj target, Skill skill, BulletObj bullet)
        {
            this.attacker = attacker;
            this.target = target;
            this.skill = skill;
            this.bullet = bullet;
        }
        
        protected override void OnExecute()
        {
            var combatMgr = this.GetSystem<CombatMgr>();
            combatMgr.CalculateDamage(attacker, target, skill, bullet);
        }
    }
}