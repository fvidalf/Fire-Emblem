using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public abstract class BrazenSkill: ConditionalBonusSkill {
    
    public BrazenSkill(string name, Dictionary<Stat, int> statsToModify)
        : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isHpAtOrBelow80 = Character.Hp <= Character.BaseHp * 0.8;
        return GameStatus.RoundPhase == 0 && isHpAtOrBelow80;
    }
}