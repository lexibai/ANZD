using System.Collections.Generic;
using Bullet;
using DefaultNamespace;
using LogTool;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.ShaderKeywordFilter;

namespace Editor.Tool
{
    public class BulletTool : OdinEditorWindow, IController
    {

        [MenuItem("安宁之地/工具/子弹工具")]
        public static void Open()
        {
            GetWindow<BulletTool>();

        }

        [TableList]
        public List<BulletData> data;

        protected override void Initialize()
        {
            base.Initialize();
            data = this.GetModel<BulletModel>().data;
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        [Button("保存")]
        public void Save()
        {
            XLog.Instance.info("保存子弹数据");
            BulletModel bulletModel = this.GetModel<BulletModel>();
            bulletModel.data = data;
            bulletModel.Save();
        }
    }
}