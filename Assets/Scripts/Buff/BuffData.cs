using System;
using System.Collections.Generic;
using Actor;
using Sirenix.OdinInspector;

namespace Buff
{

    [Serializable]
    public class BuffData : ICloneable
    {
        [VerticalGroup("基本属性")]
        public int id;

        [VerticalGroup("基本属性")]
        [LabelText("名称")]
        public string name;

        /// <summary>
        /// buff字面量, 要求buff中唯一
        /// </summary>
        [VerticalGroup("基本属性")]
        [LabelText("字面量")]
        public string buffLiteralQuantity;

        /// <summary>
        /// 优先级
        /// </summary>
        [VerticalGroup("基本属性")]
        [LabelText("优先级")]
        public int priority;

        /// <summary>
        /// 持续时间
        /// </summary>
        [VerticalGroup("基本属性")]
        [LabelText("持续时间")]
        public float duration;

        /// <summary>
        /// 心跳间隔
        /// </summary>
        [VerticalGroup("基本属性")]
        [LabelText("心跳间隔")]
        public float tickTime;

        /// <summary>
        /// 是否可永久存在
        /// </summary>
        [VerticalGroup("选项")]
        [LabelText("永久存在")]
        public bool canPermanent;

        /// <summary>
        /// 新增是否重置时间
        /// </summary>
        [VerticalGroup("选项")]
        [LabelText("重置时间")]
        public bool resetDuration;

        /// <summary>
        /// 新增是否增加时间
        /// </summary>
        [VerticalGroup("选项")]
        [LabelText("增加时间")]
        public bool addDuration;

        /// <summary>
        /// 最大层数
        /// </summary>
        [VerticalGroup("基本属性")]
        [LabelText("最大层数")]
        public int maxCount;

        /// <summary>
        /// 标签
        /// </summary>
        [VerticalGroup("标签列表")]
        [LabelText("标签")]
        public List<string> tags;

        /// <summary>
        /// 添加的属性
        /// </summary>
        [VerticalGroup("属性")]
        [LabelText("添加的属性")]
        public ActorBaseData[] addAttrs = new ActorBaseData[2];

        public object Clone()
        {
            var memberwiseClone = (BuffData)this.MemberwiseClone();
            memberwiseClone.tags = new List<string>(tags);
            memberwiseClone.addAttrs = new ActorBaseData[2];
            for (int i = 0; i < addAttrs.Length; i++)
            {
                memberwiseClone.addAttrs[i] = (ActorBaseData)addAttrs[i].Clone();
            }
            return memberwiseClone;
        }
    }
}