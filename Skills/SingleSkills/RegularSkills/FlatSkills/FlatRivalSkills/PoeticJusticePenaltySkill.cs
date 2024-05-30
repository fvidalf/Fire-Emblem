using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

public class PoeticJusticePenaltySkill: FlatRivalSkill {
    
    public PoeticJusticePenaltySkill()
        : base("Poetic Justice Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Spd, -4)
                }
            }
        }
    ) {}
}