using System;
using Sirenix.OdinInspector;

namespace Bullet
{
    public class BulletAddBuffConfigInfo
    {
        [LabelText("buff名称")]
        public string buffName;


        [LabelText("buff实例化类名")]
        public string buffClassName;


        [LabelText("buff层数")]
        public int buffNum;
    }
}