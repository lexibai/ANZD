using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bullet
{
    /// <summary>
    /// 子弹数据配置，用于生成子弹数据
    /// </summary>
    public class BulletConfigData
    {

        /// <summary>
        /// 子弹名称
        /// </summary>
        [GUIColor("#00ff00")]
        [LabelText("子弹名称"),HorizontalGroup("title", 0.3f)]
        public string name;

        /// <summary>
        /// 子弹基础伤害
        /// </summary>
        [HorizontalGroup("MainGroup", Width = 0.33f)]
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据", showLabel: true)]
        [LabelText("基础伤害")]
        public int baseDamage = 10;

        /// <summary>
        /// 子弹移动速度
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("移动速度")]
        public int moveSpeed = 40;

        /// <summary>
        /// 子弹存活时间
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("子弹存在时长")]
        public float lifeTime = 5f;

        /// <summary>
        /// 子弹是否可以追踪
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("开启追踪")]
        public bool canTracking = false;

        /// <summary>
        /// 子弹开始追踪的时间
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("追踪开始时间")]
        public float trackingStartTime = 0.5f;

        /// <summary>
        /// 子弹追踪时间
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("追踪时长")]
        public float trackingTime = 1f;

        /// <summary>
        /// 子弹追踪速度
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("追踪旋转速度")]
        public float trackingSpeed = 10f;

        /// <summary>
        /// 子弹可攻击次数
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("子弹可击中次数")]
        public int hitNum = 1;

        /// <summary>
        /// 施加的力
        /// </summary>
        [VerticalGroup("MainGroup/Left")]
        [BoxGroup("MainGroup/Left/基础数据")]
        [LabelText("命中后施加力")]
        public float force = 0;

        /// <summary>
        /// 子弹图片资源路径
        /// </summary>
        [HorizontalGroup("MainGroup", Width = 0.33f)]
        [VerticalGroup("MainGroup/Center")]
        [BoxGroup("MainGroup/Center/视觉效果", showLabel: true)]
        [LabelText("精灵资源")]
        [ValueDropdown("@Const.ConstGet.GetAllConstValues<Bulletsprite>()")]
        public string spriteAss = QAssetBundle.Bulletsprite.CIRCLE;

        /// <summary>
        /// 子弹颜色
        /// </summary>
        [OdinSerialize]
        [HorizontalGroup("MainGroup", Width = 0.33f)]
        [VerticalGroup("MainGroup/Center")]
        [BoxGroup("MainGroup/Center/视觉效果", showLabel: true)]
        [LabelText("颜色")]
        public Color color = Color.white;


        [HorizontalGroup("MainGroup", Width = 0.33f)]
        [VerticalGroup("MainGroup/Right")]
        [BoxGroup("MainGroup/Right/添加buff", showLabel: true)]
        [LabelText("添加buff")]
        public List<BulletAddBuffConfigInfo> addBuffs = new List<BulletAddBuffConfigInfo>();

        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// 通过此配置生成对应的子弹数据
        /// </summary>
        public BulletData bulletData
        {
            get
            {
                BulletData bulletData = new BulletData();
                bulletData.name = name;
                bulletData.baseDamage = baseDamage;
                bulletData.moveSpeed = moveSpeed;
                bulletData.lifeTime = lifeTime;
                bulletData.canTracking = canTracking;
                bulletData.trackingStartTime = trackingStartTime;
                bulletData.trackingTime = trackingTime;
                bulletData.trackingSpeed = trackingSpeed;
                bulletData.hitNum = hitNum;
                bulletData.force = force;
                bulletData.spriteAss = spriteAss;
                bulletData.color = color;
                return bulletData;
            }
        }

    }
}