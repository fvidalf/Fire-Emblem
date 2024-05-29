using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BoostSkills;

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