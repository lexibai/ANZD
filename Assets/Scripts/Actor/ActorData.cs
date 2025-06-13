using System;

namespace Actor
{
    [Serializable]
    public class ActorData: ActorBaseData, ICloneable
    {
        public int nowHp;
        
        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}