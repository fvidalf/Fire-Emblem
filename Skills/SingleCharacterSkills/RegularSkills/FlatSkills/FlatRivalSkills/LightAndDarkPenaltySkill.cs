using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills;

public class LightAndDarkPenaltySkill: FlatRivalSkill {
    
    public LightAndDarkPenaltySkill()
        : base("Light and Dark Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Atk, -5),
                    new StatEffect(Stat.Spd, -5),
                    new StatEffect(Stat.Def, -5),
                    new StatEffect(Stat.Res, -5)
                }
            }
        }) {
    }
}