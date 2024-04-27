using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.StarterBonusSkills;

public class ChaosStyleSkill: StarterSkill {
    
    public ChaosStyleSkill()
        : base(
            "Chaos Style",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 3)
                }}
            }) {}

    protected override bool IsSpecificConditionMet() {
        var scenarioOne = Character.IsPhysical() && !GameStatus.RivalCharacter.IsPhysical();
        var scenarioTwo = !Character.IsPhysical() && GameStatus.RivalCharacter.IsPhysical();
        return scenarioOne || scenarioTwo;
    }
}
