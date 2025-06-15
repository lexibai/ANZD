using Actor;
using Bullet;
using Combat.Command;
using Model.Skill;
using QFramework;
using UnityEngine;

public class EnemyObj : ActorObj
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalActorData.hp = 30;
        originalActorData.nowHp = 30;
        originalActorData.attack = 10;
        originalActorData.defense = 5;
        originalActorData.moveSpeed = 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var playerObj = PlayerObj.Instance;
        if (playerObj != null)
        {
            //接近玩家
            transform.Translate((playerObj.transform.position - transform.position).normalized * (actorData.moveSpeed * Time.deltaTime));
            
            
            if (Vector3.Distance(playerObj.transform.position, transform.position) < 1.5f)
            {
                //伤害玩家
                this.SendCommand<DamageCommand>(new DamageCommand(this, playerObj, null, null));
            }
        }
    }



    public override void OnTakeDamage(ActorObj attacker, Skill skill, BulletObj bullet)
    {
        
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
}
