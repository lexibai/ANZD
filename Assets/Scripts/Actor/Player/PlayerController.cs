using Const;
using DefaultNamespace;
using Model.Skill;
using Model.Skill.Impl;
using Model.Skill.Impl.Move;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IController
{
    /// <summary>
    /// 武器的ik节点
    /// </summary>
    public Transform WeaponIkTransform;
    
    /// <summary>
    /// 头部的ik节点
    /// </summary>
    public Transform HeadIkTransform;

    /// <summary>
    /// 武器运动的圆心
    /// </summary>
    public Transform centerOfCircle;

    public PlayerObj playobj;
    
    /// <summary>
    /// 移动向量
    /// </summary>
    public Vector2 movement;

    public Animator animator;

    public Skill fireSkill;
    public Skill moveSkill;


    //发射间隔
    public float launchInterval = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playobj = GetComponent<PlayerObj>();
        fireSkill = SkillFactory.Instance.CreateSkill(SkillModelAssets.普通奥术射击);
        moveSkill = SkillFactory.Instance.CreateSkill(SkillModelAssets.加速移动);
        
    }

    // Update is called once per frame 
    void Update()
    {
        
        transform.Translate(movement*Vector2.one * (playobj.actorData.moveSpeed * Time.deltaTime));
        // print(movement);
        if (movement.x == 0 && movement.y != 0)
        {
            animator.SetInteger("speed", Mathf.Abs(Mathf.RoundToInt(movement.y)));
        }
        else
        {
            animator.SetInteger("speed", Mathf.RoundToInt(movement.x*transform.localScale.x));
        }
    }


    /// <summary>
    /// 指向鼠标方型
    /// </summary>
    /// <param name="ctx"></param>
    public void Look(InputAction.CallbackContext ctx)
    {
        if (Camera.main == null)
        {
            throw new System.NotImplementedException();
        }
        
        var mouseScreenPos = ctx.ReadValue<Vector2>();
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0));
        mousePos.z = 0;
        
        if (transform.position.x <= mousePos.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.y);
        }
        
        float radius = 1.2f; // 圆的半径
        Vector3 direction = mousePos - centerOfCircle.position;
        direction = Vector3.ClampMagnitude(direction, radius);
        WeaponIkTransform.position = centerOfCircle.position + direction;

        HeadIkTransform.position = mousePos;

    }

    /// <summary>
    /// 控制人物移动
    /// </summary>
    /// <param name="ctx"></param>
    public void Move(InputAction.CallbackContext ctx)
    {
            var readValue = ctx.ReadValue<Vector2>();
            movement = readValue.normalized;
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
            fireSkill.UseSkill(playobj);
        print("按下");
        if (launchInterval < 0)
        {
            fireSkill.UseSkill(playobj);
            launchInterval = 0.2f;
        }
        launchInterval -= Time.deltaTime;
    }
    
    public void MoveSkill(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
            moveSkill.UseSkill(playobj);
        print("按下");
    }

    public IArchitecture GetArchitecture()
    {
        return GameArch.Interface;
    }
}
