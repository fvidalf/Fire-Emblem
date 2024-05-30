using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BoostSkills;

public class EarthBoostBoostSkill: BoostSkill {
        
    public EarthBoostBoostSkill()
        : base(
            "Earth Boost", 
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 6)
                }}
            }) {}
        
}