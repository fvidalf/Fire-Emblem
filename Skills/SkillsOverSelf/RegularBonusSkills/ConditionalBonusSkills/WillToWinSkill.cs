using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

public class WillToWinSkill: ConditionalBonusSkill {
    
    public WillToWinSkill()
        : base(
            "Will to Win", 
            new Dictionary<Stat, int> { { Stat.Atk, 8 }, }) { }

    protected override bool IsConditionMet() {
        var isHpAtOrBelowHalf = Character.Hp <= Character.BaseHp / 2;
        return isHpAtOrBelowHalf;
    }
}