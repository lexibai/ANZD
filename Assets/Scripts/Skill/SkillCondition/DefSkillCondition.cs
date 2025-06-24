using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Model.Skill;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件抽象实现
    /// </summary>
    [DisplayName("默认条件器")]
    public class DefSkillCondition : SkillCondition
    {

        public virtual bool Check(Skill skill, bool prevCheck)
        {
            LoadFromBlackboard(skill);
            return true;
        }

               /// <summary>
        /// 从技能黑板中加载字段/属性值（基于 LabelText）
        /// </summary>
        /// <param name="skill">技能实例</param>
        protected void LoadFromBlackboard(Skill skill)
        {
            var blackboard = skill.skillData.blackboard;
            Type type = this.GetType();

            // 获取类级别的 LabelText
            var classLabelTextAttribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(
                type,
                typeof(DisplayNameAttribute));

            string classLabelText = classLabelTextAttribute?.DisplayName ?? type.Name;

            // 处理公共字段
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var labelTextAttribute = (LabelTextAttribute)Attribute.GetCustomAttribute(
                    field,
                    typeof(LabelTextAttribute));

                string fieldName = labelTextAttribute?.Text ?? field.Name;
                string key = $"{classLabelText}_{fieldName}";

                if (blackboard.TryGetValue(key, out object value) && value is not null)
                {
                    field.SetValue(this, Convert.ChangeType(value, field.FieldType));
                }
            }

            // 处理公共属性
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!property.CanWrite) continue; // 跳过只读属性

                var labelTextAttribute = (LabelTextAttribute)Attribute.GetCustomAttribute(
                    property,
                    typeof(LabelTextAttribute));

                string propertyName = labelTextAttribute?.Text ?? property.Name;
                string key = $"{classLabelText}_{propertyName}";

                if (blackboard.TryGetValue(key, out object value) && value is not null)
                {
                    property.SetValue(this, Convert.ChangeType(value, property.PropertyType));
                }
            }
        }
    }
}