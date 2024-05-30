using Fire_Emblem.Skills.SingleSkills;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public abstract class LullSkill: MultiSkill {

    protected StatModifierSkill PenaltySkill;
    protected StatModifierSkill NeutralizeSkill;
    
    public LullSkill(string name, StatModifierSkill penaltySkill, StatModifierSkill neutralizeSkill)
        : base(name, new StatModifierSkill[] {penaltySkill, neutralizeSkill})
    {
        PenaltySkill = penaltySkill;
        NeutralizeSkill = neutralizeSkill;
    }
}