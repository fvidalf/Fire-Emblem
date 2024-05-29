using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class LightAndDarkSkill: MultiCharacterSkill {
    
    public LightAndDarkSkill()
        : base("Light and Dark", new SingleCharacterSkill[] {
            new LightAndDarkPenaltySkill(),
            new AgneasArrowSkill(),
            new BeorcsBlessingSkill()
        }) {
    }
}