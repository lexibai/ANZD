using System;
using Actor;
using UnityEngine;

namespace Buff
{
    public class BaseBuffObj
    {
        /// <summary>
        /// buff数据
        /// </summary>
        public BuffData buffData;

        /// <summary>
        /// buff存活时间
        /// </summary>
        public float nowTime = 0;

        /// <summary>
        /// buff持续时间
        /// </summary>
        public float nowDuration = 0;

        /// <summary>
        /// buff心跳时间
        /// </summary>
        public float nowTickTime = 0;

        /// <summary>
        /// buff心跳次数
        /// </summary>
        public int tickNum = 0;

        /// <summary>
        /// 当前层数
        /// </summary>
        public int nowCount = 0;

        /// <summary>
        /// 被附加buff的actor
        /// </summary>
        public ActorObj ao;

        private void TickTemp()
        {
            if (buffData.tickTime > 0)
            {
                nowTickTime += Time.deltaTime;
                if (nowTickTime >= buffData.tickTime)
                {
                    nowTickTime = 0;
                    Tick();
                    tickNum++;
                }
            }
        }

        private void RemoveBuffTemp()
        {
            nowTime += Time.deltaTime;
            if (!buffData.canPermanent)
            {
                nowDuration += Time.deltaTime;
                if (nowDuration >= buffData.duration)
                {
                    RemoveBuff();

                }
            }
        }

        /// <summary>
        /// 持续运行buff
        /// </summary>
        public virtual void run()
        {
            TickTemp();
            RemoveBuffTemp();
        }

        #region 子类重写

        protected virtual void Tick()
        {
        }

        protected virtual void RemoveBuff()
        {
            ao.buffs.Remove(this);
        }

        protected virtual void Additional()
        {
        }

        #endregion


        #region 外部回调

        /// <summary>
        /// 调用添加附加效果
        /// </summary>
        public void TriggerAddBuff()
        {
            nowCount = Math.Clamp(nowCount + 1, 0, buffData.maxCount);
            if (buffData.resetDuration)
            {
                nowDuration = Math.Min(0, nowDuration);
            }
            if (buffData.addDuration)
            {
                nowDuration -= buffData.duration;
            }
            Additional();
        }

        /// <summary>
        /// 外部触发心跳
        /// </summary>
        public void TriggerTick()
        {
            Tick();
        }

        /// <summary>
        /// 调用移除附加效果
        /// </summary>
        public void TriggerRemoveBuff()
        {
            RemoveBuff();
        }
        #endregion

    }
}