using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Buff;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Editor.Tool
{
    public class BuffTool:OdinEditorWindow, IController
    {
        [MenuItem("安宁之地/工具/buff工具")]
        public static void Open()
        {
            GetWindow<BuffTool>().Show();
            
        }
        
        public List<BuffData> buffDatas = new List<BuffData>();

        [ButtonGroup("操作"), Button("加载")]
        public void Load()
        {
            buffDatas = this.GetModel<BuffModel>().data;
        }

        [ButtonGroup("操作"), Button("保存")]
        public void Save()
        {
            this.GetModel<BuffModel>().data = buffDatas;
            this.GetModel<BuffModel>().Save();
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}