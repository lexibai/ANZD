using System;

namespace Actor
{
    [Serializable]
    public class ActorState: ICloneable
    {
        public bool isHit;
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}