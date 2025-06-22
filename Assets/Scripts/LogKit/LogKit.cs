using DefaultNamespace;
using QFramework;
using UnityEngine;

namespace LogTool
{
    public class XLog : Singleton<XLog>, ICanGetModel
    {
        private XLog()
        {

        }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            logConfig = logConfig ?? this.GetModel<LogConfig>();
        }
        LogConfig logConfig = null;
        public void error(string logInfo)
        {
            if (!CanUseLogLevel(LogLevelType.ERROR))
            {
                return;
            }
            logInfo = handleLogInfo(logInfo, LogLevelType.ERROR,"#FF0000");
            //打印堆栈日志
            Debug.LogError(logInfo + "\n" + System.Environment.StackTrace);
        }

        public void warn(string logInfo)
        {
             if (!CanUseLogLevel(LogLevelType.WARN))
            {
                return;
            }
            logInfo = handleLogInfo(logInfo, LogLevelType.WARN, "#FFA500");
            //打印堆栈日志
            Debug.LogWarning(logInfo + "\n" + System.Environment.StackTrace);
        }

        public void info(string logInfo)
        {
             if (!CanUseLogLevel(LogLevelType.INFO))
            {
                return;
            }
            logInfo = handleLogInfo(logInfo, LogLevelType.INFO, "#00FF00");
            //打印堆栈日志
            Debug.Log(logInfo);
        }

        public void debug(string logInfo)
        {
             if (!CanUseLogLevel(LogLevelType.DEBUG))
            {
                return;
            }
            logInfo = handleLogInfo(logInfo, LogLevelType.DEBUG,"#66CCFF");
            //打印堆栈日志
            Debug.Log(logInfo + "\n" + System.Environment.StackTrace);
        }


        public string handleLogInfo(string logInfo, LogLevelType level, string color = "#FFFFFF")
        {
            logConfig = logConfig ?? this.GetModel<LogConfig>();
            if (logConfig.showLogTime)
            {
                string formattedTime = System.DateTime.Now.ToString("yyyy年-MM月-dd日 HH:mm:ss:fff");
                logInfo ="<b><color=#FFFFFF>" + formattedTime + "</color></b> " + logInfo;
            }

            if (logConfig.showLogLevel)
            {
                logInfo = " <b>[" + level.ToString() + "] </b>" + logInfo;
            }
            logInfo = $"<color={color}>{logInfo}</color>";
            return logInfo;
        }

        public bool CanUseLogLevel(LogLevelType level)
        {
            logConfig = logConfig ?? this.GetModel<LogConfig>();
            return logConfig.level >= level;
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}