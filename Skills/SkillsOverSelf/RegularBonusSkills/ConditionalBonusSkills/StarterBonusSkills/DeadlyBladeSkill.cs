using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class DeadlyBladeSkill: StarterBonusSkill {

    public DeadlyBladeSkill()
        : base(
            "Deadly Blade",
            new Dictionary<Stat, int> { { Stat.Atk, 8 }, { Stat.Spd, 8 }, }) { }

    protected override bool IsSpecificConditionMet() {
        return Character.Weapon == "Sword";
    }
}