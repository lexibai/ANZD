using System.Collections.Generic;
using System.IO;
using Buff;
using DefaultNamespace;
using LogTool;
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
            this.GetModel<LogConfig>().Save();
        }

        [Button("打印错误日志")]
        public void TestLogError()
        {
            XLog.Instance.error("这是一条测试用错误日志");
        }

        [Button("打印警告")]
        public void TestLogWarn()
        {
            XLog.Instance.warn("这是一条测试用警告日志");
        }


        [Button("打印信息日志")]
        public void TestLogInfo()
        {
            XLog.Instance.info("这是一条测试用消息日志");
        }


        [Button("打印调试日志")]
        public void TestLogDebug()
        {
            XLog.Instance.debug("这是一条测试用调试日志");
        }


    }
}