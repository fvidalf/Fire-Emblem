namespace Fire_Emblem.TeamsLoaderFiles;

public static class TeamVerifier {
    
    public static void VerifyTeams(TeamsContent teamsContent) {
         foreach (var team in teamsContent.GetTeamsContent()) {
             CheckValidTeamLength(team);
             CheckValidTeamUnits(team);
         }
    }
         
    private static void CheckValidTeamLength(string[] team) {
        if (team.Length is 0 or > 3) {
             throw new InvalidTeamException();
        }
    }
     
    private static void CheckValidTeamUnits(string[] team) {
        var unitNames = new List<string>();

        foreach (var unit in team) {
            var unitName = GetUnitName(unit);

            CheckUnitAlreadyInTeam(unitName, unitNames);
            unitNames.Add(unitName);
             
            CheckUnitSkills(unit);
        }
    }
     
    private static string GetUnitName(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1 ? unit[..firstWhitespace] : unit[..];
    }

    private static void CheckUnitAlreadyInTeam(string unit, List<string> unitNames) {
        if (unitNames.Contains(unit)) {
            throw new InvalidTeamException();
        }
    }

    private static bool AreThereSkills(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1;
    }

    private static void CheckUnitSkills(string unit) {
        if (AreThereSkills(unit)) { 
            CheckUnitSkillsAreValid(unit);
        }
    }
    
    private static void CheckUnitSkillsAreValid(string unit) {
        var skillNames = new List<string>();
        var unitSkills = GetUnitSkillNames(unit);
        
        foreach (var skill in unitSkills) {
            CheckSkillAlreadyInUnit(skill, skillNames);
            skillNames.Add(skill);
        }

        CheckValidSkillCount(unitSkills);
    }
    
    private static string[] GetUnitSkillNames(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
    }
    
    private static void CheckSkillAlreadyInUnit(string skill, List<string> skillNames) {
        if (skillNames.Contains(skill)) {
            throw new InvalidTeamException();
        }
    }
    
    private static void CheckValidSkillCount(string[] unitSkills) {
        if (unitSkills.Length > 2) {
            throw new InvalidTeamException();
        }
    }
}