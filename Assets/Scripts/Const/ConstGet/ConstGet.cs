using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using QAssetBundle;

namespace Const
{
    public class ConstGet{
                // 获取所有常量值的方法
        public static IEnumerable<string> GetAllQAssetValues()
        {
            return typeof(Bulletsprite)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(fi => (string)fi.GetValue(null));
        }
    }
}