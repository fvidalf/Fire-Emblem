using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class FairFightSkill: MultiCharacterSkill {
    
    public FairFightSkill()
        : base("Light and Dark", new StatModifierSkill[] {
            new FairFightSelfBonusSkill(),
            new FairFightRivalBonusSkill()
        }) {
    }
}