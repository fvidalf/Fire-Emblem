using Fire_Emblem.Skills.SingleCharacterSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public abstract class LullSkill: MultiCharacterSkill {

    protected SingleCharacterSkill PenaltySkill;
    protected SingleCharacterSkill NeutralizeSkill;
    
    public LullSkill(string name, SingleCharacterSkill penaltySkill, SingleCharacterSkill neutralizeSkill)
        : base(name, new SingleCharacterSkill[] {penaltySkill, neutralizeSkill})
    {
        PenaltySkill = penaltySkill;
        NeutralizeSkill = neutralizeSkill;
    }
}