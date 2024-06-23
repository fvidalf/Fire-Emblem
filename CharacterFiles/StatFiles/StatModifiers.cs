using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles.StatFiles;

public class StatModifiers {

    private readonly StatModifier _hpModifiers = new StatModifier();
    private readonly StatModifier _atkModifiers = new StatModifier();
    private readonly StatModifier _spdModifiers = new StatModifier();
    private readonly StatModifier _defModifiers = new StatModifier();
    private readonly StatModifier _resModifiers = new StatModifier();
    
    private readonly Dictionary<Stat, StatModifier> _statModifiers;
    
    public StatModifiers() {
        _statModifiers = new Dictionary<Stat, StatModifier>() {
            {Stat.Hp, _hpModifiers},
            {Stat.Atk, _atkModifiers},
            {Stat.Spd, _spdModifiers},
            {Stat.Def, _defModifiers},
            {Stat.Res, _resModifiers},
        };
    }
    
    public StatModifier GetStatModifier(Stat stat) {
        return _statModifiers[stat];
    }
    
    public void UpdateModifiers(EffectType effectType, StatEffect statEffect) {
        var simplifiedStat = StatSimplifier.SimplifyStatMap[statEffect.Stat];
        var statModifier = _statModifiers[simplifiedStat];
        statModifier.UpdateModifier(effectType, statEffect.Amount);
    }
}