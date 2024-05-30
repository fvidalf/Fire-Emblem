using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class BushidoSkill: MultiSkill {
    
    public BushidoSkill()
        : base("Bushido", new ISingleSkill[] {
            new BushidoRegularDamageIncreaseSkill(),
            new DodgeSkill()
        }) {
    }
}