using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class CloseDefSkill: MultiSkill {
    
    public CloseDefSkill()
        : base("Close Def", new StatModifierSkill[] {
            new CloseDefBonusSkill(),
            new CloseDefNeutralizer()
        }) {
    }
}