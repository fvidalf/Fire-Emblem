using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.FirstAttackSkills;

public class IgnisSkill: FirstAttackSkill {

    public IgnisSkill()
        : base("Ignis") { }

    protected override void ConcreteApply() {
        var characterAtk = Character.BaseAtk;
        var increaseInAtk = (double) 0.5d * characterAtk;
        var newStatEffect = new StatEffect(Stat.FirstAttackAtk, (int) increaseInAtk);
        UpdateStat(Character, EffectType.FirstAttackBonus, newStatEffect);
    }

    public override void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
}