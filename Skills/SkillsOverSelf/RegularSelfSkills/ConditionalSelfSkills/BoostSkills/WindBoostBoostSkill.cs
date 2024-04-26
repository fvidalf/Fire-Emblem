using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BoostSkills;

public class WindBoostBoostSkill: BoostSkill {
        
    public WindBoostBoostSkill()
        : base(
            "Wind Boost", 
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 6)
                }}
            }) {}
        
}