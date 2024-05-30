using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiSkills.RemoteSkills;

public abstract class RemoteSkill: MultiSkill {
    
    public RemoteSkill(string name, StatModifierSkill statBonusSkill)
        : base(name, new ISingleSkill[] {statBonusSkill, new RemoteFirstAttackDamageReductionSkill()})
    {}
    
}