using Fire_Emblem_View;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.FixedAmountSkills;

namespace Fire_Emblem.CharacterFiles;

public class Character {

    public string Name { get; set; }
    public string Weapon { get; set; }
    public string Gender { get; set; }
    public string DeathQuote { get; set; }
    public IBaseSkill[] Skills { get; private set; }

    private int _baseHp;
    private int _baseAtk;
    private int _baseSpd;
    private int _baseDef;
    private int _baseRes;

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

    public bool IsDead;

    private Dictionary<Stat, int> _modifiedStats = new Dictionary<Stat, int>();

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
        Atk = atk;
        Spd = spd;
        Def = def;
        Res = res;
        _view = view;
    }

    public void Attack(Character target) {
        var discount = IsPhysical() ? target.Def : target.Res;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(Atk * WeaponTriangleAdvantage(target))) - discount), 0);
        target.Hp -= damage;
        _view.WriteLine($"{Name} ataca a {target.Name} con {damage} de daño");
    }

    public void ApplySkills(GameStatus gameStatus) {
        foreach (var skill in Skills) {
            if (!skill.IsActivated) ApplySkill(skill, gameStatus);
        }
        NotifySkillEffects();
    }

    private void ApplySkill(IBaseSkill skill, GameStatus gameStatus) {
        skill.Apply(gameStatus);
        // Thisll have to be refactored: GetModifiedStats should probably be in IBaseSkill
        var statsModifiedBySkill = GetStatsModifiedBySkill((FixedAmountSkill)skill);
        UpdateModifiedStats(statsModifiedBySkill);
    }

    private Dictionary<Stat, int> GetStatsModifiedBySkill(FixedAmountSkill skill) {
        return skill.GetModifiedStats();
    }

    private void UpdateModifiedStats(Dictionary<Stat, int> stats) {
        foreach (var stat in stats) {
            if (_modifiedStats.ContainsKey(stat.Key)) {
                _modifiedStats[stat.Key] += stat.Value;
            }
            else {
                _modifiedStats[stat.Key] = stat.Value;
            }
        }
    }
    
    private void NotifySkillEffects() {
        // Sort the stats by key to ensure a consistent output
        var sortedStats = GetSortedStats();
        foreach (var stat in sortedStats) {
            if (stat.Value != 0) {
                var diffSign = stat.Value > 0 ? "+" : "";
                _view.WriteLine($"{Name} obtiene {StatToString.Map[stat.Key]}{diffSign}{stat.Value}");
            }
        }
    }
    
    private List<KeyValuePair<Stat, int>> GetSortedStats() {
        return _modifiedStats.OrderBy(stat => stat.Key).ToList();
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
    
    private bool IsPhysical() {
        return Weapon is "Sword" or "Axe" or "Lance" or "Bow";
    }
    
    private void Heal(int amount) {
        Hp += amount;
    }
    
    private void ResetAtk() {
        Atk = _baseAtk;
    }
    
    private void ResetSpd() {
        Spd = _baseSpd;
    }
    
    private void ResetDef() {
        Def = _baseDef;
    }
    
    private void ResetRes() {
        Res = _baseRes;
    }
}