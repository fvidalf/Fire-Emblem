﻿
using Fire_Emblem.Skills.SkillsOverSelf.FirstAttackBonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.BoostSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.BrazenSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.StarterBonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.WeaponSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills.FlatBonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills.SacrificeSkills;

namespace Fire_Emblem.Skills;

public static class SkillAssigner {

    public static List<IBaseSkill> AssignSkills(string[] skillNames) {
        var skills = new List<IBaseSkill>();
        foreach (var skillName in skillNames) {
            if (Map.TryGetValue(skillName, out var skillType)) {
                var skill = (IBaseSkill) Activator.CreateInstance(skillType);
                skills.Add(skill);
            }
        }
        return skills;
    }
    
    private static readonly Dictionary<string, Type> Map = new Dictionary<string, Type> {
        { "Attack +6", typeof(AttackSkill) },
        { "Defense +5", typeof(DefenseSkill) },
        { "Speed +5", typeof(SpeedSkill)},
        { "Resistance +5", typeof(ResistanceSkill)},
        { "Atk/Def +5", typeof(AttackAndDefenseSkill)},
        { "Atk/Res +5", typeof(AttackAndResistanceSkill)},
        { "Spd/Res +5", typeof(SpeedAndResistanceSkill)},
        { "Wrath", typeof(WrathSkill)},
        { "Resolve", typeof(ResolveSkill)},
        { "Deadly Blade", typeof(DeadlyBladeSkill)},
        { "Death Blow", typeof(DeathBlowSkill)},
        { "Armored Blow", typeof(ArmoredBlowSkill)},
        { "Darting Blow", typeof(DartingBlowSkill)},
        { "Warding Blow", typeof(WardingBlowSkill) },
        { "Swift Sparrow", typeof(SwiftSparrowSkill) },
        { "Sturdy Blow", typeof(SturdyBlowSkill)},
        { "Mirror Strike", typeof(MirrorStrikeSkill)},
        { "Steady Blow", typeof(SteadyBlowSkill)},
        { "Swift Strike", typeof(SwiftStrikeSkill)},
        { "Bracing Blow", typeof(BracingBlowSkill)},
        { "Tome Precision", typeof(TomePrecisionSkill)},
        { "Perceptive", typeof(PerceptiveSkill)},
        { "Brazen Atk/Spd", typeof(BrazenAttackAndSpeedSkill)},
        { "Brazen Atk/Def", typeof(BrazenAttackAndDefenseSkill)},
        { "Brazen Atk/Res", typeof(BrazenAttackAndResistanceSkill)},
        { "Brazen Spd/Def", typeof(BrazenSpeedAndDefenseSkill)},
        { "Brazen Spd/Res", typeof(BrazenSpeedAndResistanceSkill)},
        { "Brazen Def/Res", typeof(BrazenDefenseAndResistanceSkill)},
        { "Chaos Style", typeof(ChaosStyleSkill)},
        { "Fire Boost", typeof(FireBoostBoostSkill)},
        { "Wind Boost", typeof(WindBoostBoostSkill)},
        { "Earth Boost", typeof(EarthBoostBoostSkill)}, 
        { "Water Boost", typeof(WaterBoostBoostSkill)},
        { "Will to Win", typeof(WillToWinSkill)},
        { "Ignis", typeof(IgnisSkill)},
        { "Single-Minded", typeof(SingleMindedSkill)},
        { "Fort. Def/Res", typeof(FortifyDefenseAndResistanceSkill)},
        { "Life and Death", typeof(LifeAndDeathSkill)},
        { "Solid Ground", typeof(SolidGroundSkill)},
        { "Sword Agility", typeof(SwordAgilitySkill)},
        { "Sword Focus", typeof(SwordFocusSkill)},
        { "Sword Power", typeof(SwordPowerSkill)},
        { "Still Water", typeof(StillWaterSkill)}
    };
}