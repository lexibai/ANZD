using System;

namespace Actor
{
    [Serializable]
    public class ActorBaseData: ICloneable
    {
        //tip: 此处新增变量, 必须修改对应子类的重载计算方法!!!
        
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