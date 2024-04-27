using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.FlatRivalSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

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