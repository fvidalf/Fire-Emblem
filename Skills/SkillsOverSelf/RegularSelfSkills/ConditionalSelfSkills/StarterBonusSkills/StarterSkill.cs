using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.StarterBonusSkills;

public abstract class StarterSkill: ConditionalSelfSkill {
    
    public StarterSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
    : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isSpecificConditionMet = IsSpecificConditionMet();
        return Character == GameStatus.FirstCharacter && isSpecificConditionMet;
    }

    protected virtual bool IsSpecificConditionMet() {
        return true;
    }
}