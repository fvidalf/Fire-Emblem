using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

public class DisarmingSighSkill: ConditionalRivalSkill {
    public DisarmingSighSkill()
        : base("Disarming Sigh", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -8)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalMale = Character.Gender == "Male";
        return isRivalMale;
    }
}