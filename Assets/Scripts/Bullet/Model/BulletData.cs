using System;
using System.Collections.Generic;
using Buff;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bullet
{
    [Serializable]
    public class BulletData : ICloneable
    {
        /// <summary>
        /// 子弹名称
        /// </summary>
        [VerticalGroup("基础数据")]
        [LabelText("子弹名称")]
        public string name;

        /// <summary>
        /// 子弹基础伤害
        /// </summary>
        [VerticalGroup("基础数据")]
        [LabelText("基础伤害")]
        public int baseDamage = 10;

        /// <summary>
        /// 子弹移动速度
        /// </summary>
        [VerticalGroup("基础数据")]
        [LabelText("移动速度")]
        public int moveSpeed = 40;

        /// <summary>
        /// 子弹存活时间
        /// </summary>
        [VerticalGroup("基础数据")]
        public float lifeTime = 5f;

        /// <summary>
        /// 子弹是否可以追踪
        /// </summary>
        [VerticalGroup("基础数据")]
        public bool canTracking = false;

        /// <summary>
        /// 子弹开始追踪的时间
        /// </summary>
        [VerticalGroup("基础数据")]
        public float trackingStartTime = 0.5f;

        /// <summary>
        /// 子弹追踪时间
        /// </summary>
        [VerticalGroup("基础数据")]
        public float trackingTime = 1f;

        /// <summary>
        /// 子弹已经瞄准的跟踪对象
        /// </summary>
        [NonSerialized]
        public GameObject selectTarget = null;

        /// <summary>
        /// 子弹追踪速度
        /// </summary>
        [VerticalGroup("基础数据")]
        public float trackingSpeed = 10f;

        /// <summary>
        /// 子弹可攻击目标
        /// </summary>
        public List<string> targetTags = new List<string>() { "Enemy" };

        /// <summary>
        /// 要对附加对象添加的buff
        /// </summary>
        [OdinSerialize]
        public List<Tuple<BuffData, Type>> addBuffs = new();


        /// <summary>
        /// 子弹可攻击次数
        /// </summary>
        [VerticalGroup("基础数据")]
        public int hitNum = 1;

        /// <summary>
        /// 施加的力
        /// </summary>
        [VerticalGroup("基础数据")]
        public float force = 0;

        /// <summary>
        /// 子弹图片资源路径
        /// </summary>
        [VerticalGroup("视觉效果")]
        [LabelText("精灵资源")]
        [ValueDropdown("@Const.ConstGet.GetAllQAssetValues()")]
        public string spriteAss = QAssetBundle.Bulletsprite.CIRCLE;

        /// <summary>
        /// 子弹颜色
        /// </summary>
        [OdinSerialize]
        [VerticalGroup("视觉效果")]
        [LabelText("颜色")]
        public Color color = Color.white;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}