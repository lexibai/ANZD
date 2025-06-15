using System;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actor.Enemy
{
    public class EnemyMgr : MonoSingleton<EnemyMgr>
    {
        //生成怪物间隔
        public float spawnInterval = 1;

        private GameObject enemyPrefab;

        /// <summary>
        /// 初始化, 用于唤出管理器
        /// </summary>
        public void Init()
        {
            
        }
        
        private void Awake()
        {
            enemyPrefab = Resources.Load<GameObject>("Enemy");
        }

        private void Update()
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0)
            {
                spawnInterval = Random.Range(0.5f, 1.5f);
                var enemyObj = Instantiate(enemyPrefab);

                // 获取摄像机的正交视角尺寸和屏幕宽高比
                float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
                float halfHeight = Camera.main.orthographicSize;

                // 随机选择一个方向（左、右、上、下）生成敌人
                int side = Random.Range(0, 4);
                float spawnX, spawnY;

                switch (side)
                {
                    case 0: // 左边
                        spawnX = -halfWidth - 10;
                        spawnY = Random.Range(-halfHeight, halfHeight);
                        break;
                    case 1: // 右边
                        spawnX = halfWidth + 10;
                        spawnY = Random.Range(-halfHeight, halfHeight);
                        break;
                    case 2: // 下边
                        spawnX = Random.Range(-halfWidth, halfWidth);
                        spawnY = -halfHeight - 10;
                        break;
                    default: // 上边
                        spawnX = Random.Range(-halfWidth, halfWidth);
                        spawnY = halfHeight + 10;
                        break;
                }

                enemyObj.transform.position = new Vector3(spawnX, spawnY, 0);
            }
        }
    }
}