using Buff;
using Combat;
using Model.Skill;
using QFramework;
using Tool;

namespace DefaultNamespace
{
    public class GameArch : Architecture<GameArch>
    {
        protected override void Init()
        {
            ResKit.Init();

            //buff模型
            RegisterModel<BuffModel>(new BuffModel());
            //技能模型
            RegisterModel<SkillModel>(new SkillModel());

            // 战斗系统
            RegisterSystem<CombatMgr>(new CombatMgr());

            // 持久化工具
            RegisterUtility<BinaryStorageUtility>(new BinaryStorageUtility());
        }
    }
}