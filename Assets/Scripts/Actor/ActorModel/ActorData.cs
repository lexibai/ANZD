using System;

namespace Actor
{
    [Serializable]
    public class ActorData : ActorBaseData, ICloneable
    {
        public int nowHp;

        public override object Clone()
        {
            return MemberwiseClone();
        }

        #region 重载运算

        public static ActorData operator +(ActorData a, ActorBaseData b)
        {
            return new ActorData()
            {
                hp = a.hp + b.hp,
                attack = a.attack + b.attack,
                defense = a.defense + b.defense,
                moveSpeed = a.moveSpeed + b.moveSpeed,
            };
        }
        public static ActorData operator -(ActorData a, ActorBaseData b)
        {
            return new ActorData()
            {
                hp = a.hp - b.hp,
                attack = a.attack - b.attack,
                defense = a.defense - b.defense,
                moveSpeed = a.moveSpeed - b.moveSpeed,
            };
        }
        public static ActorData operator *(ActorData a, ActorBaseData b)
        {
            return new ActorData()
            {
                attack = a.attack + (int)(a.attack * (b.attack / 100f)),
                defense = a.defense + (int)(a.defense * (b.defense / 100f)),
                moveSpeed = a.moveSpeed + (int)(a.moveSpeed * (b.moveSpeed / 100f)),
                hp = a.hp + (int)(a.hp * (b.hp / 100f)),
            };
        }
        public static ActorData operator /(ActorData a, ActorBaseData b)
        {
            return new ActorData()
            {
                attack = a.attack - (int)(a.attack * (b.attack / 100f)),
                defense = a.defense - (int)(a.defense * (b.defense / 100f)),
                moveSpeed = a.moveSpeed - (int)(a.moveSpeed * (b.moveSpeed / 100f)),
                hp = a.hp - (int)(a.hp * (b.hp / 100f)),
            };
        }

        #endregion
    }
}