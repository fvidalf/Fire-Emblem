using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
namespace Fire_Emblem.Skills.MultiSkills;

public class ScendscaleSkill : MultiSkill {
    
    public ScendscaleSkill()
        : base("Scendscale", new ISingleSkill[] {
                new ScendscaleRegularDamageIncreaseSkill(),
                new ScendscalePostCombatHpModification()
            }
        ) {}
}