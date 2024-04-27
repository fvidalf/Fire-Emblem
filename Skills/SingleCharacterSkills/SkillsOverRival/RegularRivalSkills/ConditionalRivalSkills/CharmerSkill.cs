using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

public class CharmerSkill: ConditionalRivalSkill {
    public CharmerSkill()
        : base("Charmer", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -3),
                new StatEffect(Stat.Spd, -3)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalMostRecentOpponent = GameStatus.ActivatingCharacter.MostRecentRival == Character;
        return isRivalMostRecentOpponent;
    }
}