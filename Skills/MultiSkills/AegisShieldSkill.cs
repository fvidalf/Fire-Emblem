using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class AegisShieldSkill: MultiSkill {
    
    public AegisShieldSkill() : base("Aegis Shield", new ISingleSkill[] {
        new AegisShieldBonusSkill(),
        new AegisShieldFirstAttackDamageReductionSkill()
    }) { }
    
}