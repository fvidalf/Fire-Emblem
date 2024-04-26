
using Fire_Emblem.Skills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;
using Fire_Emblem.Skills.SkillsOverSelf;
using Fire_Emblem.Skills.SkillsOverSelf.FirstAttackSelfSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BoostSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BrazenSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.StarterBonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.WeaponSkills.WeaponAgilitySkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.WeaponSkills.WeaponFocusSkill;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.WeaponSkills.WeaponPowerSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.BonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.SacrificeSkills;

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
        { "Attack +6", typeof(AttackSelfSkill) },
        { "Defense +5", typeof(DefenseSelfSkill) },
        { "Speed +5", typeof(SpeedSelfSkill)},
        { "Resistance +5", typeof(ResistanceSelfSkill)},
        { "Atk/Def +5", typeof(AttackAndDefenseSelfSkill)},
        { "Atk/Res +5", typeof(AttackAndResistanceSelfSkill)},
        { "Spd/Res +5", typeof(SpeedAndResistanceSelfSkill)},
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
        { "Fort. Def/Res", typeof(FortifyDefenseAndResistanceSelfSkill)},
        { "Life and Death", typeof(LifeAndDeathSelfSkill)},
        { "Solid Ground", typeof(SolidGroundSelfSkill)},
        { "Still Water", typeof(StillWaterSelfSkill)},
        { "Sword Agility", typeof(SwordSelfAgilitySkill)},
        { "Sword Focus", typeof(SwordSelfFocusSkill)},
        { "Sword Power", typeof(SwordSelfPowerSkill)},
        { "Lance Power", typeof(LanceSelfPowerSkill)},
        { "Lance Agility", typeof(LanceSelfAgilitySkill)},
        { "Axe Power", typeof(AxeSelfPowerSkill)},
        { "Bow Agility", typeof(BowSelfAgilitySkill)},
        { "Bow Focus", typeof(BowSelfFocusSkill)},
        { "Agnea's Arrow", typeof(AgneasArrowSkill)},
        { "Blinding Flash", typeof(BlindingFlashSkill)},
        { "Charmer", typeof(CharmerSkill)},
        { "Stunning Smile", typeof(StunningSmileSkill)},
        { "Disarming Sigh", typeof(DisarmingSighSkill)},
        { "Not *Quite*", typeof(NotQuiteSkill)},
        { "HP +15", typeof(HpSkill)}
    };
}