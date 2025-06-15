using System.Collections.Generic;
using Actor.Enemy;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.VisualScripting;
using UnityEditor;

namespace Editor.Tool
{
    public class EnemyTool:OdinEditorWindow
    {
        [MenuItem("安宁之地/工具/怪物工具")]
        public static void Open()
        {
            GetWindow<EnemyTool>().Show();
        }

        public EnemyGenerateInfo info;
        
        /// <summary>
        /// 添加敌人
        /// </summary>
        /// <param name="info"></param>
        [ButtonGroup("操作"), Button("添加敌人")]
        public void AddEnemys()
        {
            EnemyMgr.Instance.AddEnemyInfo(new EnemyGenerateInfo()
            {
                prefab = info.prefab,
                num = info.num
            });
        }
        
        public List<EnemyGenerateInfo> enemyGenerateInfos = new List<EnemyGenerateInfo>();

        protected override void OnEnable()
        {
            base.OnEnable();
            enemyGenerateInfos = EnemyMgr.Instance.enemyGenerateInfos;
        }


    }
}