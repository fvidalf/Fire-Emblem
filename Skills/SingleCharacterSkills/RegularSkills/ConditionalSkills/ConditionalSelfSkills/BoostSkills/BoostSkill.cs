using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BoostSkills;

public abstract class BoostSkill: ConditionalSelfSkill {
    
    public BoostSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isHpGreaterThanRivalsBy3 = Character.Hp >= RoundStatus.RivalCharacterModel.Hp + 3;
        return isHpGreaterThanRivalsBy3;
    }
}