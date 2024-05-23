using Fire_Emblem.Skills;

namespace Fire_Emblem.TeamsLoaderFiles;

public static class SkillLoader {
    
    public static IBaseSkill[] GetSkills(string unit) {
        if (AreThereSkills(unit)) {
            var unitSkillNames = GetUnitSkillNames(unit);
            var loadedSkills = SkillAssigner.AssignSkills(unitSkillNames);
            return loadedSkills.ToArray();
        }
        return Array.Empty<IBaseSkill>();
    }
    
    private static string[] GetUnitSkillNames(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
    }
    
    private static bool AreThereSkills(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1;
    }
}