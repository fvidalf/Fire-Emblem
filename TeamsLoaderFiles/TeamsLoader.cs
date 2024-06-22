using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using System.Text.Json;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;

namespace Fire_Emblem.TeamsLoaderFiles;

public class TeamsLoader {

    private View _view;
    private readonly TeamFilesReader _teamFilesReader;

    public TeamsLoader(View view, string teamsFolder) {
        _view = view;
        _teamFilesReader = new TeamFilesReader(teamsFolder);
    }
    
    public Team[] GetTeams() {
        var teamsContent = GetTeamsContent();
        TeamVerifier.VerifyTeams(teamsContent);
        var charactersInfo = GetCharactersFromJson();
        return GetTeamsFromInfo(teamsContent, charactersInfo);
    }
    
    private TeamsContent GetTeamsContent() {
        var userOption = AskForTeam();
        var lines = _teamFilesReader.ReadTeamFile(userOption);
        return new TeamsContent(lines);
    }

    private int AskForTeam() {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        ShowTeamFiles();
        var userOption = GetUserOption();
        return userOption;
    }
    
    private void ShowTeamFiles() {
        var fileNames = _teamFilesReader.GetAllFileNames();
        for (var i = 0; i < fileNames.Length; i++) {
            _view.WriteLine($"{i}: {fileNames[i]}");
        }
    }

    private int GetUserOption() {
        string? userString;
        do {
            userString = _view.ReadLine();
        } while (!int.TryParse(userString, out var _));
        return int.Parse(userString);
    }
    
    private List<CharacterInfo> GetCharactersFromJson() {
        var myJson = File.ReadAllText("characters.json");
        var charactersInfo = JsonSerializer.Deserialize<List<CharacterInfo>>(myJson) ??
                         throw new InvalidTeamException();
        return charactersInfo;
    }

    private Team[] GetTeamsFromInfo(TeamsContent teamsContent, List<CharacterInfo> charactersInfo) {
        var teamCharacters = new Team[2];
        for (var teamIndex = 0; teamIndex < 2; teamIndex++) {
            var newTeam = GetSingleTeamFromInfo(teamsContent, charactersInfo, teamIndex);
            teamCharacters[teamIndex] = newTeam;
        }
        return teamCharacters;
    }
    
    private Team GetSingleTeamFromInfo(TeamsContent teamsContent, List<CharacterInfo> charactersInfo, int teamIndex) {
        var numberOfUnits = teamsContent.GetNumberOfUnitsOfTeam(teamIndex);
        var team = new Team();
        for (var unitIndex = 0; unitIndex < numberOfUnits; unitIndex++) {
            var currentCharacter = GetCharacterFromInfo(teamsContent, charactersInfo, teamIndex, unitIndex);
            team.AddCharacter(currentCharacter);
        }
        return team;
    }
    
    private CharacterModel GetCharacterFromInfo(TeamsContent teamsContent, List<CharacterInfo> charactersInfo, int teamIndex, int unitIndex) {
        var unitName = teamsContent.GetUnitName(teamIndex, unitIndex);
        var unitSkillNames = teamsContent.GetUnitSkillNames(teamIndex, unitIndex);
        var unitSkills = SkillLoader.GetSkills(unitSkillNames);
        var character = CharacterLoader.CreateCharacterFromInfo(charactersInfo, unitName, unitSkills, _view);
        return character;
    }
}