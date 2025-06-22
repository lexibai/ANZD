using System.Collections.Generic;
using System.IO;
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

        [HorizontalGroup("数据", order:2), TableList]
        public List<BulletConfigData> configData;

        protected override void Initialize()
        {
            base.Initialize();
            configData = this.GetModel<BulletModel>().configData;
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        [HorizontalGroup("操作", order:1),Button("保存")]
        public void Save()
        {
            XLog.Instance.info("保存子弹数据");
            BulletModel bulletModel = this.GetModel<BulletModel>();
            bulletModel.configData = configData;
            bulletModel.Save();
        }

        [HorizontalGroup("操作", order:1),Button("生成常量")]
        public void Generate()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"namespace Const");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {nameof(BulletModel)}Assets");
            sb.AppendLine("    {");
            foreach (var item in configData)
            {
                sb.AppendLine($"        public const string {item.name} = \"{item.name}\";");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");
            File.WriteAllText($"Assets/Scripts/Const/{nameof(BulletModel)}Assets.cs", sb.ToString());
            AssetDatabase.Refresh(); // 刷新资源数据库使新脚本生效
        }
    }
}