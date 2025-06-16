using System;
using Actor;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Model.Skill.Impl.Attack
{
    public class NormalAttack:AbstractSkill
    { 

        
        
        public override void UseSkill(ActorObj userObj)
        {
            var mousePosV2 = userObj.GetFaceLookAtTransform().right;
            
            //可视化, 绘制一条射线, 持续1s
            Debug.DrawRay(userObj.transform.position, mousePosV2*20f, Color.red, 1f);
            
        }
    }
}