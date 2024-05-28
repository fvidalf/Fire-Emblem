using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class NotQuiteSkill: ConditionalRivalSkill {
    public NotQuiteSkill()
        : base("Not *Quite*", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -4)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = Character == RoundStatus.FirstCharacterModel;
        return isRivalFirstToAttack;
    }
}