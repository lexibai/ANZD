using System;
using Bullet;
using DefaultNamespace;
using Model.Skill;
using QFramework;
using UnityEngine;

namespace Actor
{
    [Serializable]
    public abstract class ActorObj : MonoBehaviour, IController
    {
        //角色数据
        public ActorData actorData = new ActorData();
        
        //角色状态
        public ActorState actorState = new ActorState();
        
        /// <summary>
        /// 被击中后触发
        /// </summary>
        /// <param name="attacker">攻击者, 可以为空</param>
        /// <param name="skill">技能</param>
        /// <param name="bullet">子弹</param>
        public abstract void OnTakeDamage(ActorObj attacker, Skill skill, BulletObj bullet);

        /// <summary>
        /// 命中敌人触发
        /// </summary>
        /// <param name="target"></param>
        public abstract void OnHit(ActorObj target);
        
        /// <summary>
        /// 死亡触发
        /// </summary>
        public abstract void OnDeath();

        /// <summary>
        /// 恢复生命调用
        /// </summary>
        public abstract void OnHeal(int hp);
        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}