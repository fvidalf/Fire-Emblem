using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class ChivalrySkill: MultiSkill {
    
    public ChivalrySkill()
        : base("Chivalry", new ISingleSkill[] {
            new ChivalryRegularDamageIncreaseSkill(),
            new ChivalryRegularDamageAbsoluteReductionSkill()
        }) {
    }
}