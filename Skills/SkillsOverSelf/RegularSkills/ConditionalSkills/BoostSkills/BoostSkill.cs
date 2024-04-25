using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.BoostSkills;

public abstract class BoostSkill: ConditionalSkill {
    
    public BoostSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isHpGreaterThanRivalsBy3 = Character.Hp >= GameStatus.RivalCharacter.Hp + 3;
        return isHpGreaterThanRivalsBy3;
    }
}