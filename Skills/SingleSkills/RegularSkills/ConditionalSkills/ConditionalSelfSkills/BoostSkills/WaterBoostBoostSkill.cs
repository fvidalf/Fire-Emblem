using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BoostSkills;

public class WaterBoostBoostSkill: BoostSkill {
            
    public WaterBoostBoostSkill()
        : base(
            "Water Boost", 
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Res, 6)
                }}
            }) {}
}