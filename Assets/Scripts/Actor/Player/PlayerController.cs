using System;
using Combat.Command;
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
    // 输入控制类的实例  
    private PlayerAction playerAction;  
    
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

    public bool fireSwitch = false;
    
    public Skill fireSkill;
    public Skill moveSkill;
    public Skill magicSkill;

    private void Awake()
    {
        playerAction  = new PlayerAction();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playobj = GetComponent<PlayerObj>();
        fireSkill = SkillFactory.Instance.CreateSkill(SkillModelAssets.普通奥术射击);
        moveSkill = SkillFactory.Instance.CreateSkill(SkillModelAssets.加速移动);
        magicSkill = SkillFactory.Instance.CreateSkill(SkillModelAssets.八个子弹);
    }

    private void OnEnable()
    {
        playerAction.DefMaps.Look.performed += Look;
        playerAction.DefMaps.Move.performed += Move;
        playerAction.DefMaps.Move.canceled += Move;
        playerAction.DefMaps.Fire.started += FireStart;
        playerAction.DefMaps.Fire.canceled += FireEnd;
        playerAction.DefMaps.MoveSkill.performed += MoveSkill;
        playerAction.DefMaps.MoveSkill.canceled += MoveSkill;
        playerAction.DefMaps.MagicSkill.performed += MagicSkill;
        playerAction.Enable();
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

        Fire();
    }

    private void OnDisable()
    {
        playerAction.DefMaps.Look.performed -= Look;
        playerAction.DefMaps.Move.performed -= Move;
        playerAction.DefMaps.Move.canceled -= Move;
        playerAction.DefMaps.Fire.started -= FireStart;
        playerAction.DefMaps.Fire.canceled -= FireEnd;
        playerAction.DefMaps.MoveSkill.performed -= MoveSkill;
        playerAction.DefMaps.MoveSkill.canceled -= MoveSkill;
        playerAction.DefMaps.MagicSkill.performed -= MagicSkill;
        playerAction.Disable();
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

    public void FireStart(InputAction.CallbackContext ctx)
    {
        fireSwitch  = true;
    }

    public void Fire()
    {
        if(fireSwitch)
            this.SendCommand<bool>(new UseSkillCommand(playobj, fireSkill));
    }

    public void FireEnd(InputAction.CallbackContext ctx)
    {
        fireSwitch = false;
    }
    
    public void MoveSkill(InputAction.CallbackContext ctx)
    {
        this.SendCommand<bool>(new UseSkillCommand(playobj, moveSkill));
    }

    public void MagicSkill(InputAction.CallbackContext ctx)
    {
        this.SendCommand<bool>(new UseSkillCommand(playobj, magicSkill));
    }

    public IArchitecture GetArchitecture()
    {
        return GameArch.Interface;
    }
}
