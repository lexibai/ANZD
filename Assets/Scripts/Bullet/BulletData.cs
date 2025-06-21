using System;
using System.Collections.Generic;
using Buff;
using UnityEngine;

namespace Bullet
{
    public class BulletData : ICloneable
    {
        /// <summary>
        /// 子弹图片资源路径
        /// </summary>
        public string spriteAss = QAssetBundle.Bulletsprite.CIRCLE;

        public Color color = Color.white;

        // 技能基础伤害
        public int baseDamage = 10;

        // 子弹移动速度
        public int moveSpeed = 40;

        /// <summary>
        /// 子弹存活时间
        /// </summary>
        public float lifeTime = 5f;

        /// <summary>
        /// 子弹是否可以追踪
        /// </summary>
        public bool canTracking = false;

        /// <summary>
        /// 子弹开始追踪的时间
        /// </summary>
        public float trackingStartTime = 0.5f;

        /// <summary>
        /// 子弹追踪时间
        /// </summary>
        public float trackingTime = 1f;

        /// <summary>
        /// 子弹已经瞄准的跟踪对象
        /// </summary>
        public GameObject selectTarget = null;

        /// <summary>
        /// 子弹追踪速度
        /// </summary>
        public float trackingSpeed = 10f;

        /// <summary>
        /// 子弹可攻击目标
        /// </summary>
        public List<string> targetTags = new List<string>() { "Enemy" };

        /// <summary>
        /// 要对附加对象添加的buff
        /// </summary>
        public Dictionary<BuffData, Type> addBuffs = new();


        /// <summary>
        /// 子弹可攻击次数
        /// </summary>
        public int hitNum = 1;

        /// <summary>
        /// 施加的力
        /// </summary>
        public float force = 0;


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}