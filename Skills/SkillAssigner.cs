using Fire_Emblem.Skills.SkillsOverSelf.FlatBonusSkills;

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
        { "Speed + 5", typeof(SpeedSkill)},
        { "Resistance + 5", typeof(ResistanceSkill)},
        { "Atk/Def +5", typeof(AttackAndDefenseSkill)},
        { "Atk/Res +5", typeof(AttackAndResistanceSkill)},
        { "Spd/Res +5", typeof(SpeedAndResistanceSkill)}
    };
}