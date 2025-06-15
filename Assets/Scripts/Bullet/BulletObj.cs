using System;
using System.Collections;
using System.Collections.Generic;
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
        private Coroutine destroyRoutine;
        public BulletData bulletData = new BulletData();
        
        private void Awake()
        {
            var spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Circle");
        }

        private void Start()
        {
            destroyRoutine = StartCoroutine(DestroyAfterDelay(bulletData.lifeTime));
            hitCollider = this.gameObject.GetComponent<Collider2D>();
            hitCollider.isTrigger = true;
        }

        private void Update()
        {
            transform.Translate(Vector3.right * (Time.deltaTime * bulletData.moveSpeed));
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
        
        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (destroyRoutine != null)
            {
                StopCoroutine(destroyRoutine);
            }
        }
        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }
        
        
    }
}