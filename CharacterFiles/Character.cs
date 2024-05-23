using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.MultiCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles;

public class Character {

    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    public IBaseSkill[] Skills { get; private set; }
    public SingleCharacterSkill[] SingleSkills { get; private set; }
    public GameStatus GameStatus { get; private set; }

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

    public int Atk { get; set; }
    public int Spd { get; set; }
    public int Def { get; set; }
    public int Res { get; set; }
    
    public int FirstAttackHp { get; set; }
    public int FirstAttackAtk { get; set; }
    public int FirstAttackSpd { get; set; }
    public int FirstAttackDef { get; set; }
    public int FirstAttackRes { get; set; }
    
    public Character? MostRecentRival { get; private set; }

    public bool IsDead;

    private SkillEffect _selfModifiedStats = new SkillEffect();
    private SkillEffect _rivalModifiedStats = new SkillEffect();

    private View _view;

    public Character(
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
        Atk = atk;
        BaseAtk = atk;
        Spd = spd;
        BaseSpd = spd;
        Def = def;
        BaseDef = def;
        Res = res;
        BaseRes = res;
        _view = view;

        PrepareSkills();
        StatModifiers = new StatModifiers();

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

    public void Attack(Character target) {
        var thisTurnAtk = GetThisTurnAtk();
        var thisTurnDef = GetThisTurnDef(target);
        var thisTurnRes = GetThisTurnRes(target);
        ExecuteAttack(thisTurnAtk, thisTurnDef, thisTurnRes, target);
        SetMostRecentRival(target);
    }
    
    private int GetThisTurnAtk() {
        var thisTurnAtk = Atk;
        if (FirstAttackAtk != 0) {
            thisTurnAtk += FirstAttackAtk;
            FirstAttackAtk = 0;
        }
        return thisTurnAtk;
    }
    
    private int GetThisTurnDef(Character target) {
        var thisTurnDef = target.Def;
        if (target.FirstAttackDef != 0) {
            thisTurnDef += target.FirstAttackDef;
            target.FirstAttackDef = 0;
        }
        return thisTurnDef;
    }
    
    private int GetThisTurnRes(Character target) {
        var thisTurnRes = target.Res;
        if (target.FirstAttackRes != 0) {
            thisTurnRes += target.FirstAttackRes;
            target.FirstAttackRes = 0;
        }
        return thisTurnRes;
    }

    private void ExecuteAttack(int thisTurnAtk, int thisTurnDef, int thisTurnRes, Character target) {
        var discount = IsPhysical() ? thisTurnDef : thisTurnRes;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * GetWeaponTriangleAdvantage(target))) - discount), 0);
        target.Hp -= damage;
        _view.WriteLine($"{Name} ataca a {target.Name} con {damage} de daño");
    }
    
    public bool IsPhysical() {
        return Weapon is "Sword" or "Axe" or "Lance" or "Bow";
    }
    
    private double GetWeaponTriangleAdvantage(Character target) {
        return WeaponTriangleAdvantage.GetAdvantage(this, target);
    }

    public void SetMostRecentRival(Character target) {
        MostRecentRival = target;
        target.MostRecentRival = this;
    }
    
    public void ReceiveGameStatus(GameStatus gameStatus) {
        GameStatus = gameStatus;
    }

    public void ResetSkills() {
        foreach (var skill in SingleSkills) {
            skill.IsActivated = false;
            skill.Reset();
        }
        ResetStats();
    }

    private void ResetStats() {
        ResetCharacterStats();
        ResetModifiedStats();
    }
    
    private void ResetCharacterStats() {
        ResetAtk();
        ResetSpd();
        ResetDef();
        ResetRes();
    }
    
    private void ResetAtk() {
        Atk = BaseAtk;
    }
    
    private void ResetSpd() {
        Spd = BaseSpd;
    }
    
    private void ResetDef() {
        Def = BaseDef;
    }
    
    private void ResetRes() {
        Res = BaseRes;
    }

    public void ResetModifiedStats() {
        _selfModifiedStats = new SkillEffect();
        _rivalModifiedStats = new SkillEffect();
    }
    
    public void ApplySkill(IBaseSkill skill, GameStatus gameStatus) {
        skill.Apply(gameStatus);
        var characterPairedToSkillEffect = GetStatsModifiedBySkill((SingleCharacterSkill)skill);
        UpdateModifiedStats(characterPairedToSkillEffect);
    }
    
    private Dictionary<Character, SkillEffect> GetStatsModifiedBySkill(IBaseSkill skill) {
        return skill.GetModifiedStats();
    }
    
    private void UpdateModifiedStats(Dictionary<Character, SkillEffect> characterPairedToSkillEffect) {
        foreach (var pair in characterPairedToSkillEffect) {
            var character = pair.Key;
            var skillEffect = pair.Value;
            if (character == this) {
                UpdateSelfModifiedStats(skillEffect);
            }
            else {
                UpdateRivalModifiedStats(skillEffect);
            }
        }
    }
    
    private void UpdateSelfModifiedStats(SkillEffect newSkillEffect) {
        _selfModifiedStats.Join(newSkillEffect);
    }
    
    private void UpdateRivalModifiedStats(SkillEffect newSkillEffect) {
        _rivalModifiedStats.Join(newSkillEffect);
    }

    public Dictionary<Character, SkillEffect> GetSkillEffects() {
        var skillEffects = new Dictionary<Character, SkillEffect>();
        var rival = GameStatus.RivalCharacter;
        skillEffects[this] = _selfModifiedStats;
        skillEffects[rival] = _rivalModifiedStats;
        return skillEffects;
    }

    public void NeutralizeStats(List<Stat> statsToNeutralize) {
        StatModifiers.NeutralizeStats(this, statsToNeutralize);
    }
}