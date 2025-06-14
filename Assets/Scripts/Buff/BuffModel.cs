using System;
using System.Collections.Generic;
using QFramework;
using Tool;

namespace DefaultNamespace.Buff
{
    [Serializable]
    public class BuffModel:AbstractModel
    {
        public List<BuffData> data = new List<BuffData>();
        
        protected override void OnInit()
        {
            data = this.GetUtility<BinaryStorageUtility>().Load<BuffModel>(nameof(BuffModel)).data;
        }

        public void Save()
        {
            this.GetUtility<BinaryStorageUtility>().Save<BuffModel>(this, nameof(BuffModel), true);
        }
    }
}