﻿using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.MultiCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles;

public class CharacterModel {

    private static int _nextId = 0;
    public int Id { get; private set; } = _nextId++;

    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    public IBaseSkill[] Skills { get; private set; }
    public SingleCharacterSkill[] SingleSkills { get; private set; }
    public RoundStatus RoundStatus { get; private set; }

    public int BaseHp { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseSpd { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseRes { get; private set; }
    
    public StatModifiers StatModifiers { get; set; }

    private int _hp;

    public int Hp {
        get => _hp;
        set {
            if (value <= 0) {
                _hp = 0;
                IsDead = true;
            }
            else {
                _hp = value;
            }
        }
    }
    
    public bool HasUsedHpSkill { get; set; }

    public int Atk { get; set; }
    public int Spd { get; set; }
    public int Def { get; set; }
    public int Res { get; set; }
    
    public int FirstAttackHp { get; set; }
    public int FirstAttackAtk { get; set; }
    public int FirstAttackSpd { get; set; }
    public int FirstAttackDef { get; set; }
    public int FirstAttackRes { get; set; }
    
    public int FollowUpAtk { get; set; }
    public int FollowUpDef { get; set; }
    public int FollowUpRes { get; set; }

    public CharacterModel? MostRecentRival { get; private set; }

    public bool IsDead;

    private SkillEffect _selfModifiedStats = new SkillEffect();
    private SkillEffect _rivalModifiedStats = new SkillEffect();
    
    public CharacterModel(
        string name,
        string weapon,
        string gender,
        string deathQuote,
        IBaseSkill[] skills,
        int hp,
        int atk,
        int spd,
        int def,
        int res,
        View view
    ) {
        Name = name;
        Weapon = weapon;
        Gender = gender;
        DeathQuote = deathQuote;
        Skills = skills;
        Hp = hp;
        BaseHp = hp;
        HasUsedHpSkill = false;
        Atk = atk;
        BaseAtk = atk;
        Spd = spd;
        BaseSpd = spd;
        Def = def;
        BaseDef = def;
        Res = res;
        BaseRes = res;
        StatModifiers = new StatModifiers();

        PrepareSkills();
    }

    private void PrepareSkills() {
        SingleSkills = GetSingleSkills();
        SingleSkills = OrderSkills(SingleSkills);
    }
    
    private SingleCharacterSkill[] GetSingleSkills() {
        var singleSkills = new List<SingleCharacterSkill>();
        foreach (var skill in Skills) {
            singleSkills = AddSkillToList(skill, singleSkills);
        }
        return singleSkills.ToArray();
    }
    
    private List<SingleCharacterSkill> AddSkillToList(IBaseSkill skill, List<SingleCharacterSkill> decomposedSkills) {
        if (skill is MultiCharacterSkill multiCharacterSkill) {
            decomposedSkills.AddRange(multiCharacterSkill.DecomposeIntoList());
        }
        else if (skill is SingleCharacterSkill singleCharacterSkill) {
            decomposedSkills.Add(singleCharacterSkill);     
        }
        return decomposedSkills;
    }
    
    private SingleCharacterSkill[] OrderSkills(SingleCharacterSkill[] skills) {
        var orderedSkills = new List<SingleCharacterSkill>();
        foreach (var skill in skills) {
            if (skill is BonusNeutralizer or PenaltyNeutralizer) {
                orderedSkills.Add(skill);
            } else {
                orderedSkills.Insert(0, skill);
            }
        }
        return orderedSkills.ToArray();
    }
    
    public bool IsPhysical() {
        return Weapon is "Sword" or "Axe" or "Lance" or "Bow";
    }

    public void SetMostRecentRival(CharacterModel target) {
        MostRecentRival = target;
        target.MostRecentRival = this;
    }
    
    public void SetRoundStatus(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
    }

    public void ResetSkills() {
        //ReloadSkills();
        
        foreach (var skill in SingleSkills) {
            skill.IsActivated = false;
            skill.Reset();
        }
        ResetStats();
    }

    private void ResetStats() {
        ResetCharacterStats();
        ResetModifiedStats();
        ResetStatModifiers();
    }
    
    private void ResetCharacterStats() {
        ResetAtk();
        ResetSpd();
        ResetDef();
        ResetRes();
    }
    
    private void ResetAtk() {
        Atk = BaseAtk;
        FirstAttackAtk = 0;
        FollowUpAtk = 0;
    }
    
    private void ResetSpd() {
        Spd = BaseSpd;
        FirstAttackSpd = 0;
    }
    
    private void ResetDef() {
        Def = BaseDef;
        FirstAttackDef = 0;
        FollowUpDef = 0;
    }
    
    private void ResetRes() {
        Res = BaseRes;
        FirstAttackRes = 0;
        FollowUpRes = 0;
    }

    public void ResetModifiedStats() {
        _selfModifiedStats = new SkillEffect();
        _rivalModifiedStats = new SkillEffect();
    }
    
    private void ResetStatModifiers() {
        StatModifiers = new StatModifiers();
    }
    
    public void UpdateSelfModifiedStats(SkillEffect newSkillEffect) {
        _selfModifiedStats.Join(newSkillEffect);
    }
    
    public void UpdateRivalModifiedStats(SkillEffect newSkillEffect) {
        _rivalModifiedStats.Join(newSkillEffect);
    }

    public Dictionary<CharacterModel, SkillEffect> GetSkillEffects() {
        var skillEffects = new Dictionary<CharacterModel, SkillEffect>();
        var rival = RoundStatus.RivalCharacterModel;
        skillEffects[this] = _selfModifiedStats;
        skillEffects[rival] = _rivalModifiedStats;
        return skillEffects;
    }

    public void NeutralizeStats(List<Stat> statsToNeutralize) {
        StatModifiers.NeutralizeStats(this, statsToNeutralize);
    }

    private void ReloadSkills() {
        var newSkills = new List<IBaseSkill>();
        foreach (var skill in SingleSkills) {
            var newSkill = Activator.CreateInstance(skill.GetType()) as SingleCharacterSkill;
            newSkills.Add(newSkill);
        }
        Skills = newSkills.ToArray();
    }
}