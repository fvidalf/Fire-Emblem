using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public abstract class ConditionalRivalSkill: ConditionalSkill {
    
    protected ConditionalRivalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}
    
    public override void DetermineTarget() {
        Character = RoundStatus.RivalCharacterModel;
    }
}