using System;
using System.Collections.Generic;
using LogTool;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Tool;
using UnityEngine;

namespace Bullet
{
    [Serializable]
    public class BulletModel : AbstractModel
    {
        [NonSerialized]
        public List<BulletData> data = null;
        
        public List<BulletConfigData> configData = null;
        protected override void OnInit()
        {
            BulletModel bulletModel = this.GetUtility<IStorageUtility>().Load<BulletModel>(nameof(BulletModel));
            configData = bulletModel.configData;
            XLog.Instance.debug($"子弹模型加载完成： {this}");
        }

        public void Save()
        {
            this.GetUtility<IStorageUtility>().Save<BulletModel>(this, nameof(BulletModel), true);
            XLog.Instance.debug($"子弹模型保存完成： {this}");
        }
    }
}