using Combat;
using DefaultNamespace.Buff;
using QFramework;
using Tool;

namespace DefaultNamespace
{
    public class GameArch: Architecture<GameArch>
    {
        protected override void Init()
        {
            //buff模块
            RegisterModel<BuffModel>(new BuffModel());
            
            // 战斗系统
            RegisterSystem<CombatMgr>(new CombatMgr());
            
            // 持久化工具
            RegisterUtility<BinaryStorageUtility>(new BinaryStorageUtility());
        }
    }
}