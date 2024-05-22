using Fire_Emblem.Skills;

namespace Fire_Emblem.TeamsLoaderFiles;

public static class SkillLoader {
    
    public static IBaseSkill[] GetSkills(string[] unitSkillNames) {
        if (AreThereSkills(unitSkillNames)) {
            var loadedSkills = SkillAssigner.AssignSkills(unitSkillNames);
            return loadedSkills.ToArray();
        }
        return Array.Empty<IBaseSkill>();
    }
    
    private static bool AreThereSkills(string[] unitSkillNames) {
        return unitSkillNames.Length > 1;
    }
}