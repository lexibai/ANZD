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
        actorData.hp = 30;
        actorData.nowHp = 30;
        actorData.attack = 10;
        actorData.defense = 5;
        actorData.moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
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
}
