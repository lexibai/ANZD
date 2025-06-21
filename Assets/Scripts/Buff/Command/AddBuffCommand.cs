using System;
using Actor;
using QFramework;

namespace Buff.Command
{

    public class AddBuffCommand : AbstractCommand
    {
        public ActorObj ao;
        public BuffData buffData;
        public Type type;

        public AddBuffCommand(ActorObj ao, BuffData buffData, Type type)
        {
            this.ao = ao;
            this.buffData = buffData;
            this.type = type;
        }

        protected override void OnExecute()
        {
            //检查ao是否已经包含此buff
            BaseBuffObj buffObj = AddBuffTool.CheckBuff(ao, buffData);

            if (ReferenceEquals(buffObj, null))
            {
                //如果不包含, 添加此buff
                buffObj = AddBuffTool.AddBuff(ao, buffData, type);
                buffObj.TriggerAddBuff();
            }
            else
            {
                //如果包含, 触发新增事件
                buffObj.TriggerAddBuff();
            }

        }

    }


    public class AddBuffCommand<T> : AbstractCommand where T : BaseBuffObj, new()
    {
        public ActorObj ao;
        public BuffData buffData;

        public AddBuffCommand(ActorObj ao, BuffData buffData)
        {
            this.ao = ao;
            this.buffData = buffData;
        }

        protected override void OnExecute()
        {
            //检查ao是否已经包含此buff
            BaseBuffObj buffObj = AddBuffTool.CheckBuff(ao, buffData);

            if (ReferenceEquals(buffObj, null))
            {
                //如果不包含, 添加此buff
                buffObj = AddBuffTool.AddBuff<T>(ao, buffData);
                buffObj.TriggerAddBuff();
            }
            else
            {
                //如果包含, 触发新增事件
                buffObj.TriggerAddBuff();
            }

        }



    }

    public class AddBuffTool
    {
        /// <summary>
        /// 检查actorObj是否已经包含此buff
        /// </summary>
        /// <param name="actorObj"></param>
        /// <param name="buffData1"></param>
        /// <returns></returns>
        public static BaseBuffObj CheckBuff(ActorObj actorObj, BuffData buffData1)
        {
            foreach (var actorObjBuff in actorObj.buffs)
            {
                if (actorObjBuff.buffData.id == buffData1.id)
                {
                    return actorObjBuff;
                }
            }
            return null;
        }



        #region 添加buff
        public static BaseBuffObj AddBuff(ActorObj actorObj, BuffData buffData1, Type type)
        {
            BaseBuffObj buffObj = (BaseBuffObj)Activator.CreateInstance(type);
            buffObj.buffData = buffData1;
            buffObj.ao = actorObj;
            actorObj.buffs.Add(buffObj);

            actorObj.buffs.Sort((x, y)
                => x.buffData.priority.CompareTo(y.buffData.priority));

            return buffObj;
        }


        public static TBaseBuffObj AddBuff<TBaseBuffObj>(ActorObj actorObj, BuffData buffData1) where TBaseBuffObj : BaseBuffObj, new()
        {
            TBaseBuffObj buffObj = new TBaseBuffObj();
            buffObj.buffData = buffData1;
            buffObj.ao = actorObj;
            actorObj.buffs.Add(buffObj);

            actorObj.buffs.Sort((x, y)
                => x.buffData.priority.CompareTo(y.buffData.priority));

            return buffObj;
        }
        #endregion

    }
}