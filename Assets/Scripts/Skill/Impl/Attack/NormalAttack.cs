using System;
using Actor;
using Combat.Command;
using QFramework;
using Tool;
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
            var LookATDir = faceLookAtTransform.right;

            var gameObject = new GameObject(nameof(NormalAttack));
            gameObject.transform.position = faceLookAtTransform.position;
            gameObject.transform.rotation = faceLookAtTransform.rotation;
            gameObject.transform.localScale = Vector3.one * 1f;
            
            gameObject.AddComponent<SpriteRenderer>();
            var animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = normalAnimatorController;
            animator.Play("normalAtk");
            
            ActionKit.OnUpdate.Register(() =>
            {
                gameObject.transform.position = faceLookAtTransform.position;
                
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.Sequence()
                .Delay(0.05f)
                .Callback(() =>
                {
                    var overlapSectorComponent = PhysicsTool.OverlapSectorComponent<Rigidbody2D>(faceLookAtTransform.position, 
                        faceLookAtTransform.right,
                        230, 4f, LayerMask.GetMask("Default"));
                    
                    //击退
                    foreach (var rigidbody2D in overlapSectorComponent)
                    {
                        if (rigidbody2D.CompareTag("Enemy"))
                        {
                            var enemyObj = rigidbody2D.gameObject.GetComponent<EnemyObj>();
                            //伤害敌人
                            this.SendCommand<DamageCommand>(
                                new DamageCommand(userObj, enemyObj.gameObject.GetComponent<ActorObj>(), 
                                    this, null)
                            );
                            rigidbody2D.AddForce((rigidbody2D.transform.position-faceLookAtTransform.position).normalized * 30, ForceMode2D.Impulse);
                        }
                            
                    }
                })
                .Delay(0.4f)
                .Callback(() =>
                {
                    Object.Destroy(gameObject);
                }).Start(userObj);

            //可视化, 绘制一条射线, 持续1s 
            Debug.DrawRay(userObj.transform.position, LookATDir*20f, Color.red, 1f);
            
        }
       
        //析构函数
        ~NormalAttack()
        {
            rl.Recycle2Cache();
            rl = null;
        }
    }
}