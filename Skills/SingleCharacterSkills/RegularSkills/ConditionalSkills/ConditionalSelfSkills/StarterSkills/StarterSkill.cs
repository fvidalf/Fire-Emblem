using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills;

public abstract class StarterSkill: ConditionalSelfSkill {
    
    public StarterSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
    : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isSpecificConditionMet = IsSpecificConditionMet();
        return Character == RoundStatus.FirstCharacterModel && isSpecificConditionMet;
    }

    protected virtual bool IsSpecificConditionMet() {
        return true;
    }
}