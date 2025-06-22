using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace Blood
{

    /// <summary>
    /// 流血特效工厂
    /// </summary>
    public class BloodFactory : MonoSingleton<BloodFactory>
    {
        ResLoader rl = ResLoader.Allocate();

        SimpleObjectPool<GameObject> bloodPool;

        Dictionary<string, GameObject> bloodMap = new();

        public void Init() { }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            GameObject gameobjectP = rl.LoadSync<GameObject>(QAssetBundle.Bloodeff.BundleName, QAssetBundle.Bloodeff.DEFBLOOD);
            bloodMap.Add("默认流血", gameobjectP);

            bloodPool = new SimpleObjectPool<GameObject>(() =>
            {
                var obj = GameObject.Instantiate(bloodMap["默认流血"]);
                obj.SetActive(false);
                return obj;
            }, obj =>
            {
                obj.SetActive(false);
            }, 50);
        }


        public GameObject CreateDefBlood(Transform pos, Vector3 dir)
        {
            GameObject gameObject = bloodPool.Allocate();
            gameObject.SetActive(true);
            gameObject.transform.position = pos.position;

            //dir是一个向量，我希望x轴转向这个方向
            gameObject.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

            ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            particleSystems.ForEach(obj =>
            {
                obj.Play();
            });

            ActionKit.Delay(0.2f, () =>
            {
                bloodPool.Recycle(gameObject);
            }).Start(this);

            return gameObject;
        }


        public override void Dispose()
        {
            base.Dispose();
            rl.Recycle2Cache();
            rl = null;
        }

    }
}
