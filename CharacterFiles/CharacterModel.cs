using System.Numerics;
using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.MultiSkills;
using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles;

public class CharacterModel {

    public string Name { get; }
    public string Weapon { get; }
    public string Gender { get; }
    public string DeathQuote { get; set; }
    private IBaseSkill[] Skills { get; }
    public ISingleSkill[] SingleSkills { get; private set; }
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

    public int Atk { get; private set; }
    public int Spd { get; private set; }
    public int Def { get; private set; }
    public int Res { get; private set; }
    
    public int FirstAttackAtk { get; set; }
    public int FirstAttackDef { get; set; }
    public int FirstAttackRes { get; set; }
    
    public int FollowUpAtk { get; set; }
    public int FollowUpDef { get; set; }
    public int FollowUpRes { get; set; }

    public CharacterModel? MostRecentRival { get; private set; }

    public bool IsDead;
    
    public bool HasUsedGuardBearingForAttacking { get; set; }
    public bool HasUsedGuardBearingForDefending { get; set; }
    
    public int SavedDamageDiff { get; set; }

    private SkillEffect _selfModifiedStats = new SkillEffect();
    private SkillEffect _rivalModifiedStats = new SkillEffect();
    private DamageModifiers _damageModifiers = new DamageModifiers();
    
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
        HasUsedGuardBearingForAttacking = false;
        HasUsedGuardBearingForDefending = false;
        SavedDamageDiff = 0;
        
        StatModifiers = new StatModifiers();

