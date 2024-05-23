using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using System.Text.Json;
using Fire_Emblem.Skills;

namespace Fire_Emblem.TeamsLoaderFiles;

public class TeamsLoader {

    private View _view;
    private readonly string _teamsFolder;
    private TeamsContent _teamsContent;

    public TeamsLoader(View view, string teamsFolder) {
        _view = view;
        _teamsFolder = teamsFolder;
        _teamsContent = new TeamsContent();
    }
    
    public Character[][] GetTeams() {
        var userOption = AskForTeam();
        ReadTeamFile(userOption);
        TeamVerifier.VerifyTeams(_teamsContent);
        return LoadTeams();
    }

    private int AskForTeam() {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        ShowTeamFiles();

        var userOption = GetUserOption();
        return userOption;
    }
    
    private void ShowTeamFiles() {
        var files = Directory.GetFiles(_teamsFolder);
        for (var i = 0; i < files.Length; i++) {
            _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
        }
    }

    private int GetUserOption() {
        string? userString;
        do {
            userString = _view.ReadLine();
        } while (!int.TryParse(userString, out var _));
        
        return int.Parse(userString);
    }
    
    private void ReadTeamFile(int option) {
        var file = GetFile(option);
        var lines = GetFileLines(file);
        _teamsContent.FillFromLines(lines);
    }

    private string GetFile(int option) {
        var files = Directory.GetFiles(_teamsFolder);
        return files[option];
    }

    private string[] GetFileLines(string file) {
        var fileContent = File.ReadAllText(file);
        var lines = fileContent.Trim().Split("\n");
        return lines;
    }

    private Character[][] LoadTeams() {
        var charactersInfo = GetCharactersFromJson();
        var teamCharacters = GetTeamCharacters(charactersInfo);
        return teamCharacters;
    }
    
    private List<CharacterInfo> GetCharactersFromJson() {
        var myJson = File.ReadAllText("characters.json");
        var charactersInfo = JsonSerializer.Deserialize<List<CharacterInfo>>(myJson) ??
                         throw new InvalidTeamException();
        return charactersInfo;
    }

    private Character[][] GetTeamCharacters(List<CharacterInfo> charactersInfo) {
        var teamCharacters = InitializeTeams();
        
        for (var teamIndex = 0; teamIndex < 2; teamIndex++) {
            for (var unitIndex = 0; unitIndex < _teamsContent.GetNumberOfUnitsOfTeam(teamIndex); unitIndex++) {
                teamCharacters[teamIndex][unitIndex] = GetCharacterFromInfo(charactersInfo, teamIndex, unitIndex);
            }
        }
        return teamCharacters;
    }
    
    private Character GetCharacterFromInfo(List<CharacterInfo> charactersInfo, int teamIndex, int unitIndex) {
        var unitName = _teamsContent.GetUnitName(teamIndex, unitIndex);
        var unit = _teamsContent.GetTeamsContent()[teamIndex][unitIndex];
        var unitSkills = SkillLoader.GetSkills(unit);
        var character = CharacterLoader.CreateCharacterFromInfo(charactersInfo, unitName, unitSkills, _view);
        return character;
    }

    private Character[][] InitializeTeams() {
        var teamCharacters = new Character[2][];
        teamCharacters[0] = new Character[_teamsContent.GetNumberOfUnitsOfTeam(0)];
        teamCharacters[1] = new Character[_teamsContent.GetNumberOfUnitsOfTeam(1)];
        return teamCharacters;
    }
}