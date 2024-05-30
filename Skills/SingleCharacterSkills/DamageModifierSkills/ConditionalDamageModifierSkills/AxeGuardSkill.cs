using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class AxeGuardSkill: GuardSkill {
    public AxeGuardSkill() : base("Axe Guard") {}

    protected override bool IsConditionMet() {
        var doesRivalUseBow = RoundStatus.RivalCharacterModel.Weapon == "Axe";
        return doesRivalUseBow;
    }
}