using Fire_Emblem_View;
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
    public SingleCharacterSkill[] SingledSkills { get; private set; }
    public GameStatus GameStatus { get; private set; }

    public int BaseHp { get; private set; }
    public int BaseAtk { get; private set; }
    public int BaseSpd { get; private set; }
    public int BaseDef { get; private set; }
    public int BaseRes { get; private set; }
    
    public Dictionary<EffectType, int> HpModifiers;
    public Dictionary<EffectType, int> AtkModifiers;
    public Dictionary<EffectType, int> SpdModifiers;
    public Dictionary<EffectType, int> DefModifiers;
    public Dictionary<EffectType, int> ResModifiers;

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

        InitializeModifierMemory();
        PrepareSkills();

    }

    private void InitializeModifierMemory() {
        HpModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        AtkModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        SpdModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        DefModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        ResModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
    }

    private void PrepareSkills() {
        SingledSkills = DecomposeSkills();
        SingledSkills = OrderSkills(SingledSkills);
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
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * WeaponTriangleAdvantage(target))) - discount), 0);
        target.Hp -= damage;
        _view.WriteLine($"{Name} ataca a {target.Name} con {damage} de daño");
    }

    private void SetMostRecentRival(Character target) {
        MostRecentRival = target;
        target.MostRecentRival = this;
    }
    
    public void ReceiveGameStatus(GameStatus gameStatus) {
        GameStatus = gameStatus;
    }

    public void ResetSkills() {
        foreach (var skill in SingledSkills) {
            skill.IsActivated = false;
            skill.Reset();
        }
        ResetAffectedStats();
    }

    private void ResetAffectedStats() {
        ResetAtk();
        ResetSpd();
        ResetDef();
        ResetRes();
        ResetModifiedStats();
    }

    public void ResetModifiedStats() {
        _selfModifiedStats = new SkillEffect();
        _rivalModifiedStats = new SkillEffect();
    }
    
    public void ApplySkills() {
        ResetModifiedStats();
        foreach (var skill in SingledSkills) {
            if (!skill.IsActivated) ApplySkill(skill, GameStatus);
        }
    }
    
    private SingleCharacterSkill[] DecomposeSkills() {
        var decomposedSkills = new List<SingleCharacterSkill>();
        foreach (var skill in Skills) {
            if (skill is MultiCharacterSkill multiCharacterSkill) {
                decomposedSkills.AddRange(multiCharacterSkill.Decompose());
            }
            else if (skill is SingleCharacterSkill singleCharacterSkill) {
                decomposedSkills.Add(singleCharacterSkill);
            }
        }
        return decomposedSkills.ToArray();
    }
    
    private SingleCharacterSkill[] OrderSkills(SingleCharacterSkill[] skills) {
        var firstSkillsToApply = new List<SingleCharacterSkill>();
        var lastSkillsToApply = new List<SingleCharacterSkill>();
        foreach (var skill in skills) {
            if (skill is BonusNeutralizer or PenaltyNeutralizer) {
                lastSkillsToApply.Add(skill);
            }
            else {
                firstSkillsToApply.Add(skill);
            }
        }
        firstSkillsToApply.AddRange(lastSkillsToApply);
        return firstSkillsToApply.ToArray();
    }

    public void ApplySkill(IBaseSkill skill, GameStatus gameStatus) {
        skill.Apply(gameStatus);
        var characterPairedToSkillEffect = GetStatsModifiedBySkill((SingleCharacterSkill)skill);
        UpdateModifiedStats(characterPairedToSkillEffect);
    }

    public Dictionary<Character, SkillEffect> GetSkillEffects() {
        var skillEffects = new Dictionary<Character, SkillEffect>();
        skillEffects[this] = _selfModifiedStats;
        var rival = GameStatus.RivalCharacter;
        skillEffects[rival] = _rivalModifiedStats;
        return skillEffects;
    }

    private Dictionary<Character, SkillEffect> GetStatsModifiedBySkill(IBaseSkill skill) {
        switch (skill) {
            case ISingleCharacterSkill singleCharacterSkill:
                return singleCharacterSkill.GetModifiedStats();
            case IMultiCharacterSkill multiCharacterSkill:
                return multiCharacterSkill.GetModifiedStats();
            default:
                return new Dictionary<Character, SkillEffect>();
        }
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
    
    private float WeaponTriangleAdvantage(Character target) {
        switch (Weapon) {
            case "Sword" when target.Weapon == "Axe":
            case "Axe" when target.Weapon == "Lance":
            case "Lance" when target.Weapon == "Sword":
                return 1.2f;
            case "Axe" when target.Weapon == "Sword":
            case "Lance" when target.Weapon == "Axe":
            case "Sword" when target.Weapon == "Lance":
                return 0.8f;
            default:
                return 1.0f;
        }
    }
    
    public bool IsPhysical() {
        return Weapon is "Sword" or "Axe" or "Lance" or "Bow";
    }
    
    private void Heal(int amount) {
        Hp += amount;
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

    public void NeutralizeStats(List<Stat> statsToNeutralize) {
        foreach (var stat in statsToNeutralize) {
            switch (stat) {
                case Stat.HpPenalty:
                    Hp += int.Abs(HpModifiers[EffectType.RegularPenalty]);
                    FirstAttackHp += int.Abs(HpModifiers[EffectType.FirstAttackPenalty]);
                    HpModifiers[EffectType.RegularPenalty] = 0;
                    HpModifiers[EffectType.FirstAttackPenalty] = 0;
                    break;
                case Stat.AtkPenalty:
                     Console.WriteLine($"Añadiendo Bonus de Atk de {Name}");
                    Atk += int.Abs(AtkModifiers[EffectType.RegularPenalty]);
                    FirstAttackAtk += int.Abs(AtkModifiers[EffectType.FirstAttackPenalty]);
                    AtkModifiers[EffectType.RegularPenalty] = 0;
                    AtkModifiers[EffectType.FirstAttackPenalty] = 0;
                    break;
                case Stat.SpdPenalty:
                    Spd += int.Abs(SpdModifiers[EffectType.RegularPenalty]);
                    FirstAttackSpd += int.Abs(SpdModifiers[EffectType.FirstAttackPenalty]);
                    SpdModifiers[EffectType.RegularPenalty] = 0;
                    SpdModifiers[EffectType.FirstAttackPenalty] = 0;
                    break;
                case Stat.DefPenalty:
                    Def += int.Abs(DefModifiers[EffectType.RegularPenalty]);
                    FirstAttackDef += int.Abs(DefModifiers[EffectType.FirstAttackPenalty]);
                    DefModifiers[EffectType.RegularPenalty] = 0;
                    DefModifiers[EffectType.FirstAttackPenalty] = 0;
                    break;
                case Stat.ResPenalty:
                    Res += int.Abs(ResModifiers[EffectType.RegularPenalty]);
                    FirstAttackRes += int.Abs(ResModifiers[EffectType.FirstAttackPenalty]);
                    ResModifiers[EffectType.RegularPenalty] = 0;
                    ResModifiers[EffectType.FirstAttackPenalty] = 0;
                    break;
                case Stat.HpBonus:
                    Hp -= int.Abs(HpModifiers[EffectType.RegularBonus]);
                    FirstAttackHp -= int.Abs(HpModifiers[EffectType.FirstAttackBonus]);
                    HpModifiers[EffectType.RegularBonus] = 0;
                    HpModifiers[EffectType.FirstAttackBonus] = 0;
                    break;
                case Stat.AtkBonus:
                     Console.WriteLine($"Eliminando Bonus de Atk de {Name}");
                    Atk -= int.Abs(AtkModifiers[EffectType.RegularBonus]);
                    FirstAttackAtk -= int.Abs(AtkModifiers[EffectType.FirstAttackBonus]);
                    AtkModifiers[EffectType.RegularBonus] = 0;
                    AtkModifiers[EffectType.FirstAttackBonus] = 0;
                    break;
                case Stat.SpdBonus:
                    Spd -= int.Abs(SpdModifiers[EffectType.RegularBonus]);
                    FirstAttackSpd -= int.Abs(SpdModifiers[EffectType.FirstAttackBonus]);
                    SpdModifiers[EffectType.RegularBonus] = 0;
                    SpdModifiers[EffectType.FirstAttackBonus] = 0;
                    break;
                case Stat.DefBonus:
                    Def -= int.Abs(DefModifiers[EffectType.RegularBonus]);
                    FirstAttackDef -= int.Abs(DefModifiers[EffectType.FirstAttackBonus]);
                    DefModifiers[EffectType.RegularBonus] = 0;
                    DefModifiers[EffectType.FirstAttackBonus] = 0;
                    break;
                case Stat.ResBonus:
                    Res -= int.Abs(ResModifiers[EffectType.RegularBonus]);
                    FirstAttackRes -= int.Abs(ResModifiers[EffectType.FirstAttackBonus]);
                    ResModifiers[EffectType.RegularBonus] = 0;
                    ResModifiers[EffectType.FirstAttackBonus] = 0;
                    break;
            }
        }
    }
}