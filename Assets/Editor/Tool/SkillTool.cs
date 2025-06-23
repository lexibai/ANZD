using System.Collections.Generic;
using System.IO;
using Buff;
using DefaultNamespace;
using Model.Skill;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine.Serialization;

namespace Editor.Tool
{
    public class SkillTool:OdinEditorWindow, IController
    {
        [MenuItem("安宁之地/工具/技能工具")]
        public static void Open()
        {
            GetWindow<SkillTool>().Show();
            
        }
        
        [TableList]
        public List<SkillConfigData> skillConfigDatas = new List<SkillConfigData>();

        [ButtonGroup("操作"), Button("加载")]
        public void Load()
        {
            skillConfigDatas = this.GetModel<SkillModel>().configDatas;
        }

        [ButtonGroup("操作"), Button("保存")]
        public void Save()
        {
            this.GetModel<SkillModel>().configDatas = skillConfigDatas;
            this.GetModel<SkillModel>().Save();
        }
        
        [ButtonGroup("操作"), Button("生成常量")]
        public void Generate()
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"namespace Const");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {nameof(SkillModel)}Assets");
            sb.AppendLine("    {");
            foreach (var skillData in skillConfigDatas)
            {
                sb.AppendLine($"        public const string {skillData.literalQuantity} = \"{skillData.literalQuantity}\";");
            }
            sb.AppendLine("    }");
            sb.AppendLine("}");
            File.WriteAllText($"Assets/Scripts/Const/{nameof(SkillModel)}Assets.cs", sb.ToString());
            AssetDatabase.Refresh(); // 刷新资源数据库使新脚本生效
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}