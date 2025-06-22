using Actor;
using Buff;
using Buff.Command;
using Combat.Command;
using Const;
using QAssetBundle;
using QFramework;
using Tool;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Model.Skill.Impl.Attack
{
    public class NormalAttack : AbstractSkill
    {
        private ResLoader rl = ResLoader.Allocate();
        private RuntimeAnimatorController normalAnimatorController;

        public NormalAttack()
        {
            //rl.Add2Load<AnimatorController>(QAssetBundle.Skilleff.NORMALATKCTR, (b, res) =>
            normalAnimatorController = rl.LoadSync<RuntimeAnimatorController>(Normalatkctr_controller.NORMALATKCTR);
        }

        public override void UseSkill(ActorObj userObj)
        {
            var faceLookAtTransform = userObj.GetFaceLookAtTransform();
            var LookATDir = faceLookAtTransform.right;
            var hit = false;
            var searchBuffObj = userObj.SearchBuffObj(BuffAssets.普通攻击连段数);


            var gameObject = new GameObject(nameof(NormalAttack));
            gameObject.transform.position = faceLookAtTransform.position;
            gameObject.transform.rotation = faceLookAtTransform.rotation;
            gameObject.transform.localScale = Vector3.one * 1f;
            gameObject.AddComponent<SpriteRenderer>();

            var animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = normalAnimatorController;
            animator.Play("normalAtk");

            var sprite = gameObject.GetComponent<SpriteRenderer>();
            if (searchBuffObj?.nowCount == 1)
            {
                sprite.color = Color.yellow;
            }
            else if (searchBuffObj?.nowCount == 2)
            {
                sprite.color = Color.red;
            }
            else if (searchBuffObj?.nowCount == 3)
            {
                sprite.color = Color.magenta;
            }
            else
            {
                sprite.color = Color.white;
            }


            ActionKit.OnUpdate.Register(() => { gameObject.transform.position = faceLookAtTransform.position; })
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.Sequence()
                .Delay(0.05f)
                .Callback(() =>
                {
                    var overlapSectorComponent = PhysicsTool.OverlapSectorComponent<Rigidbody2D>(
                        faceLookAtTransform.position,
                        faceLookAtTransform.right,
                        230, 4f, LayerMask.GetMask("Default"));

                    //击退
                    foreach (var rigidbody2D in overlapSectorComponent)
                    {
                        if (rigidbody2D.CompareTag("Enemy"))
                        {
                            hit = true;
                            var enemyObj = rigidbody2D.gameObject.GetComponent<EnemyObj>();
                            //伤害敌人
                            this.SendCommand<DamageCommand>(
                                new DamageCommand(userObj, enemyObj.gameObject.GetComponent<ActorObj>(),
                                    this, null)
                            );
                            rigidbody2D.AddForce(
                                (rigidbody2D.transform.position - faceLookAtTransform.position).normalized * 30,
                                ForceMode2D.Impulse);
                        }
                    }

                    // 每次命中都添加连段buff，每层都加深颜色，三层后移除并触发位移
                    if (hit)
                    {
                        this.SendCommand<AddBuffCommand<BaseBuffObj>>(new AddBuffCommand<BaseBuffObj>(userObj,
                            this.GetModel<BuffModel>().SelectBuffData(BuffAssets.普通攻击连段数)));
                    }
                    else
                    {
                        searchBuffObj?.TriggerRemoveBuff();
                    }

                    if (searchBuffObj?.nowCount > 1)
                    {
                        userObj.GetComponent<PlayerController>().MagicSkill(new InputAction.CallbackContext());
                    }
                    if (searchBuffObj?.nowCount > 2)
                    {

                        userObj.GetComponent<PlayerController>().moveSkill.nowCd = -1f;
                        userObj.GetComponent<PlayerController>().MoveSkill(new InputAction.CallbackContext());
                    }
                    if (searchBuffObj?.nowCount > 3)
                    {
                        searchBuffObj?.TriggerRemoveBuff();
                    }
                })
                .Delay(0.4f)
                .Callback(() => { Object.Destroy(gameObject); }).Start(userObj);

            //可视化, 绘制一条射线, 持续1s 
            Debug.DrawRay(userObj.transform.position, LookATDir * 20f, Color.red, 1f);
        }

        //析构函数
        ~NormalAttack()
        {
            rl.Recycle2Cache();
            rl = null;
        }
    }
}