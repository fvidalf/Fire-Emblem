using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

public class GuardBearingPenaltySkill: FlatRivalSkill {
    
    public GuardBearingPenaltySkill()
        : base("Guard Bearing Penalty", new Dictionary<EffectType, List<StatEffect>> {
                {
                    EffectType.RegularPenalty, new List<StatEffect> {
                        new StatEffect(Stat.Spd, -4),
                        new StatEffect(Stat.Def, -4)
                    }
                }
            }
        ) {}
}