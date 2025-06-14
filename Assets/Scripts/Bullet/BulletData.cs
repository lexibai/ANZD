using System;

namespace Bullet
{
    public class BulletData:ICloneable
    {
        // 技能基础伤害
        public int baseDamage = 10;
        
        // 子弹移动速度
        public int moveSpeed = 20;

        
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}