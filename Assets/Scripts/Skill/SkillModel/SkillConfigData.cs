using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Sirenix.OdinInspector;
using SkillModule.Condition;

namespace Model.Skill
{

    public class SkillConfigData : ICloneable
    {
        [HorizontalGroup("技能数据")]

        [GUIColor("#4A90E2")]
        [VerticalGroup("技能数据/基础数据")]
        public int id;


        /// <summary>
        /// 技能名称
        /// </summary>
        [GUIColor("#4A90E2")]
        [VerticalGroup("技能数据/基础数据")]
        [LabelText("技能名称")]
        public string name;

        /// <summary>
        /// 技能绑定的按键
        /// </summary>
        [GUIColor("#4A90E2")]
        [VerticalGroup("技能数据/基础数据")]
        [ValueDropdown("@Const.ConstGet.GetAllPublicFieldNames<PlayerAction.DefMapsActions>()")]
        [LabelText("绑定按键")]
        public string bindKey;

        /// <summary>
        /// 技能字面量
        /// </summary>
        [GUIColor("#4A90E2")]
        [VerticalGroup("技能数据/基础数据")]
        [LabelText("技能字面量")]
        public string literalQuantity;

        /// <summary>
        /// 技能冷却时间
        /// </summary>
        [GUIColor("#4A90E2")]
        [VerticalGroup("技能数据/基础数据")]
        [LabelText("技能冷却时间")]
        public float cd;

        /// <summary>
        /// 基础伤害
        /// </summary>
        [GUIColor("#4A90E2")]
        [VerticalGroup("技能数据/基础数据")]
        [LabelText("基础伤害")]
        public int baseDamage = 10;

        /// <summary>
        /// 技能条件器
        /// </summary>
        [GUIColor("#ffcc33")]
        [VerticalGroup("技能数据/技能条件器")]
        [LabelText("技能条件器")]
        public List<AddSkillConditionInfo> skillConditions = new List<AddSkillConditionInfo>();

        /// <summary>
        /// 黑板数据
        /// </summary>
        [GUIColor("#7ED321")]
        [VerticalGroup("技能数据/条件器黑板数据")]
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


        [GUIColor("#abcdefee")]
        [HorizontalGroup("操作", width: 0.33f), ButtonGroup("操作/技能操作")]
        [Button("初始化黑板", ButtonSizes.Large)]
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