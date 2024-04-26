using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.WeaponSkills.WeaponFocusSkill;

public class WeaponSelfFocusSkill: WeaponSelfSkill {

    public WeaponSelfFocusSkill(string name, string weapon)
        : base(
            name,
            new Dictionary<EffectType, List<StatEffect>>(),
            weapon) {
        
        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 10),
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Res, -10),
        };
    }
}