using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BrazenSkills;

public class BrazenSpeedAndDefenseSkill: BrazenSkill {
    
    public BrazenSpeedAndDefenseSkill() 
        :base (
            "Brazen Spd/Def",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 10),
                    new StatEffect(Stat.Def, 10)
                }}
            }) {}
}