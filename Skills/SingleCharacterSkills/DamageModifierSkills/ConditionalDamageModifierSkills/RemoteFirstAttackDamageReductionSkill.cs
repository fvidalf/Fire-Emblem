using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class RemoteFirstAttackDamageReductionSkill: ConditionalDamageModifierSkill {
    
    public RemoteFirstAttackDamageReductionSkill() : base("Remote First Attack Damage Reduction", 
        new DamageModification(EffectType.FirstAttackDamagePercentageReduction, 0.3)
    ) {}
    
    protected override bool IsConditionMet() {
        var isActivatingFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        return isActivatingFirstToAttack;
    }
}