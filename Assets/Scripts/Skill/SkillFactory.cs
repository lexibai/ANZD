using System;
using System.Collections.Generic;
using Const;
using Model.Skill.Impl;
using Model.Skill.Impl.Move;
using QFramework;

namespace Model.Skill
{
    public class SkillFactory:AbstractSystem
    {
        public readonly Dictionary<string, Type> skillMapping = new Dictionary<string, Type>()
        {
            {SkillModelAssets.普通奥术射击, typeof(FireSkill)},
            {SkillModelAssets.加速移动, typeof(SpeedUpSkill)},
        };
        
        public Skill CreateSkill(string skillLiteralQuantity)
        {
            if (skillMapping.ContainsKey(skillLiteralQuantity))
            {
                var skill = (Skill)Activator.CreateInstance(skillMapping[skillLiteralQuantity]);
                var skillData = this.GetModel<SkillModel>().data.Find(x => x.literalQuantity == skillLiteralQuantity);
                skill.Init(skillData);
                return skill;
            }
            return null;
        }

        protected override void OnInit()
        {
            
        }
    }
}