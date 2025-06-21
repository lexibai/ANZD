using Actor;
using Bullet;
using Model.Skill;
using QFramework;
using UnityEngine;

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
            Debug.Log($"攻击命令被触发: {attacker.name} 对 {target.name}.\n    技能：{skill?.skillData?.name}\n    子弹：{bullet?.bulletData?.color}");
            var combatMgr = this.GetSystem<CombatMgr>();
            combatMgr.CalculateDamage(attacker, target, skill, bullet);
        }
    }
}