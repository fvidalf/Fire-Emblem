using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills;

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
        var scenarioOne = Character.IsPhysical() && !RoundStatus.RivalCharacterModel.IsPhysical();
        var scenarioTwo = !Character.IsPhysical() && RoundStatus.RivalCharacterModel.IsPhysical();
        return scenarioOne || scenarioTwo;
    }
}
