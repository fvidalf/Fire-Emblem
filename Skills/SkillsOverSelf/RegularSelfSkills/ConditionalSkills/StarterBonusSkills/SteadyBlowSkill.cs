using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSkills.StarterBonusSkills;

public class SteadyBlowSkill: StarterSkill {
    
    public SteadyBlowSkill()
        : base(
            "Steady Blow",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 6),
                    new StatEffect(Stat.Def, 6)
                }}
            }) {}
}