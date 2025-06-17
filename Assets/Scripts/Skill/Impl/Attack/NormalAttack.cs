using System;
using Actor;
using QFramework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;


namespace Model.Skill.Impl.Attack
{
    public class NormalAttack:AbstractSkill
    {

        private ResLoader rl = ResLoader.Allocate();
        private AnimatorController normalAnimatorController;
        
        public NormalAttack()
        {
            //rl.Add2Load<AnimatorController>(QAssetBundle.Skilleff.NORMALATKCTR, (b, res) =>
            normalAnimatorController = rl.LoadSync<AnimatorController>("normalAtkCtr");
        }
        
        public override void UseSkill(ActorObj userObj)
        {
            var faceLookAtTransform = userObj.GetFaceLookAtTransform();
            var mousePosV2 = faceLookAtTransform.right;

            var gameObject = new GameObject(nameof(NormalAttack));
            gameObject.transform.position = userObj.transform.position;
            gameObject.transform.rotation = faceLookAtTransform.rotation;
            
            gameObject.AddComponent<SpriteRenderer>();
            var animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = normalAnimatorController;
            animator.Play("normalAtk");

            ActionKit.OnUpdate.Register(() =>
            {
                gameObject.transform.position = userObj.transform.position;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.Delay(1f, () =>
            {
                Object.Destroy(gameObject);
            }).Start(userObj);

            //可视化, 绘制一条射线, 持续1s 
            Debug.DrawRay(userObj.transform.position, mousePosV2*20f, Color.red, 1f);
            
        }
        
        //析构函数
        ~NormalAttack()
        {
            rl.Recycle2Cache();
            rl = null;
        }
    }
}