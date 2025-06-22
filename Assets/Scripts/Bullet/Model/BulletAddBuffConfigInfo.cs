using System;
using Sirenix.OdinInspector;

namespace Bullet
{
    public class BulletAddBuffConfigInfo
    {
        [LabelText("buff名称")]
        [ValueDropdown("@Const.ConstGet.GetAllConstValues<BuffAssets>()")]
        public string buffName;


        [LabelText("buff实例化类名")]
        [ValueDropdown("@Const.ConstGet.GetBuffClassName()")]
        public string buffClassName;


        [LabelText("buff层数")]
        public int buffNum;
    }
}