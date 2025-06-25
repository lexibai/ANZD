using Actor;
using DefaultNamespace;
using LogTool;
using QFramework;
using UnityEngine.InputSystem;

namespace Model.Skill
{
    public abstract class AbstractSkill : IController, Skill
    {
        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public SkillData skillData { get; set; } = new SkillData();
        public float nowCd { get; set; }
        public abstract void UseSkill(ActorObj userObj);
        public virtual void Init(SkillData skillData)
        {
            this.skillData = skillData;
            nowCd = 0;
        }

        /// <summary>
        /// 是否可以使用技能, 特化的判断, 即便返回true, 也要根据战斗管理器得出的最终结果确定
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        public virtual bool CanUse(ActorObj userObj)
        {
            #region 日志打印
            PlayerController playerController = userObj?.GetComponent<PlayerController>();
            float v = playerController.playerAction.FindAction(skillData.bindKey).ReadValue<float>();
            if (playerController.playerAction.FindAction(skillData.bindKey).IsPressed())
            {
                XLog.Instance.debug($"{skillData.name}技能检测到{skillData.bindKey}按下");
            }
            XLog.Instance.debug($"{skillData.name}技能获得值：{v}");
            #endregion
            bool prepSuc = true;
            foreach (var item in skillData.skillConditions)
            {
                prepSuc = item.Check(userObj, this, prepSuc);
            }
            return prepSuc;
        }
    }
}