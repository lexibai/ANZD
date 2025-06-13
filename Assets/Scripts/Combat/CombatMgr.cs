using System;
using Actor;
using Bullet;
using Model.Skill;
using QFramework;
using UnityEngine;

namespace Combat
{
    public class CombatMgr: AbstractSystem
    {
        protected override void OnInit()
        {
            
        }

        /// <summary>
        /// 计算伤害
        /// </summary>
        /// <param name="attacker">攻击者, 可为null</param>
        /// <param name="target">被攻击者</param>
        /// <param name="skill">技能, 可以为null</param>
        /// <param name="bullet">子弹, 可为null</param>
        public void CalculateDamage(ActorObj attacker, ActorObj target, Skill skill, BulletObj bullet)
        {
            // 伤害公式:
            //    基础伤害 = 攻击者攻击力 - 被攻击者防御力 
            //    最终伤害 = 基础伤害 + 技能固定伤害 + 子弹固定伤害

            // 计算伤害
            float baseDamage = 0;
            if (!ReferenceEquals(attacker, null))
            {
                baseDamage = attacker.actorData.attack - target.actorData.defense;
            }
            if (!ReferenceEquals(skill, null))
            {
                baseDamage += skill.skillData.baseDamage;
            }
            if (!ReferenceEquals(bullet, null))
            {
                baseDamage += bullet.bulletData.baseDamage;
            }
            // 伤害不能小于1
            baseDamage = Math.Clamp(baseDamage, 1, baseDamage);

            // 造成伤害并触发回调
            target.actorData.nowHp -= (int)baseDamage;
            target.OnTakeDamage(attacker, skill, bullet);
            if (!ReferenceEquals(attacker, null))
            {
                attacker.OnHit(target);
            }

            if (target.actorData.nowHp <= 0)
            {
                target.OnDeath();
            }
        }
    }
}