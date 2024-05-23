namespace Fire_Emblem.Skills;

public static class SkillLoader {
    
    public static IBaseSkill[] GetSkills(string[] unitSkillNames) {
        var skills = new List<IBaseSkill>();
        if (unitSkillNames.Length > 0) {
            skills = SkillAssigner.AssignSkills(unitSkillNames);
        }
        return skills.ToArray();
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