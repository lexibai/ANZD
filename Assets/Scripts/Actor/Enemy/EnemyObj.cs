using Actor;
using Blood;
using Bullet;
using Combat.Command;
using Model.Skill;
using QFramework;
using UnityEngine;

public class EnemyObj : ActorObj
{
    private ActorObj targetActor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        targetActor = FindFirstObjectByType<PlayerObj>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (targetActor != null)
        {
            //接近玩家
            Vector2 direction = (targetActor.transform.position - transform.position).normalized;

            //移动
            Move(direction);

            if (Vector3.Distance(targetActor.transform.position, transform.position) < 1.5f)
            {
                //伤害玩家
                this.SendCommand<DamageCommand>(new DamageCommand(this, targetActor, null, null));
            }
        }
    }



    public override void OnTakeDamage(ActorObj attacker, Skill skill, BulletObj bullet)
    {
        #region 计算方向
        Vector3 dir = Vector3.down;
        if (bullet != null)
        {
            dir = bullet.transform.right.normalized;
        }
        else if (attacker != null)
        {
            dir = (this.transform.position - attacker.transform.position).normalized;
        }
        #endregion
        BloodFactory.Instance.CreateDefBlood(transform, dir);
    }

    public override void OnHit(ActorObj target)
    {

    }

    public override void OnDeath()
    {
        Destroy(this.gameObject);
    }

    public override void OnHeal(int hp)
    {

    }

    public override Transform GetFireTransform()
    {
        return transform;
    }

    public override Transform GetFaceLookAtTransform()
    {
        return transform;
    }
}
