using System.Collections.Generic;
using System.IO;
using Buff;
using DefaultNamespace;
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
        
        [TableList]
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
        
        [ButtonGroup("操作"), Button("生成常量")]
        public void Generate()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("namespace Buff");
            sb.AppendLine("{");
            sb.AppendLine("    public class BuffAssets");
            sb.AppendLine("    {");
            foreach (var buffData in buffDatas)
            {
                sb.AppendLine($"        public const string {buffData.buffLiteralQuantity} = \"{buffData.buffLiteralQuantity}\";");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");
            File.WriteAllText("Assets/Scripts/Buff/BuffAssets.cs", sb.ToString());
            AssetDatabase.Refresh(); // 刷新资源数据库使新脚本生效
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}