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

            ResetBulletData();
        }

        public void Save()
        {
            this.GetUtility<IStorageUtility>().Save<BulletModel>(this, nameof(BulletModel), true);
            XLog.Instance.debug($"子弹模型保存完成： {this}");
            ResetBulletData();
        }
        
        /// <summary>
        /// 根据配置重新生成子弹数据
        /// </summary>
        private void ResetBulletData()
        {
            data = new List<BulletData>();
            foreach (var cfgItem in configData)
            {
                BulletData bulletData = cfgItem.bulletData;
                data.Add(bulletData);
                XLog.Instance.debug($"加载子弹： {JsonUtility.ToJson(bulletData)}");
            }
        }
    }
}