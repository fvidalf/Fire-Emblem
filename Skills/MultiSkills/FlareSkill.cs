using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;
using Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;
namespace Fire_Emblem.Skills.MultiSkills;

public class FlareSkill : MultiSkill {
    
    public FlareSkill()
        : base("Flare", new ISingleSkill[] {
            new FlarePenaltySkill(),
            new NosferatuPreCombatHealingSkill(),
            new NosferatuCombatHealingSkill()
        }) {}
    
}