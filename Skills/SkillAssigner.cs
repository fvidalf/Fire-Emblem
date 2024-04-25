using Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;
using Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.FlatBonusSkills;

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
        { "Brazen Def/Res", typeof(BrazenDefenseAndResistanceSkill)}
    };
}