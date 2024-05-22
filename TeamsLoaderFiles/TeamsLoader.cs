using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using System.Text.Json;
using Fire_Emblem.Skills;

namespace Fire_Emblem.TeamsLoaderFiles;

public class TeamsLoader {

    private View _view;
    private readonly string _teamsFolder;
    private TeamsContent _teamsContent;
    public Character[][] TeamsCharacters = new Character[2][];

    public TeamsLoader(View view, string teamsFolder) {
        _view = view;
        _teamsFolder = teamsFolder;
        _teamsContent = new TeamsContent();
    }
    
    public void Execute() {
        var userOption = AskForTeam();
        ReadTeamFile(userOption);
        TeamVerifier.VerifyTeams(_teamsContent);
        TeamsCharacters = LoadTeams();
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
    
    private string[] GetUnitSkillNames(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
    }
    
    private string GetUnitName(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1 ? unit[..firstWhitespace] : unit[..];
    }
    
    private bool AreThereSkills(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1;
    }

    // REFACTORING NEEDED
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
        var unitSkills = GetSkills(unit);
        // var unitSkills = _teamsContent.GetUnitSkillsNames(teamIndex, unitIndex);
        var character = CreateCharacterFromInfo(charactersInfo, unitName, unitSkills);
        return character;
    }

    private Character[][] InitializeTeams() {
        var teamCharacters = new Character[2][];
        teamCharacters[0] = new Character[_teamsContent.GetNumberOfUnitsOfTeam(0)];
        teamCharacters[1] = new Character[_teamsContent.GetNumberOfUnitsOfTeam(1)];
        return teamCharacters;
    }

    private Character CreateCharacterFromInfo(List<CharacterInfo> charactersInfo, string unitName, IBaseSkill[] unitSkills) {
        var characterInfo = charactersInfo.Find(character => character.Name.Trim() == unitName.Trim());
        var character = new Character(
            characterInfo.Name,
            characterInfo.Weapon,
            characterInfo.Gender,
            characterInfo.DeathQuote,
            unitSkills,
            Convert.ToInt32(characterInfo.HP),
            Convert.ToInt32(characterInfo.Atk),
            Convert.ToInt32(characterInfo.Spd),
            Convert.ToInt32(characterInfo.Def),
            Convert.ToInt32(characterInfo.Res),
            _view
        );
        return character;
    }

    private IBaseSkill[] GetSkills(string unit) {
        if (AreThereSkills(unit)) {
            var unitSkillNames = GetUnitSkillNames(unit);
            var loadedSkills = SkillAssigner.AssignSkills(unitSkillNames);
            return loadedSkills.ToArray();
        }
        return Array.Empty<IBaseSkill>();
    }
}