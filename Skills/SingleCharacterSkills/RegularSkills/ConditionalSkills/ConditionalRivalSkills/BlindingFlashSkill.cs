using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class BlindingFlashSkill: ConditionalRivalSkill {
    public BlindingFlashSkill()
        : base("Blinding Flash", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Spd, -4),
            }}
        }) {}

    protected override bool IsConditionMet() {
        var IsFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        return IsFirstToAttack;
    }
}