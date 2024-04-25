using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class ChaosStyleSkill: StarterBonusSkill {
    
    public ChaosStyleSkill()
        : base(
            "Chaos Style",
            new Dictionary<Stat, int> { { Stat.Spd, 3 }, }) { }

    protected override bool IsSpecificConditionMet() {
        var scenarioOne = Character.IsPhysical() && !GameStatus.RivalCharacter.IsPhysical();
        var scenarioTwo = !Character.IsPhysical() && GameStatus.RivalCharacter.IsPhysical();
        return scenarioOne || scenarioTwo;
    }
}
