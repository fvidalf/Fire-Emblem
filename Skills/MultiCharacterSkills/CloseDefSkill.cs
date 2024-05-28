using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class CloseDefSkill: MultiCharacterSkill {
    
    public CloseDefSkill()
        : base("Close Def", new SingleCharacterSkill[] {
            new CloseDefBonusSkill(),
            new CloseDefNeutralizer()
        }) {
    }
}