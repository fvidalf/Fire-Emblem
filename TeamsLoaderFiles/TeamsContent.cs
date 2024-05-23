namespace Fire_Emblem.TeamsLoaderFiles;

public class TeamsContent {
    private string[][] _teamsContent = new string[2][];

    public TeamsContent() {
        // FillFromLines(lines);
    }
    
    public void FillFromLines(string[] lines) {
        InitializeTeamsContent(lines);
        FillTeamsContent(lines);
    }
    
    public string[][] GetTeamsContent() {
        return _teamsContent;
    }
    
    public int GetNumberOfUnitsOfTeam(int teamIndex) {
        return _teamsContent[teamIndex].Length;
    }
    
    public string GetUnitName(int teamIndex, int unitIndex) {
        var unit = _teamsContent[teamIndex][unitIndex];
        var unitName = GetUnitNameFromUnit(unit);
        return unitName;
    }
    
    public string[] GetUnitSkillNames(int teamIndex, int unitIndex) {
        var unit = _teamsContent[teamIndex][unitIndex];
        var unitSkills = GetUnitSkillNamesFromUnit(unit);
        return unitSkills;
    }
    
    private string GetUnitNameFromUnit(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1 ? unit[..firstWhitespace] : unit[..];
    }
    
    private string[] GetUnitSkillNamesFromUnit(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
    }
    
    private void InitializeTeamsContent(string[] lines) {
        var currentTeam = 0;
        var teamUnits = 0;
        foreach (var line in lines) {
            if (line.Contains("Player 2")) {
                _teamsContent[currentTeam] = new string[teamUnits];
                currentTeam++;
                teamUnits = 0;
            } else if (!line.Contains("Player 1")) {
                teamUnits++;
            }
        }
        _teamsContent[currentTeam] = new string[teamUnits];
    }
    
    private void FillTeamsContent(string[] lines) {
        var currentTeam = 0;
        var unitIndex = 0;
        foreach (var line in lines) {
            if (line.Contains("Player 2")) {
                currentTeam++;
                unitIndex = 0;
            } else if (!line.Contains("Player 1")) {
                _teamsContent[currentTeam][unitIndex] = line;
                unitIndex++;
            }
        }
    }
}