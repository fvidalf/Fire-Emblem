using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.FlatRivalSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

public class SpeedAndDefencePenaltySkill: FlatRivalSkill {
    public SpeedAndDefencePenaltySkill()
        : base("Spd/Def Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Spd, -3),
                    new StatEffect(Stat.Def, -3)
                }
            }
        }) {
    }
}