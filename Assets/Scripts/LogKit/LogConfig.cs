using System;
using QFramework;
using Tool;

namespace LogTool
{
    [Serializable]
    public class LogConfig : AbstractModel
    {
        /// <summary>
        /// 是否展示日志等级
        /// </summary>
        public bool showLogLevel = true;

        /// <summary>
        /// 是否展示日志时间
        /// </summary>
        public bool showLogTime = true;

        /// <summary>
        /// 日志等级，会打印此等级以上的所有日志
        /// </summary>
        public LogLevelType level = LogLevelType.INFO;

        protected override void OnInit()
        {
            LogConfig logConfig = this.GetUtility<BinaryStorageUtility>().Load<LogConfig>(nameof(LogConfig));
            if (logConfig == null)
            {
                logConfig = new();
                Save();
            }
            showLogLevel = logConfig.showLogLevel;
            showLogTime = logConfig.showLogTime;
            level = logConfig.level;

        }

        public void Save()
        {
            this.GetUtility<BinaryStorageUtility>().Save<LogConfig>(this, nameof(LogConfig), true);
        }
    }
}