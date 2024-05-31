using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class DivineRecreationFirstAttackDamageReductionSkill : ConditionalDamageModifierSkill {

    public DivineRecreationFirstAttackDamageReductionSkill()
        : base("Divine Recreation: First Attack Damage Reduction",
            new DamageModification(EffectType.FirstAttackDamagePercentageReduction, 0.3)
        ) {}
    
    protected override bool IsConditionMet() {
        var isRivalAtOrAboveHalfHp = RoundStatus.RivalCharacterModel.Hp >= RoundStatus.RivalCharacterModel.BaseHp / 2;
        return isRivalAtOrAboveHalfHp;
    }
}