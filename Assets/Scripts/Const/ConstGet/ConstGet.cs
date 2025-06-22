using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using QAssetBundle;
using Sirenix.OdinInspector;

namespace Const
{
    public class ConstGet
    {
        // 获取所有常量值的方法
        public static IEnumerable<string> GetAllConstValues<T>()
        {
            return typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => (string)fi.GetValue(null));
        }

        public static IEnumerable<ValueDropdownItem> GetBuffClassName()
        {
            var baseType = typeof(Buff.BaseBuffObj);
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var items = new List<ValueDropdownItem>();
            items.Add(new ValueDropdownItem("默认Buff", baseType.FullName));

            foreach (var type in types)
            {
                if (type != baseType &&
                    type.IsClass &&
                    !type.IsAbstract &&
                    baseType.IsAssignableFrom(type))
                {
                    // 获取 DisplayName 特性
                    var displayNameAttr = type.GetCustomAttribute<DisplayNameAttribute>();
                    var name = displayNameAttr?.DisplayName ?? type.Name;

                    items.Add(new ValueDropdownItem(name, type.FullName));
                }
            }

            return items;
        }

    }
}