using Fire_Emblem.Skills.SingleCharacterSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public abstract class LullSkill: MultiCharacterSkill {

    protected StatModifierSkill PenaltySkill;
    protected StatModifierSkill NeutralizeSkill;
    
    public LullSkill(string name, StatModifierSkill penaltySkill, StatModifierSkill neutralizeSkill)
        : base(name, new StatModifierSkill[] {penaltySkill, neutralizeSkill})
    {
        PenaltySkill = penaltySkill;
        NeutralizeSkill = neutralizeSkill;
    }
}