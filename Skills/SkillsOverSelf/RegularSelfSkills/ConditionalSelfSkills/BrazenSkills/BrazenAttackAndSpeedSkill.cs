using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BrazenSkills;

public class BrazenAttackAndSpeedSkill: BrazenSkill {
    
    public BrazenAttackAndSpeedSkill() 
        :base (
            "Brazen Atk/Spd",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 10),
                    new StatEffect(Stat.Spd, 10)
                }}
            }) {}
}