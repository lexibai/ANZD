using System;
using System.Collections.Generic;
using LogTool;
using QFramework;
using Tool;

namespace Model.Skill
{
    [Serializable]
    public class SkillModel : AbstractModel
    {

        public List<SkillConfigData> configDatas = new List<SkillConfigData>();

        [NonSerialized]
        public List<SkillData> data = new List<SkillData>();


        protected override void OnInit()
        {
            this.configDatas = this.GetUtility<IStorageUtility>().Load<SkillModel>(nameof(SkillModel)).configDatas;
            foreach (var item in configDatas)
            {
                XLog.Instance.debug($"{item.name}技能加载中...");
                SkillData skillData = item.GetSkillData();
                XLog.Instance.debug($"{skillData.name}技能加载完成！");
                data.Add(skillData);
            }
            XLog.Instance.debug($"技能加载完成！");
        }

        public void Save()
        {
            this.GetUtility<IStorageUtility>().Save<SkillModel>(this, nameof(SkillModel), true);
            XLog.Instance.debug($"技能保存完成！");
        }
    }
}