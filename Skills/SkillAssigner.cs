using Fire_Emblem.Skills.MultiCharacterSkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.FlatDamageModifierSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.FirstAttackSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.FollowUpSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BoostSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BrazenSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterBonusSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.WeaponSkills.WeaponAgilitySkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.WeaponSkills.WeaponFocusSkill;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.WeaponSkills.WeaponPowerSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills.SacrificeSkills;

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
        { "Still Water", typeof(StillWaterSkill)},
        { "Sword Agility", typeof(SwordSelfAgilitySkill)},
        { "Sword Focus", typeof(SwordSelfFocusSkill)},
        { "Sword Power", typeof(SwordSelfPowerSkill)},
        { "Lance Power", typeof(LanceSelfPowerSkill)},
        { "Lance Agility", typeof(LanceSelfAgilitySkill)},
        { "Axe Power", typeof(AxeSelfPowerSkill)},
        { "Bow Agility", typeof(BowSelfAgilitySkill)},
        { "Bow Focus", typeof(BowSelfFocusSkill)},
        { "Agnea's Arrow", typeof(AgneasArrowSkill)},
        { "Beorc's Blessing", typeof(BeorcsBlessingSkill)},
        { "Blinding Flash", typeof(BlindingFlashSkill)},
        { "Charmer", typeof(CharmerSkill)},
        { "Stunning Smile", typeof(StunningSmileSkill)},
        { "Disarming Sigh", typeof(DisarmingSighSkill)},
        { "Not *Quite*", typeof(NotQuiteSkill)},
        { "Luna", typeof(LunaSkill)},
        { "Belief in Love", typeof(BeliefInLoveSkill)},
        { "HP +15", typeof(HpSkill)},
        { "Lull Atk/Spd", typeof(LullAttackAndSpeedSkill)},
        { "Lull Atk/Def", typeof(LullAttackAndDefenceSkill)},
        { "Lull Atk/Res", typeof(LullAttackAndResistanceSkill)},
        { "Lull Spd/Def", typeof(LullSpeedAndDefenceSkill)},
        { "Lull Spd/Res", typeof(LullSpeedAndResistanceSkill)},
        { "Lull Def/Res", typeof(LullDefenceAndResistanceSkill)},
        { "Dragonskin", typeof(DragonskinSkill)},
        { "Soulblade", typeof(SoulbladeSkill)},
        { "Light and Dark", typeof(LightAndDarkSkill)},
        { "Fair Fight", typeof(FairFightSkill)},
        { "Close Def", typeof(CloseDefSkill)},
        { "Distant Def", typeof(DistantDefSkill)},
        { "Sandstorm", typeof(SandstormSkill)},
        { "Bravery", typeof(BraverySkill)},
        { "Gentility", typeof(GentilitySkill)},
        { "Bow Guard", typeof(BowGuardSkill)},
        { "Axe Guard", typeof(AxeGuardSkill)},
        { "Magic Guard", typeof(MagicGuardSkill)},
        { "Lance Guard", typeof(LanceGuardSkill)},
        { "Arms Shield", typeof(ArmsShieldSkill)},
        { "Sympathetic", typeof(SympatheticSkill)},
        { "Lunar Brace", typeof(LunarBraceSkill)},
        { "Bushido", typeof(BushidoSkill)},
        { "Dodge", typeof(DodgeSkill)}
    };
}