using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using System.Text.Json;
using Fire_Emblem.Skills;

namespace Fire_Emblem;

public class TeamsLoader {

    private View _view;
    private readonly string _teamsFolder;
    private string[][] _teamsContent = new string[2][];
    public Character[][] TeamsCharacters = new Character[2][];
    
    public TeamsLoader(View view, string teamsFolder) {
        _view = view;
        _teamsFolder = teamsFolder;
    }
    
    public void Execute() {
        var userOption = AskForTeam();
        ReadTeamFile(userOption);
        VerifyTeams();
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

    // REFACTORING NEEDED
    private void ReadTeamFile(int option) {

        var file = Directory.GetFiles(_teamsFolder)[option];
        var lines = File.ReadAllText(file).Trim().Split("\n");

        var currentTeam = 0;
        var teamUnits = 0;

        // Count units per team to initialize the arrays
        foreach (var line in lines) {
            // Console.WriteLine(line);
            if (line.Contains("Player 1")) {
                continue;
            }

            if (line.Contains("Player 2")) {
                _teamsContent[currentTeam] = new string[teamUnits];
                currentTeam++;
                teamUnits = 0;
            }
            else {
                teamUnits++;
            }
        }

        _teamsContent[currentTeam] = new string[teamUnits];

        // Fill the array with the units
        currentTeam = 0;
        var teamUnit = 0;
        foreach (var line in lines) {
            if (line.Contains("Player 1")) {
                continue;
            }

            if (line.Contains("Player 2")) {
                currentTeam++;
                teamUnit = 0;
            }
            else {
                _teamsContent[currentTeam][teamUnit] = line;
                teamUnit++;
            }
        }
    }

    private void VerifyTeams() {
        foreach (var team in _teamsContent) {
            CheckValidTeamLength(team);
            CheckValidTeamUnits(team);
        }
    }
    
    private void CheckValidTeamLength(string[] team) {
        if (team.Length is 0 or > 3) {
            throw new ArgumentException("Invalid team length");
        }
    }
    
    private void CheckValidTeamUnits(string[] team) {
        var unitNames = new List<string>();

        foreach (var unit in team) {
            var unitName = GetUnitName(unit);

            CheckUnitAlreadyInTeam(unitName, unitNames);
            unitNames.Add(unitName);
            
            CheckUnitSkills(unit);
        }
    }
    
    private string GetUnitName(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1 ? unit[..firstWhitespace] : unit[..];
    }

    private void CheckUnitAlreadyInTeam(string unit, List<string> unitNames) {
        if (unitNames.Contains(unit)) {
            throw new ArgumentException("Unit already in team");
        }
    }

    private bool AreThereSkills(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1;
    }

    private void CheckUnitSkills(string unit) {
        if (AreThereSkills(unit)) {
            CheckUnitSkillsAreValid(unit);
        }
    }
    
    private void CheckUnitSkillsAreValid(string unit) {
        var skillNames = new List<string>();
        var unitSkills = GetUnitSkillNames(unit);
        
        foreach (var skill in unitSkills) {
            CheckSkillAlreadyInUnit(skill, skillNames);
            skillNames.Add(skill);
        }

        CheckValidSkillCount(unitSkills);
    }
    
    private string[] GetUnitSkillNames(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
    }
    
    private void CheckSkillAlreadyInUnit(string skill, List<string> skillNames) {
        if (skillNames.Contains(skill)) {
            throw new ArgumentException("Skill already in unit");
        }
    }
    
    private void CheckValidSkillCount(string[] unitSkills) {
        if (unitSkills.Length > 2) {
            throw new ArgumentException("Unit has more than 2 skills");
        }
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
                         throw new ArgumentNullException("JsonSerializer.Deserialize<List<CharacterInfo>>(myJson)");
        return charactersInfo;
    }

    private Character[][] GetTeamCharacters(List<CharacterInfo> charactersInfo) {
        var teamCharacters = InitializeTeams();
        
        for (var teamIndex = 0; teamIndex < 2; teamIndex++) {
            for (var unitIndex = 0; unitIndex < _teamsContent[teamIndex].Length; unitIndex++) {
                teamCharacters[teamIndex][unitIndex] = GetCharacterFromInfo(charactersInfo, teamIndex, unitIndex);
            }
        }
        return teamCharacters;
    }
    
    private Character GetCharacterFromInfo(List<CharacterInfo> charactersInfo, int teamIndex, int unitIndex) {
        var unit = _teamsContent[teamIndex][unitIndex];
        var unitName = GetUnitName(unit);
        var unitSkills = GetSkills(unit);
        var character = CreateCharacterFromInfo(charactersInfo, unitName, unitSkills);
        return character;
    }

    private Character[][] InitializeTeams() {
        var teamCharacters = new Character[2][];
        teamCharacters[0] = new Character[_teamsContent[0].Length];
        teamCharacters[1] = new Character[_teamsContent[1].Length];
        return teamCharacters;
    }

    private Character CreateCharacterFromInfo(List<CharacterInfo> charactersInfo, string unitName, IBaseSkill[] unitSkills) {
        var characterInfo = charactersInfo.Find(character => character.Name.Trim() == unitName.Trim());
        var character = new Character(
            characterInfo.Name,
            characterInfo.Weapon,
            characterInfo.Gender,
            characterInfo.DeathQuote,
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