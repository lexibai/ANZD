using System;
using System.Collections.Generic;
using Const;
using DefaultNamespace;
using Model.Skill.Impl;
using Model.Skill.Impl.Attack;
using Model.Skill.Impl.Magic;
using Model.Skill.Impl.Move;
using QFramework;

namespace Model.Skill
{
    public class SkillFactory:Singleton<SkillFactory>, ICanGetModel
    {
        private SkillFactory()
        {
        }

        private readonly Dictionary<string, Type> skillMapping = new Dictionary<string, Type>()
        {
            {SkillModelAssets.普通奥术射击, typeof(FireSkill)},
            {SkillModelAssets.加速移动, typeof(SpeedUpSkill)},
            {SkillModelAssets.八个子弹, typeof(MoreBulletSkill)},
            {SkillModelAssets.普通攻击, typeof(NormalAttack)},
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

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}