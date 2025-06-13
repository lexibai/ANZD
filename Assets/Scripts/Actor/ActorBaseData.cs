using System;

namespace Actor
{
    [Serializable]
    public class ActorBaseData: ICloneable
    {
        // hp总量
        public int hp;
        
        // 攻击力
        public int attack;
        
        // 防御力
        public int defense;
        
        //移动速度
        public int moveSpeed;
        
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}