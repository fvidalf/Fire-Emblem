using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills;

public class SwiftStrikeSkill: StarterSkill {
    
    public SwiftStrikeSkill()
        : base(
            "Swift Strike",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 6),
                    new StatEffect(Stat.Res, 6)
                }}
            }) {}
}