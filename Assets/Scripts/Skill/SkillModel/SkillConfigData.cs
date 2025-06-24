using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using QFramework;
using Sirenix.OdinInspector;
using SkillModule.Condition;

namespace Model.Skill
{

    public class SkillConfigData : ICloneable
    {
        public int id;

        /// <summary>
        /// 技能名称
        /// </summary>
        [LabelText("技能名称")]
        public string name;

        /// <summary>
        /// 技能绑定的按键
        /// </summary>
        [ValueDropdown("@Const.ConstGet.GetAllPublicFieldNames<PlayerAction.DefMapsActions>()")]
        [LabelText("绑定按键")]
        public string bindKey;

        /// <summary>
        /// 技能字面量
        /// </summary>
        [LabelText("技能字面量")]
        public string literalQuantity;

        /// <summary>
        /// 技能冷却时间
        /// </summary>
        [LabelText("技能冷却时间")]
        public float cd;

        /// <summary>
        /// 基础伤害
        /// </summary>
        [LabelText("基础伤害")]
        public int baseDamage = 10;

        /// <summary>
        /// 技能条件器
        /// </summary>
        [LabelText("技能条件器")]
        public List<AddSkillConditionInfo> skillConditions = new List<AddSkillConditionInfo>();

        /// <summary>
        /// 黑板数据
        /// </summary>
        [LabelText("技能黑板数据")]
        public Dictionary<string, object> blackboard = new Dictionary<string, object>();

        public SkillData GetSkillData()
        {
            SkillData skillData = new SkillData();
            skillData.id = id;
            skillData.name = name;
            skillData.bindKey = bindKey;
            skillData.literalQuantity = literalQuantity;
            skillData.cd = cd;
            skillData.baseDamage = baseDamage;
            skillData.skillConditions = new List<SkillCondition>();
            foreach (var className in skillConditions)
            {
                Type skillConditionType = Type.GetType(className.className);
                skillData.skillConditions.Add((SkillCondition)Activator.CreateInstance(skillConditionType));
            }
            skillData.blackboard = blackboard;
            return skillData;
        }

        [Button("初始化黑板")]
        public void InitBlackboard()
        {
            blackboard = new Dictionary<string, object>();
            foreach (var condition in skillConditions)
            {
                var conditionType = Type.GetType(condition.className);
                InitializeBlackboardFromSkillCondition(conditionType, blackboard);
            }
        }

        public void InitializeBlackboardFromSkillCondition(Type conditionType, Dictionary<string, object> blackboard)
        {
            // 获取类级别的 DisplayName
            var classDisplayNameAttribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(
                conditionType,
                typeof(DisplayNameAttribute));

            string classDisplayName = classDisplayNameAttribute?.DisplayName ?? conditionType.Name;

            var publicProperties = conditionType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var publicFields = conditionType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in publicProperties)
            {
                var labelTextAttribute = (LabelTextAttribute)Attribute.GetCustomAttribute(
                    property,
                    typeof(LabelTextAttribute));

                string propertyDisplayName = labelTextAttribute?.Text ?? property.Name;
                string key = $"{classDisplayName}_{propertyDisplayName}";

                // 获取默认值
                var defaultValueAttribute = (DefaultValueAttribute)Attribute.GetCustomAttribute(
                    property,
                    typeof(DefaultValueAttribute));

                object defaultValue = null;

                if (defaultValueAttribute != null)
                {
                    defaultValue = defaultValueAttribute.Value;
                }
                else if (property.PropertyType.IsValueType)
                {
                    defaultValue = Activator.CreateInstance(property.PropertyType); // 值类型默认值
                }

                blackboard[key] = defaultValue;
            }

            foreach (var field in publicFields)
            {
                var labelTextAttribute = (LabelTextAttribute)Attribute.GetCustomAttribute(
                    field,
                    typeof(LabelTextAttribute));

                string fieldDisplayName = labelTextAttribute?.Text ?? field.Name;
                string key = $"{classDisplayName}_{fieldDisplayName}";

                // 获取默认值
                var defaultValueAttribute = (DefaultValueAttribute)Attribute.GetCustomAttribute(
                    field,
                    typeof(DefaultValueAttribute));

                object defaultValue = null;

                if (defaultValueAttribute != null)
                {
                    defaultValue = defaultValueAttribute.Value;
                }
                else if (field.FieldType.IsValueType)
                {
                    defaultValue = Activator.CreateInstance(field.FieldType); // 值类型默认值
                }

                blackboard[key] = defaultValue;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}