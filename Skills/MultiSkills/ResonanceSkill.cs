using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;
namespace Fire_Emblem.Skills.MultiSkills;

public class ResonanceSkill : MultiSkill {
    
    public ResonanceSkill()
        : base("Mystic Boost", new ISingleSkill[] {
            new ResonanceRegularDamageIncreaseSkill(),
            new ResonancePreCombatHpModification()
        }) {}
}