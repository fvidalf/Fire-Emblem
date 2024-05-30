using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class CharmerSkill: ConditionalRivalSkill {
    public CharmerSkill()
        : base("Charmer", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -3),
                new StatEffect(Stat.Spd, -3)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalMostRecentOpponent = RoundStatus.ActivatingCharacterModel.MostRecentRival == Character;
        return isRivalMostRecentOpponent;
    }
}