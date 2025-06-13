using Combat;
using QFramework;

namespace DefaultNamespace
{
    public class GameArch: Architecture<GameArch>
    {
        protected override void Init()
        {
            // 战斗系统
            RegisterSystem<CombatMgr>(new CombatMgr());
        }
    }
}