using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class SympatheticSkill: ConditionalDamageModifierSkill {
    
    public SympatheticSkill() : base("Sympathetic",
        new DamageModification(EffectType.RegularDamageAbsoluteReduction, 5)
    ) {}
    
    protected override bool IsConditionMet() {
        var isHpAtOrBelowHalf = RoundStatus.ActivatingCharacterModel.Hp <= RoundStatus.ActivatingCharacterModel.BaseHp / 2;
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        return isRivalFirstToAttack && isHpAtOrBelowHalf;
    }
    
}