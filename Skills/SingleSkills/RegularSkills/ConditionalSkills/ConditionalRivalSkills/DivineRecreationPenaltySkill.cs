using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class DivineRecreationPenaltySkill: ConditionalRivalSkill {
    
    public DivineRecreationPenaltySkill()
        : base("Divine Recreation: Penalty", new Dictionary<EffectType, List<StatEffect>> {
                {
                    EffectType.RegularPenalty, new List<StatEffect> {
                        new StatEffect(Stat.Atk, -4),
                        new StatEffect(Stat.Spd, -4),
                        new StatEffect(Stat.Def, -4),
                        new StatEffect(Stat.Res, -4)
                    }
                }
            }
        ) {}

    protected override bool IsConditionMet() {
        var isRivalAtOrAboveHalfHp = RoundStatus.RivalCharacterModel.Hp >= RoundStatus.RivalCharacterModel.BaseHp / 2;
        return isRivalAtOrAboveHalfHp;
    }
}