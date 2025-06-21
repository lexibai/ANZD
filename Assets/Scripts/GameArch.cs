using Actor.Enemy;
using Blood;
using Buff;
using Bullet;
using Combat;
using LogTool;
using Model.Skill;
using QFramework;
using Tool;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameArch : Architecture<GameArch>
    {
        protected override void Init()
        {
            if (Application.isPlaying)
            {
                ResKit.Init();
                EnemyMgr.Instance.Init();
                BloodFactory.Instance.Init();
            }

            


            //buff模型
            RegisterModel<BuffModel>(new BuffModel());
            //技能模型
            RegisterModel<SkillModel>(new SkillModel());
            // 日志配置模型
            RegisterModel<LogConfig>(new LogConfig());
            // 子弹模型
            RegisterModel<BulletModel>(new BulletModel());

            // 战斗系统
            RegisterSystem<CombatMgr>(new CombatMgr());

            // 持久化工具
            RegisterUtility<BinaryStorageUtility>(new BinaryStorageUtility());
        }
    }
}