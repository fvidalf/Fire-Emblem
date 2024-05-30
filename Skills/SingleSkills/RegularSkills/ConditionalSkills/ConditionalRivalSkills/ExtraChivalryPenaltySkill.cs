using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class ExtraChivalryPenaltySkill: ConditionalRivalSkill {
    
    public ExtraChivalryPenaltySkill()
        : base("Extra Chivalry Penalty", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -5),
                new StatEffect(Stat.Spd, -5),
                new StatEffect(Stat.Def, -5)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalAtOrAboveHalfHp = Character.Hp >= Character.BaseHp / 2;
        return isRivalAtOrAboveHalfHp;
    }
}