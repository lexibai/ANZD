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
        [OdinSerialize]
        public List<BulletData> data = null;
        protected override void OnInit()
        {
            BulletModel bulletModel = this.GetUtility<BinaryStorageUtility>().Load<BulletModel>(nameof(BulletModel));
            data = bulletModel.data;
            XLog.Instance.debug($"子弹模型加载完成： {this}");
        }

        public void Save()
        {
            this.GetUtility<BinaryStorageUtility>().Save<BulletModel>(this, nameof(BulletModel));
            XLog.Instance.debug($"子弹模型保存完成： {this}");
        }
    }
}