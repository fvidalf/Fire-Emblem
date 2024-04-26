using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FirstAttackSelfSkills;

public class IgnisSkill: FirstAttackSkill {

    public IgnisSkill()
        : base("Ignis") { }

    protected override void ConcreteApply() {
        var characterStat = GetCharacterStat(Character, Stat.Atk);
        var characterStatValue = GetCharacterStatValue(characterStat, Stat.Atk);
        
        var increaseInStat = (double) 0.5d * characterStatValue;
        var newStatEffect = new StatEffect(Stat.FirstAttackAtk, (int) increaseInStat);
        UpdateCharacterStat(Character, EffectType.FirstAttackBonus, newStatEffect);
    }
}