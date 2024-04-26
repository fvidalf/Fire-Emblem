using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BoostSkills;

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