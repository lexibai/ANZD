using System.Collections.Generic;
using Actor;
using Bullet;
using LogTool;
using Model.Skill;
using SkillModule;
using UnityEngine;
using QFramework;
using Const;
using Combat.Command;
using System;
using Sirenix.Serialization;


public class PlayerObj : ActorObj
{
    public Transform FirePos;

    public Transform HeadPos;

    public static PlayerObj Instance;

    [NonSerialized, OdinSerialize]
    public Dictionary<SkillType, Skill> skillSolt = new();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        originalActorData.hp = 100;
        originalActorData.nowHp = 100;
        originalActorData.attack = 10;
        originalActorData.defense = 5;
        originalActorData.moveSpeed = 10;
        skillSolt.Add(SkillType.Atk, SkillFactory.Instance.CreateSkill(SkillModelAssets.普通攻击));
        skillSolt.Add(SkillType.Fire, SkillFactory.Instance.CreateSkill(SkillModelAssets.普通奥术射击));
        skillSolt.Add(SkillType.Magic, SkillFactory.Instance.CreateSkill(SkillModelAssets.八个子弹));
        skillSolt.Add(SkillType.MartialSkill, SkillFactory.Instance.CreateSkill(SkillModelAssets.普通冲击波));
        skillSolt.Add(SkillType.Move, SkillFactory.Instance.CreateSkill(SkillModelAssets.加速移动));
        skillSolt.Add(SkillType.Ultimeta, SkillFactory.Instance.CreateSkill(SkillModelAssets.引爆炎烬个体));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void UseAtkSkill(SkillType skillType)
    {
        base.UseAtkSkill(skillType);
        this.SendCommand<bool>(new UseSkillCommand(this, skillSolt[skillType]));

    }

    public override Transform GetFireTransform()
    {
        return FirePos;
    }

    public override Transform GetFaceLookAtTransform()
    {
        return HeadPos;
    }

    public override void OnTakeDamage(ActorObj attacker, Skill skill, BulletObj bullet)
    {

    }

    public override void OnHit(ActorObj target)
    {

    }

    public override void OnDeath()
    {
        XLog.Instance.info("游戏结束");
    }

    public override void OnHeal(int hp)
    {

    }
}
