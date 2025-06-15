using System;

namespace Bullet
{
    public class BulletData:ICloneable
    {
        // 技能基础伤害
        public int baseDamage = 10;
        
        // 子弹移动速度
        public int moveSpeed = 20;

        /// <summary>
        /// 子弹存活时间
        /// </summary>
        public float lifeTime=5f;

        
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}