using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class ArmsShieldSkill: ConditionalDamageModifierSkill {
    
    public ArmsShieldSkill() : base("Arms Shield",
        new DamageModification(EffectType.RegularDamageAbsoluteReduction, 7)
    ) {}
    
    protected override bool IsConditionMet() {
        var activatingCharacter = RoundStatus.ActivatingCharacterModel;
        var rivalCharacter = RoundStatus.RivalCharacterModel;
        var numericAdvantage = WeaponTriangleAdvantage.GetAdvantage(activatingCharacter, rivalCharacter);
        return numericAdvantage > 1;
    }
}