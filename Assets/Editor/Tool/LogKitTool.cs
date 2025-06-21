using System.Collections.Generic;
using System.IO;
using Buff;
using DefaultNamespace;
using LogKit;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor.Tool
{
    public class LogKitTool : OdinEditorWindow, IController
    {
        [MenuItem("安宁之地/工具/LogKit工具")]
        public static void Open()
        {
            GetWindow<LogKitTool>();
            GetWindow<LogKitTool>().minSize = new Vector2(600, 400);
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        protected override void Initialize()
        {
            base.Initialize();
            logConfig = this.GetModel<LogConfig>();
        }

        public LogConfig logConfig;

        [Button("保存")]
        public void Save()
        {
            logConfig.Save();
        }

    }
}