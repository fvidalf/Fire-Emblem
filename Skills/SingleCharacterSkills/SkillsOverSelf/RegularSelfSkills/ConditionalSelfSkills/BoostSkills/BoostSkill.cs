using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BoostSkills;

public abstract class BoostSkill: ConditionalSelfSkill {
    
    public BoostSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isHpGreaterThanRivalsBy3 = Character.Hp >= GameStatus.RivalCharacterModel.Hp + 3;
        return isHpGreaterThanRivalsBy3;
    }
}