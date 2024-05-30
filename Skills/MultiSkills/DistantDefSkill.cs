using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class DistantDefSkill: MultiSkill {
    
    public DistantDefSkill()
        : base("Distant Def", new StatModifierSkill[] {
            new DistantDefBonusSkill(),
            new DistantDefNeutralizer()
        }) {
    }
}