using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

public class AttackAndResistancePenaltySkill: FlatRivalSkill {
    public AttackAndResistancePenaltySkill()
        : base("Atk/Res Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Atk, -3),
                    new StatEffect(Stat.Res, -3)
                }
            }
        }) {
    }
}