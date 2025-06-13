using System;
using Actor;
using Combat.Command;
using DefaultNamespace;
using Model.Skill;
using QFramework;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bullet
{
    public class BulletObj:MonoBehaviour, IController
    {
        [FormerlySerializedAs("collider")] public Collider2D hitCollider;
        public Skill skill;
        public ActorObj attacker;
        public BulletData bulletData = new BulletData();
        
        private void Awake()
        {
            var spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
        }

        private void Start()
        {
            hitCollider = this.gameObject.GetComponent<Collider2D>();
            hitCollider.isTrigger = true;
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 10));
            
            //检测是否离开屏幕
            var worldToScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            if (worldToScreenPoint.x > Screen.width
                || worldToScreenPoint.x < 0 
                || worldToScreenPoint.y > Screen.height 
                || worldToScreenPoint.y < 0)
            {
                Destroy(this.gameObject);
            }
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            print(other.tag);
            if (other.CompareTag("Enemy"))
            {
                Destroy(this.gameObject);

                var enemyObj = other.gameObject.GetComponent<EnemyObj>();
                //伤害敌人
                this.SendCommand<DamageCommand>(new DamageCommand(attacker, other.gameObject.GetComponent<ActorObj>(), skill, this));
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
    }
}