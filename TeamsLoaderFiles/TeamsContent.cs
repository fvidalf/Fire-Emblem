namespace Fire_Emblem.TeamsLoaderFiles;

public class TeamsContent {
    private string[][] _teams = new string[2][];

    public TeamsContent(string[] lines) {
        FillFromLines(lines);
    }
    
    private void FillFromLines(string[] lines) {
        InitializeTeamsContent(lines);
        FillTeamsContent(lines);
    }
    
    private void InitializeTeamsContent(string[] lines) {
        var currentTeam = 0;
        var teamUnits = 0;
        foreach (var line in lines) {
            if (line.Contains("Player 2")) {
                _teams[currentTeam] = new string[teamUnits];
                currentTeam++;
                teamUnits = 0;
            } else if (!line.Contains("Player 1")) {
                teamUnits++;
            }
        }
        _teams[currentTeam] = new string[teamUnits];
    }
    
    private void FillTeamsContent(string[] lines) {
        var currentTeam = 0;
        var unitIndex = 0;
        foreach (var line in lines) {
            if (line.Contains("Player 2")) {
                currentTeam++;
                unitIndex = 0;
            } else if (!line.Contains("Player 1")) {
                _teams[currentTeam][unitIndex] = line;
                unitIndex++;
            }
        }
    }
    
    public string[][] GetTeams() {
        return _teams;
    }
    
    public int GetNumberOfUnitsOfTeam(int teamIndex) {
        return _teams[teamIndex].Length;
    }
    
    public string GetUnitName(int teamIndex, int unitIndex) {
        var unit = _teams[teamIndex][unitIndex];
        var unitName = GetUnitNameFromUnit(unit);
        return unitName;
    }
    
    private string GetUnitNameFromUnit(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1 ? unit[..firstWhitespace] : unit[..];
    }
    
    public string[] GetUnitSkillNames(int teamIndex, int unitIndex) {
        var unit = _teams[teamIndex][unitIndex];
        var unitSkillNames = Array.Empty<string>();
        if (AreThereSkills(unit)) {
            unitSkillNames = GetUnitSkillNamesFromUnit(unit);
        }
        return unitSkillNames;
    }
    
    private bool AreThereSkills(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1;
    }
    
    private string[] GetUnitSkillNamesFromUnit(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        var unitSkillNames = unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
        return unitSkillNames;
    }
}