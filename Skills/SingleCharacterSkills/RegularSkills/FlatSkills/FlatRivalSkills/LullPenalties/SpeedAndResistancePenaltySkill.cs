using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

public class SpeedAndResistancePenaltySkill: FlatRivalSkill {
    public SpeedAndResistancePenaltySkill()
        : base("Spd/Res Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Spd, -3),
                    new StatEffect(Stat.Res, -3)
                }
            }
        }) {
    }
}