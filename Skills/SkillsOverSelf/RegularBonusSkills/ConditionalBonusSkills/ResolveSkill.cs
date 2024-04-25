using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

public class ResolveSkill: ConditionalBonusSkill {

    public ResolveSkill()
        : base(
            "Resolve",
            new Dictionary<Stat, int> { { Stat.Def, 7 }, { Stat.Res, 7 }, }) { }

    protected override bool IsConditionMet() {
        var isGameStart = GameStatus.RoundPhase == 0;
        var isHpAtOrBelow75 = Character.Hp <= Character.BaseHp * 0.75;
        return isGameStart && isHpAtOrBelow75;
    }
}