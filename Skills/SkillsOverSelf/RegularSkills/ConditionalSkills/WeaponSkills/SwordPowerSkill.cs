using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.BrazenSkills;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.WeaponSkills;

public class SwordPowerSkill : WeaponSkill {

    public SwordPowerSkill()
        : base(
            "Sword Power",
            new Dictionary<EffectType, List<StatEffect>>(),
            "Sword") {
        
        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 10),
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Def, -10),
        };
    }
}