using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class SingleMindedSkill: ConditionalSelfSkill {

    public SingleMindedSkill()
        : base(
            "Single-Minded",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 8)
                }}
            }) {}

    protected override bool IsConditionMet() {
        var isRivalMostRecentOpponent = Character.MostRecentRival == RoundStatus.RivalCharacterModel;
        return isRivalMostRecentOpponent;
    }
}