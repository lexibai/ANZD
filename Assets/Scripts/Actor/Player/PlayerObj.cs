using Actor;
using Bullet;
using Model.Skill;
using UnityEngine;

public class PlayerObj : ActorObj
{
    public Transform FirePos;

    public static PlayerObj Instance;
    
    //生成怪物间隔
    public float spawnInterval = 1;
    
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
        if (originalActorData.nowHp > 0)
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0)
            {
                spawnInterval = 1;
                var enemyObj = GameObject.Instantiate(Resources.Load<GameObject>("Enemy"));
                enemyObj.transform.position = transform.position + new Vector3(Random.Range(-10, 10f), 
                    Random.Range(-10, 10), 0);
            }
        }
    }

    public override Transform GetFireTransform()
    {
        return FirePos;
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
