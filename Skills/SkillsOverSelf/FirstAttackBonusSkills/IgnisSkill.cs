using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FirstAttackBonusSkills;

public class IgnisSkill: FirstAttackBonusSkill {
    
    public IgnisSkill()
        : base(
            "Ignis", 
            new Dictionary<Stat, IConvertible> { { Stat.FirstAttackAtk, 0.5d }, }) { }

    protected override void ConcreteApply() {
        var characterStat = GetCharacterStat(Character, Stat.Atk);
        var characterStatValue = GetCharacterStatValue(characterStat, Stat.Atk);
        
        var increaseInStat = (double) StatsToModify[Stat.FirstAttackAtk] * characterStatValue;
        UpdateCharacterStat(Character, new KeyValuePair<Stat, int>(Stat.FirstAttackAtk, (int) increaseInStat));
        SetModifiedStats();
    }
}