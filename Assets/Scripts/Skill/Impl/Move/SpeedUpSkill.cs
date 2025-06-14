using Actor;
using DefaultNamespace;
using DefaultNamespace.Buff;
using DefaultNamespace.Buff.Command;
using QFramework;
using UnityEngine;

namespace Model.Skill.Impl.Move
{
    public class SpeedUpSkill:  AbstractSkill
    {
        
        
        public override void UseSkill(ActorObj userObj)
        {
            var buffData = new BuffData
            {
                id = Random.Range(-1000000000, 0),
                name = null,
                priority = 100,
                duration = 0.1f,
                tickTime = 0,
                canPermanent = false,
                resetDuration = false,
                addDuration = false,
                maxCount = 1,
                tags = null,
                addAttrs = new ActorBaseData[]
                {
                    new ActorBaseData()
                    {
                        moveSpeed = 50
                    },
                    new ActorBaseData()
                }
            };
            
            this.SendCommand<AddBuffCommand<BaseBuffObj>>(new AddBuffCommand<BaseBuffObj>(userObj, buffData));
            
        }
        


    }
}