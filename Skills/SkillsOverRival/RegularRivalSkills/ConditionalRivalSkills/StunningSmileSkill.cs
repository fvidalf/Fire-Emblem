using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

public class StunningSmileSkill: ConditionalRivalSkill {
    public StunningSmileSkill()
        : base("Stunning Smile", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Spd, -8)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalMale = Character.Gender == "Male";
        return isRivalMale;
    }
}