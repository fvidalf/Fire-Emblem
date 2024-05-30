using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class LightAndDarkSkill: MultiSkill {
    
    public LightAndDarkSkill()
        : base("Light and Dark", new StatModifierSkill[] {
            new LightAndDarkPenaltySkill(),
            new AgneasArrowSkill(),
            new BeorcsBlessingSkill()
        }) {
    }
}