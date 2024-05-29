using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

public class DefenceAndResistancePenaltySkill: FlatRivalSkill {
    public DefenceAndResistancePenaltySkill()
        : base("Def/Res Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Def, -3),
                    new StatEffect(Stat.Res, -3)
                }
            }
        }) {
    }
}