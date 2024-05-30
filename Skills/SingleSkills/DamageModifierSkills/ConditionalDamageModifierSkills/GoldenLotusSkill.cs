using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class GoldenLotusSkill: ConditionalDamageModifierSkill {
    
    public GoldenLotusSkill() : base("Golden Lotus",
        new DamageModification(EffectType.FirstAttackDamagePercentageReduction, 0.5)
    ) {}
    
    protected override bool IsConditionMet() {
        var isRivalPhysical = RoundStatus.RivalCharacterModel.IsPhysical();
        return isRivalPhysical;
    }
}