using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class ResolveSkill: ConditionalSelfSkill {

    public ResolveSkill()
        : base(
            "Resolve",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 7),
                    new StatEffect(Stat.Res, 7)
                }}
            }) {}

    protected override bool IsConditionMet() {
        var isGameStart = RoundStatus.RoundPhase == 0;
        var isHpAtOrBelow75 = Character.Hp <= Character.BaseHp * 0.75;
        return isGameStart && isHpAtOrBelow75;
    }
}