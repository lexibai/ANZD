using Actor;
using Bullet;
using Combat.Command;
using Model.Skill;
using QFramework;
using UnityEngine;

public class EnemyObj : ActorObj
{
    private ActorObj targetActor;
    
    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ActionKit.Repeat().Delay(3f)
            .Callback(() =>
            {
                print("圆形检测");
                var results = Physics2D.OverlapCircleAll(transform.position, 100f);
                foreach (var result in results)
                {
                    targetActor = result.GetComponent<PlayerObj>() ?? result.GetComponentInParent<PlayerObj>();
                    print("targetActor:" + targetActor);
                    if (targetActor != null)
                    {
                        break;
                    }
                }
            }).Start(this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        if (targetActor != null)
        {
            //接近玩家
            //transform.Translate((targetActor.transform.position - transform.position).normalized * (actorData.moveSpeed * Time.deltaTime));
            
            
            Vector2 direction = (targetActor.transform.position - transform.position).normalized;
            rb.AddForce(direction * actorData.moveSpeed * rb.mass);

            // 限制最大速度
            if (rb.linearVelocity.magnitude > actorData.moveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * actorData.moveSpeed;
            }
            
            if (Vector3.Distance(targetActor.transform.position, transform.position) < 1.5f)
            {
                //伤害玩家
                this.SendCommand<DamageCommand>(new DamageCommand(this, targetActor, null, null));
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

    public override Transform GetFaceLookAtTransform()
    {
        return transform;
    }
}
