using Fire_Emblem_View;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.SkillEffectFiles;
using Fire_Emblem.Skills.SkillsOverSelf;

namespace Fire_Emblem.CharacterFiles;

public class Character {

    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    public IBaseSkill[] Skills { get; private set; }
    private GameStatus _gameStatus;

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

    public void Attack(Character target) {
        var thisTurnAtk = Atk;
        if (FirstAttackAtk != 0) {
            thisTurnAtk += FirstAttackAtk;
            FirstAttackAtk = 0;
        }
        var discount = IsPhysical() ? target.Def : target.Res;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * WeaponTriangleAdvantage(target))) - discount), 0);
        target.Hp -= damage;
        _view.WriteLine($"{Name} ataca a {target.Name} con {damage} de daño");
        MostRecentRival = target;
        target.MostRecentRival = this;
    }

    public void ReceiveStatus(GameStatus gameStatus) {
        _gameStatus = gameStatus;
    }

    public void ResetSkills() {
        foreach (var skill in Skills) {
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
        ResetSelfModifiedStats();
        ResetRivalModifiedStats();
    }

    private void ResetSelfModifiedStats() {
        _selfModifiedStats = new SkillEffect();
    }
    
    private void ResetRivalModifiedStats() {
        _rivalModifiedStats = new SkillEffect();
    }
    
    public void ApplySkills() {
        ResetSelfModifiedStats();
        ResetRivalModifiedStats();
        foreach (var skill in Skills) {
            if (!skill.IsActivated) ApplySkill(skill, _gameStatus);
        }
    }

    private void ApplySkill(IBaseSkill skill, GameStatus gameStatus) {
        skill.Apply(gameStatus);
        var characterPairedToSkillEffect = GetStatsModifiedBySkill((BaseSkill)skill);
        UpdateModifiedStats(characterPairedToSkillEffect);
    }

    public Dictionary<Character, SkillEffect> GetSkillEffects() {
        var skillEffects = new Dictionary<Character, SkillEffect>();
        skillEffects[this] = _selfModifiedStats;
        var rival = _gameStatus.RivalCharacter;
        skillEffects[rival] = _rivalModifiedStats;
        return skillEffects;
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
            // 
            switch (stat) {
                case Stat.HpPenalty:
                    Hp += int.Abs(HpModifiers[EffectType.RegularPenalty]);
                    FirstAttackHp += int.Abs(HpModifiers[EffectType.FirstAttackPenalty]);
                    break;
                case Stat.AtkPenalty:
                    Atk += int.Abs(AtkModifiers[EffectType.RegularPenalty]);
                    FirstAttackAtk += int.Abs(AtkModifiers[EffectType.FirstAttackPenalty]);
                    break;
                case Stat.SpdPenalty:
                    Spd += int.Abs(SpdModifiers[EffectType.RegularPenalty]);
                    FirstAttackSpd += int.Abs(SpdModifiers[EffectType.FirstAttackPenalty]);
                    break;
                case Stat.DefPenalty:
                    Def += int.Abs(DefModifiers[EffectType.RegularPenalty]);
                    FirstAttackDef += int.Abs(DefModifiers[EffectType.FirstAttackPenalty]);
                    break;
                case Stat.ResPenalty:
                    Res += int.Abs(ResModifiers[EffectType.RegularPenalty]);
                    FirstAttackRes += int.Abs(ResModifiers[EffectType.FirstAttackPenalty]);
                    break;
            }
        }
    }
}