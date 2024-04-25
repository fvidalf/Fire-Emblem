using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

public class TomePrecisionSkill: ConditionalBonusSkill {
    
    public TomePrecisionSkill()
        : base(
            "Tome Precision", 
            new Dictionary<Stat, int> { { Stat.Atk, 6 }, { Stat.Spd, 6 }, }) { }

    protected override bool IsConditionMet() {
        return Character.Weapon == "Magic";
    }
}