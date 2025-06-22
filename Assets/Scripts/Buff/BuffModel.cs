using System;
using System.Collections.Generic;
using QFramework;
using Tool;

namespace Buff
{
    [Serializable]
    public class BuffModel : AbstractModel
    {
        public List<BuffData> data = new List<BuffData>();

        protected override void OnInit()
        {
            data = this.GetUtility<IStorageUtility>().Load<BuffModel>(nameof(BuffModel))?.data;
        }

        public void Save()
        {
            this.GetUtility<IStorageUtility>().Save<BuffModel>(this, nameof(BuffModel), true);
        }

        /// <summary>
        /// 根据buffLiteralQuantity获取buffData
        /// </summary>
        /// <param name="buffLiteralQuantity"></param>
        /// <returns></returns>
        public BuffData SelectBuffData(string buffLiteralQuantity)
        {
            return data.Find(x => x.buffLiteralQuantity == buffLiteralQuantity);
        }
    }
}