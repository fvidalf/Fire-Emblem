using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BonusSkills;

public abstract class BonusSkill: ConditionalBonusSkill {
    
    public BonusSkill(string name, Dictionary<Stat, int> statsToModify)
        : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isHpGreaterThanRivalsBy3 = Character.Hp >= GameStatus.RivalCharacter.Hp + 3;
        return GameStatus.RoundPhase == 0 && isHpGreaterThanRivalsBy3;
    }
}