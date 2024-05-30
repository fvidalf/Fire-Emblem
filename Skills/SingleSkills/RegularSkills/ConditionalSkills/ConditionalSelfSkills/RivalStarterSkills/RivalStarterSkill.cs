using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills;

public abstract class RivalStarterSkill: ConditionalSelfSkill {
    
    public RivalStarterSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
    : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isSpecificConditionMet = IsSpecificConditionMet();
        return RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel && isSpecificConditionMet;
    }

    protected virtual bool IsSpecificConditionMet() {
        return true;
    }
}