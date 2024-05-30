using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class StanceFollowUpDamageReductionSkill: ConditionalDamageModifierSkill {
    
    public StanceFollowUpDamageReductionSkill() : base("Stance Follow Up Damage Reduction", 
        new DamageModification(EffectType.FollowUpDamagePercentageReduction, 0.1)
    ) {}
    
    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        return isRivalFirstToAttack;
    }
}