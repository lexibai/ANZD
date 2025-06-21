using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actor.Enemy
{
    public class EnemyMgr : MonoSingleton<EnemyMgr>
    {
        //生成怪物间隔
        public float spawnInterval = 1;

        /// <summary>
        /// 怪物预制体
        /// </summary>
        private GameObject enemyPrefab;
        
        // todo: 将怪物进行数据化管理，随后使用资源加载器加载
        //private ResLoader rl = ResLoader.Allocate();
        
        public List<EnemyGenerateInfo> enemyGenerateInfos = new();

        /// <summary>
        /// 初始化, 用于唤出管理器  
        /// </summary>
        public void Init()
        {
        }
        
        private void Awake()
        {
            enemyPrefab = Resources.Load<GameObject>("Enemy");
            // enemyGenerateInfos.Add(new EnemyGenerateInfo()
            // {
            //     prefab = enemyPrefab,
            //     num = 10000
            // });
        }

        private void Update()
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0)
            {
                //检测场景中敌人数量
                int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
                if (enemyCount >= 50)
                {
                    return;
                }
                
                var enemyP = RandomEnemyP();
                if (enemyP == null)
                {
                    return;
                }

                spawnInterval = Random.Range(0.1f, 0.5f);
                var enemyObj = Instantiate(enemyP);

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

        public void AddEnemyInfo(EnemyGenerateInfo enemyGenerateInfo)
        {
            enemyGenerateInfos.Add(enemyGenerateInfo);
        }

        private GameObject RandomEnemyP()
        {
            if (enemyGenerateInfos.Count == 0)
            {
                return null;
            }
            
            //随机选择一个怪物预制体
            var enemyGenerateInfo = enemyGenerateInfos[Random.Range(0, enemyGenerateInfos.Count)];
            var enemyP = enemyGenerateInfo.prefab;

            if (enemyP == null)
            {
                return null;
            }
            
            enemyGenerateInfo.num--;
            if (enemyGenerateInfo.num <= 0)
            {
                enemyGenerateInfos.Remove(enemyGenerateInfo);
            }

            return enemyP;
        }
    }
}