using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSkills;

public class ResolveSkill: ConditionalSkill {

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
        var isGameStart = GameStatus.RoundPhase == 0;
        var isHpAtOrBelow75 = Character.Hp <= Character.BaseHp * 0.75;
        return isGameStart && isHpAtOrBelow75;
    }
}