using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class ChivalryRegularDamageAbsoluteReductionSkill: ConditionalDamageModifierSkill {
    
    public ChivalryRegularDamageAbsoluteReductionSkill() : base("Chivalry Regular Damage Increase",
        new DamageModification(EffectType.RegularDamageAbsoluteReduction, 2)
    ) {}
    
    protected override bool IsConditionMet() {
        var isActivatingFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalFullHp = RoundStatus.RivalCharacterModel.Hp == RoundStatus.RivalCharacterModel.BaseHp;
        return isActivatingFirstToAttack && isRivalFullHp;
    }
}