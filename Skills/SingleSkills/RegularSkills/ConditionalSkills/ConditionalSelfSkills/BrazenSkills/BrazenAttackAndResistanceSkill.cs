using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BrazenSkills;

public class BrazenAttackAndResistanceSkill: BrazenSkill {
    
    public BrazenAttackAndResistanceSkill() 
        :base (
            "Brazen Atk/Res",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 10),
                    new StatEffect(Stat.Res, 10)
                }}
            }) {}
}