        PrepareSkills();
    }

    private void PrepareSkills() {
        SingleSkills = GetSingleSkills();
        SingleSkills = OrderSkills(SingleSkills);
    }
    
    private ISingleSkill[] GetSingleSkills() {
        var singleSkills = new List<ISingleSkill>();
        foreach (var skill in Skills) {
            singleSkills = AddSkillToList(skill, singleSkills);
        }
        return singleSkills.ToArray();
    }
    
    private List<ISingleSkill> AddSkillToList(IBaseSkill skill, List<ISingleSkill> decomposedSkills) {
        if (skill is MultiSkill multiCharacterSkill) {
            decomposedSkills.AddRange(multiCharacterSkill.DecomposeIntoList());
        }
        else if (skill is ISingleSkill singleCharacterSkill) {
            decomposedSkills.Add(singleCharacterSkill);     
        }
        return decomposedSkills;
    }
    
    private ISingleSkill[] OrderSkills(ISingleSkill[] skills) {
        var orderedSkills = new List<ISingleSkill>();
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
        foreach (var skill in SingleSkills) {
            skill.Reset();
        }
        ResetStats();
    }

    private void ResetStats() {
        ResetCharacterStats();
        ResetModifiedStats();
        ResetStatModifiers();
        ResetDamageModifiers();
    }
    
    private void ResetCharacterStats() {
        ResetAtk();
        ResetSpd();
        ResetDef();
        ResetRes();
        SavedDamageDiff = 0;
    }
    
    private void ResetAtk() {
        Atk = BaseAtk;
        FirstAttackAtk = 0;
        FollowUpAtk = 0;
    }
    
    private void ResetSpd() {
        Spd = BaseSpd;
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
    
    private void ResetDamageModifiers() {
        _damageModifiers = new DamageModifiers();
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
        foreach (var stat in statsToNeutralize) {
            var isBonus = StatSimplifier.IsBonusMap[stat];
            if (isBonus) {
                NeutralizeBonus(stat);
            } else {
                NeutralizePenalty(stat);
            }
        }
    }
    
    private void NeutralizeBonus(Stat stat) {
        var simplifiedStat = StatSimplifier.SimplifyStatMap[stat];
        var statModifier = StatModifiers.GetStatModifier(simplifiedStat);
        switch (simplifiedStat) {
            case Stat.Atk:
                NeutralizeAtkBonus(statModifier);
                break;
            case Stat.Spd:
                NeutralizeSpdBonus(statModifier);
                break;
            case Stat.Def:
                NeutralizeDefBonus(statModifier);
                break;
            case Stat.Res:
                NeutralizeResBonus(statModifier);
                break;
        }
        statModifier.ResetBonuses();
    }

    private void NeutralizePenalty(Stat stat) {
        var simplifiedStat = StatSimplifier.SimplifyStatMap[stat];
        var statModifier = StatModifiers.GetStatModifier(simplifiedStat);
        switch (simplifiedStat) {
            case Stat.Atk:
                NeutralizeAtkPenalty(statModifier);
                break;
            case Stat.Spd:
                NeutralizeSpdPenalty(statModifier);
                break;
            case Stat.Def:
                NeutralizeDefPenalty(statModifier);
                break;
            case Stat.Res:
                NeutralizeResPenalty(statModifier);
                break;
        }
        statModifier.ResetPenalties();
    }
    
    private void NeutralizeAtkBonus(StatModifier statModifier) {
        Atk -= int.Abs(statModifier.GetModifier(EffectType.RegularBonus));
        FirstAttackAtk -= int.Abs(statModifier.GetModifier(EffectType.FirstAttackBonus));
        FollowUpAtk -= int.Abs(statModifier.GetModifier(EffectType.FollowUpBonus));
    }
    
    private void NeutralizeSpdBonus(StatModifier statModifier) {
        Spd -= int.Abs(statModifier.GetModifier(EffectType.RegularBonus));
    }
    
    private void NeutralizeDefBonus(StatModifier statModifier) {
        Def -= int.Abs(statModifier.GetModifier(EffectType.RegularBonus));
        FirstAttackDef -= int.Abs(statModifier.GetModifier(EffectType.FirstAttackBonus));
        FollowUpDef -= int.Abs(statModifier.GetModifier(EffectType.FollowUpBonus));
    }
    
    private void NeutralizeResBonus(StatModifier statModifier){
        Res -= int.Abs(statModifier.GetModifier(EffectType.RegularBonus));
        FirstAttackRes -= int.Abs(statModifier.GetModifier(EffectType.FirstAttackBonus));
        FollowUpRes -= int.Abs(statModifier.GetModifier(EffectType.FollowUpBonus));
    }
    
    private void NeutralizeAtkPenalty(StatModifier statModifier) {
        Atk += int.Abs(statModifier.GetModifier(EffectType.RegularPenalty));
        FirstAttackAtk += int.Abs(statModifier.GetModifier(EffectType.FirstAttackPenalty));
        FollowUpAtk += int.Abs(statModifier.GetModifier(EffectType.FollowUpPenalty));
    }
    
    private void NeutralizeSpdPenalty(StatModifier statModifier) {
        Spd += int.Abs(statModifier.GetModifier(EffectType.RegularPenalty));
    }
    
    private void NeutralizeDefPenalty(StatModifier statModifier) {
        Def += int.Abs(statModifier.GetModifier(EffectType.RegularPenalty));
        FirstAttackDef += int.Abs(statModifier.GetModifier(EffectType.FirstAttackPenalty));
        FollowUpDef += int.Abs(statModifier.GetModifier(EffectType.FollowUpPenalty));
    }
    
    private void NeutralizeResPenalty(StatModifier statModifier) {
        Res += int.Abs(statModifier.GetModifier(EffectType.RegularPenalty));
        FirstAttackRes += int.Abs(statModifier.GetModifier(EffectType.FirstAttackPenalty));
        FollowUpRes += int.Abs(statModifier.GetModifier(EffectType.FollowUpPenalty));
    }
    
    public DamageModifiers GetDamageModifiers() {
        return _damageModifiers.GetDamageModifiers();
    }
    
    public void UpdateDamageModifiers(DamageModification damageModification) {
        _damageModifiers.UpdateDamageModifier(damageModification);
    }
}