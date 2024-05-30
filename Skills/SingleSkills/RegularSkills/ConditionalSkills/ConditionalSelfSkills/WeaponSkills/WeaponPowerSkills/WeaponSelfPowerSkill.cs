using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.WeaponSkills.WeaponPowerSkills;

public abstract class WeaponSelfPowerSkill : WeaponSelfSkill {

    public WeaponSelfPowerSkill(string name, string weapon)
        : base(
            name,
            new Dictionary<EffectType, List<StatEffect>>(),
            weapon) {
                
        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 10),
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Def, -10),
        };
    }
}