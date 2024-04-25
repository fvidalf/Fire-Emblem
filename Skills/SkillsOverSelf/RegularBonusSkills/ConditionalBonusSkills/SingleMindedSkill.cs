using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

public class SingleMindedSkill: ConditionalBonusSkill {

    public SingleMindedSkill()
        : base(
            "Single-Minded",
            new Dictionary<Stat, int> { { Stat.Atk, 8 }, }) { }

    protected override bool IsConditionMet() {
        var isRivalMostRecentOpponent = Character.MostRecentRival == GameStatus.RivalCharacter;
        return isRivalMostRecentOpponent;
    }
}