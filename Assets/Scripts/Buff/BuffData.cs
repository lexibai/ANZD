using System;
using System.Collections.Generic;
using Actor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace.Buff
{
    
    [Serializable]
    public class BuffData: ICloneable
    {
        public int id;

        [LabelText("名称")]
        public string name;
        
        /// <summary>
        /// 优先级
        /// </summary>
        [LabelText("优先级")]
        public int priority;
        
        /// <summary>
        /// 持续时间
        /// </summary>
        [LabelText("持续时间")]
        public float duration;

        /// <summary>
        /// 心跳间隔
        /// </summary>
        [LabelText("心跳间隔")]
        public float tickTime;
        
        /// <summary>
        /// 是否可永久存在
        /// </summary>
        [LabelText("永久存在")]
        public bool canPermanent;

        /// <summary>
        /// 新增是否重置时间
        /// </summary>
        [LabelText("重置时间")]
        public bool resetDuration;

        /// <summary>
        /// 新增是否增加时间
        /// </summary>
        [LabelText("增加时间")]
        public bool addDuration;
        
        /// <summary>
        /// 最大层数
        /// </summary>
        [LabelText("最大层数")]
        public int maxCount;
        
        /// <summary>
        /// 标签
        /// </summary>
        [LabelText("标签")]
        public List<string> tags;
        
        /// <summary>
        /// 添加的属性
        /// </summary>
        [LabelText("添加的属性")]
        public ActorBaseData[] addAttrs = new ActorBaseData[2];

        public object Clone()
        {
            var memberwiseClone = (BuffData)this.MemberwiseClone();
            memberwiseClone.tags = new List<string>(tags);
            memberwiseClone.addAttrs  = new ActorBaseData[2];
            for (int i = 0; i < addAttrs.Length; i++)
            {
                memberwiseClone.addAttrs[i] = (ActorBaseData)addAttrs[i].Clone();
            }
            return memberwiseClone;
        }
    }
}