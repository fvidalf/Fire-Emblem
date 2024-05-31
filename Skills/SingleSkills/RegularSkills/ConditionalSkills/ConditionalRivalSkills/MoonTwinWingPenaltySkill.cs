using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class MoonTwinWingPenaltySkill: ConditionalRivalSkill{
    public MoonTwinWingPenaltySkill()
        : base("Moon-Twin Wing: Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -5),
                new StatEffect(Stat.Spd, -5)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var unit = RoundStatus.ActivatingCharacterModel;
        var isUnitAtOrAboveQuarterHp = unit.Hp >= unit.BaseHp / 4;
        return isUnitAtOrAboveQuarterHp;
    }
}