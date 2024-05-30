using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

public class DartingStanceBonusSkill: RivalStarterSkill {
    
    public DartingStanceBonusSkill()
        : base(
            "Darting Stance Bonus Skill",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 8)
                }}
            }) {}
}