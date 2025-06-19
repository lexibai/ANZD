using System;
using System.Collections.Generic;
using Actor;
using Bullet;
using Model.Skill;
using QFramework;
using UnityEngine;

namespace Combat
{
    public class CombatTimer : MonoSingleton<CombatTimer>
    {
        public float deltaTime;
        
        public Action OnUpdate;
        
        public void Update()
        {
            deltaTime = Time.deltaTime;
            OnUpdate.Invoke();
        }
    }
    
    public class CombatMgr: AbstractSystem
    {
        
        private readonly List<Skill> skillsOnCd = new List<Skill>();
        
        protected override void OnInit()
        {
            CombatTimer.Instance.OnUpdate += () =>
            {
                foreach (var skill in skillsOnCd.ToArray())
                {
                    skill.nowCd  -= CombatTimer.Instance.deltaTime;
                    if (skill.nowCd <= 0)
                    {
                        skillsOnCd.Remove(skill);
                    }
                }
            };
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
            target.originalActorData.nowHp -= (int)baseDamage;
            target.OnTakeDamage(attacker, skill, bullet);
            if (!ReferenceEquals(attacker, null))
            {
                target.actorState.isHit = true;
                ActionKit.Delay(0.3f, () =>
                {
                    target.actorState.isHit = false;
                }).Start(target);
                attacker.OnHit(target);
            }

            if (target.originalActorData.nowHp <= 0)
            {
                target.OnDeath();
            }
        }
        
        /// <summary>
        /// 判断技能是否可用, 泛化的逻辑, 与技能独立的判断与运算
        /// </summary>
        /// <param name="userObj"></param>
        /// <param name="skill"></param>
        public bool CheckSkillCanUse(ActorObj userObj, Skill skill)
        {
            if (skill.nowCd > 0)
            {
                return false;
            }
            return skill.CanUse(userObj);
        }

        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="userObj"></param>
        /// <param name="skill"></param>
        public void UseSkill(ActorObj userObj, Skill skill)
        {
            skill.nowCd = skill.skillData.cd;
            skillsOnCd.Add(skill);
            skill.UseSkill(userObj);
        }
    }
}