using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class BowGuardSkill: GuardSkill {
    public BowGuardSkill() : base("Bow Guard") {}
    
    protected override bool IsConditionMet() {
        var doesRivalUseBow = RoundStatus.RivalCharacterModel.Weapon == "Bow";
        return doesRivalUseBow;
    }
}