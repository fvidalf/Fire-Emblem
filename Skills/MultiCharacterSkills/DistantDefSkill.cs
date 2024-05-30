using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class DistantDefSkill: MultiCharacterSkill {
    
    public DistantDefSkill()
        : base("Distant Def", new StatModifierSkill[] {
            new DistantDefBonusSkill(),
            new DistantDefNeutralizer()
        }) {
    }
}