using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public abstract class StanceSkill: MultiCharacterSkill {
    
    public StanceSkill(string name, StatModifierSkill statBonusSkill)
        : base(name, new ISingleCharacterSkill[] {statBonusSkill, new StanceFollowUpDamageReductionSkill()})
    {}
    
}