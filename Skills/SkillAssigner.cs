using Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;
using Fire_Emblem.Skills.FixedAmountSkills.FixedAmountHybridSkills;

namespace Fire_Emblem.Skills;

public static class SkillAssigner {

    public static List<IBaseSkill> AssignSkills(string[] skillNames) {
        var skills = new List<IBaseSkill>();
        foreach (var skillName in skillNames) {
            if (Map.TryGetValue(skillName, out var skill)) {
                skills.Add(skill);
            }
        }
        return skills;
    }
    
    private static readonly Dictionary<string, IBaseSkill> Map = new Dictionary<string, IBaseSkill> {
        { "Attack +6", new AttackSkill() },
        { "Defense +5", new DefenseSkill() },
        { "Speed + 5", new SpeedSkill()},
        { "Resistance + 5", new ResistanceSkill()},
        { "Atk/Def +5", new AttackAndDefenseSkill()},
        { "Atk/Res +5", new AttackAndResistanceSkill()},
        { "Spd/Res +5", new SpeedAndResistanceSkill()},
        { "Fort. Def/Res", new FortifyDefenseAndResistanceSkill()},
        { "Life and Death", new LifeAndDeathSkill()},
        { "Solid Ground", new SolidGroundSkill()},
        { "Still Water", new StillWaterSkill()}
    };
}