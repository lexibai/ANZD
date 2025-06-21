using System;
using System.Collections.Generic;
using QFramework;
using Tool;

namespace Model.Skill
{
    [Serializable]
    public class SkillModel : AbstractModel
    {
        public List<SkillData> data = new List<SkillData>();

        protected override void OnInit()
        {
            this.data = this.GetUtility<BinaryStorageUtility>().Load<SkillModel>(nameof(SkillModel)).data;
        }

        public void Save()
        {
            this.GetUtility<BinaryStorageUtility>().Save<SkillModel>(this, nameof(SkillModel), true);
        }
    }
}