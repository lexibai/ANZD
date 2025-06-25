using System.ComponentModel;
using Actor;
using Model.Skill;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace SkillModule.Condition
{
    /// <summary>
    /// 技能条件抽象实现
    /// </summary>
    [DisplayName("瞄准条件器")]
    public class AimSkillCondition : DefSkillCondition
    {
        private GameObject line = null;
        private InputAction inputAction;
        public override bool Check(ActorObj userObj, Skill skill, bool prevCheck)
        {
            base.Check(userObj, skill, prevCheck);
            PlayerController playerController = userObj?.GetComponent<PlayerController>();
            inputAction = playerController.playerAction.FindAction(skill.skillData.bindKey);
            inputAction.canceled += Cancel;
            if (inputAction.IsPressed())
            {
                if (line == null)
                {
                    line = new GameObject();
                    LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
                    lineRenderer.startColor = Color.red;
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPositions(new Vector3[] {
                        userObj.GetFireTransform().position,
                        userObj.GetFireTransform().position+(userObj.GetFireTransform().right*100)
                    });
                    lineRenderer.startWidth = 0.1f;
                    lineRenderer.endWidth = 0.2f;
                    lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                }
                if (!line.activeSelf)
                {
                    line.SetActive(true);
                }
                line.GetComponent<LineRenderer>().SetPosition(0, userObj.GetFireTransform().position);
                line.GetComponent<LineRenderer>().SetPosition(1, (userObj.GetFaceLookAtTransform().right * 100) + userObj.GetFireTransform().position);
                return true;
            }
            return true;
        }

        public void Cancel(InputAction.CallbackContext ctx)
        {
            line.SetActive(false);
            inputAction.canceled -= Cancel;
        }


    }
}