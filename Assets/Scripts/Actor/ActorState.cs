using System;

namespace Actor
{
    [Serializable]
    public class ActorState: ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}