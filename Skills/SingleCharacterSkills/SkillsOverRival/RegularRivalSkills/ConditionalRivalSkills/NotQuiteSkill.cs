using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

public class NotQuiteSkill: ConditionalRivalSkill {
    public NotQuiteSkill()
        : base("Not *Quite*", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -4)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = Character == GameStatus.FirstCharacter;
        return isRivalFirstToAttack;
    }
}