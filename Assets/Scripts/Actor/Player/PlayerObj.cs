using Actor;
using Bullet;
using Model.Skill;
using UnityEngine;

public class PlayerObj : ActorObj
{
    public Transform FirePos;
    
    public Transform HeadPos;

    public static PlayerObj Instance;
    
    
    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalActorData.hp = 100;
        originalActorData.nowHp = 100;
        originalActorData.attack = 10;
        originalActorData.defense = 5;
        originalActorData.moveSpeed = 10;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override Transform GetFireTransform()
    {
        return FirePos;
    }

    public override Transform GetFaceLookAtTransform()
    {
        return HeadPos;
    }
    
    public override void OnTakeDamage(ActorObj attacker, Skill skill, BulletObj bullet)
    {
        
    }

    public override void OnHit(ActorObj target)
    {
        
    }

    public override void OnDeath()
    {
        print("游戏结束");
    }

    public override void OnHeal(int hp)
    {
        
    }
}
