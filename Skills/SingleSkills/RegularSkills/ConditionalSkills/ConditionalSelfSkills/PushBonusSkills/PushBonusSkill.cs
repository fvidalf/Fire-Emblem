using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.PushBonusSkills;

public class PushBonusSkill : ConditionalSelfSkill {
    
    public PushBonusSkill(Stat firstStat, Stat secondStat)
        : base("Push Bonus", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(firstStat, 7),
                new StatEffect(secondStat, 7)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isHpAtOrAbove25 = Character.Hp >= Character.BaseHp * 0.25;
        return isHpAtOrAbove25;
    }
}