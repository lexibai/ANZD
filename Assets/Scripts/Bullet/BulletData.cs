using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletData:ICloneable
    {
        // 技能基础伤害
        public int baseDamage = 10;
        
        // 子弹移动速度
        public int moveSpeed = 30;

        /// <summary>
        /// 子弹存活时间
        /// </summary>
        public float lifeTime=5f;

        /// <summary>
        /// 子弹是否可以追踪
        /// </summary>
        public bool canTracking = false;

        /// <summary>
        /// 子弹追踪时间
        /// </summary>
        public float trackingTime = 1f;
        
        /// <summary>
        /// 子弹已经瞄准的跟踪对象
        /// </summary>
        public GameObject selectTarget=null;
        
        /// <summary>
        /// 子弹可攻击目标
        /// </summary>
        public List<string> targetTags = new List<string>(){"Enemy"};

        public float trackingSpeed = 2f;


        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}