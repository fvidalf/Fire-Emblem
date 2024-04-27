using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.WeaponSkills.WeaponAgilitySkills;

public abstract class WeaponSelfAgilitySkill : WeaponSelfSkill {

    public WeaponSelfAgilitySkill(string name, string weapon)
        : base(
            name,
            new Dictionary<EffectType, List<StatEffect>>(),
            weapon) {
        
        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Spd, 12),
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Atk, -6),
        };
    }
}