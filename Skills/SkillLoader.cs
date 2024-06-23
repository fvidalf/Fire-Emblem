using Fire_Emblem.Skills.SkillCollections;
namespace Fire_Emblem.Skills;

public static class SkillLoader {
    
    public static BaseSkillCollection GetSkills(string[] unitSkillNames) {
        var skills = new List<IBaseSkill>();
        if (unitSkillNames.Length > 0) {
            skills = SkillAssigner.AssignSkills(unitSkillNames);
        }
        return new BaseSkillCollection(skills.ToArray());
    }
}