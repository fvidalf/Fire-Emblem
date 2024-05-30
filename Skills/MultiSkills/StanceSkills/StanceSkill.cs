using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public abstract class StanceSkill: MultiSkill {
    
    public StanceSkill(string name, StatModifierSkill statBonusSkill)
        : base(name, new ISingleSkill[] {statBonusSkill, new StanceFollowUpDamageReductionSkill()})
    {}
    
}