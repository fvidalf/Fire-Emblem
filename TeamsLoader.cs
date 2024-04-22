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
    public bool TeamsAreValid;

    public TeamsLoader(View view, string teamsFolder) {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    private int AskForTeam() {
        /*
         * Asks the user to select a team from the available files in the teams folder
         * returns the index of the selected team as an int
         */
        _view.WriteLine("Elige un archivo para cargar los equipos");
        var files = Directory.GetFiles(_teamsFolder);
        for (var i = 0; i < files.Length; i++) {
            _view.WriteLine($"{i}: {Path.GetFileName(files[i])}");
        }

        string? userString;
        do {
            userString = _view.ReadLine();
        } while (!int.TryParse(userString, out var _));

        // Return team number
        return int.Parse(userString);
    }

    private void ReadTeamFile(int option) {
        /*
         * Reads the selected file and stores the content in the _teamsContent string array
         */
        var file = Directory.GetFiles(_teamsFolder)[option];
        // Read file content
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
        /*
         * Verifies that the teams check the following conditions:
         * 1. All units in a team have different names
         * 2. All units have two or less skills
         * 3. All skills in a unit are different
         * 4. Teams are not empty and have 3 units at most
         */
        
        TeamsAreValid = true;

        foreach (var team in _teamsContent) {

            // Checking condition 4
            var isTeamLengthValid = IsTeamLengthValid(team);
            
            // Checking condition 1, 2 and 3
            var isTeamValidRelatedToUnits = IsTeamValidRelatedToUnits(team);
            
            if (!isTeamLengthValid || !isTeamValidRelatedToUnits) {
                TeamsAreValid = false;
                return;
            }
        }
    }
    
    private bool IsTeamLengthValid(string[] team) {
        return !(team.Length is 0 or > 3);
    }
    
    private bool IsTeamValidRelatedToUnits(string[] team) {
        var unitNames = new List<string>();

        foreach (var unit in team) {
            var unitName = GetUnitName(unit);

            if (IsUnitAlreadyInTeam(unitName, unitNames)) return false;
            unitNames.Add(unitName);
            
            if (!AreThereSkills(unit)) continue;
            if (!IsUnitValidRelatedToSkills(unit)) return false;
        }

        return true;
    }
    
    private string GetUnitName(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1 ? unit[..firstWhitespace] : unit[..];
    }

    private bool IsUnitAlreadyInTeam(string unit, List<string> unitNames) {
        return unitNames.Contains(unit);
    }

    private bool AreThereSkills(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return firstWhitespace != -1;
    }

    private bool IsUnitValidRelatedToSkills(string unit) {
        var skillNames = new List<string>();

        var unitSkills = GetUnitSkillNames(unit);

        foreach (var skill in unitSkills) {
            if (skillNames.Contains(skill)) return false;
            skillNames.Add(skill);
        }
        
        if (unitSkills.Length > 2) return false;
        return true;
    }
    
    private string[] GetUnitSkillNames(string unit) {
        var firstWhitespace = unit.IndexOf(' ');
        return unit.Trim()[(firstWhitespace + 2)..^1].Split(',');
    }

    private Character[][] LoadTeams() {

        // Load characters from json file into a list
        var myJson = File.ReadAllText("characters.json");
        var characters = JsonSerializer.Deserialize<List<CharacterInfo>>(myJson) ??
                         throw new ArgumentNullException("JsonSerializer.Deserialize<List<CharacterInfo>>(myJson)");

        // Create an array of characters from the teams content
        var teamCharacters = new Character[2][];
        teamCharacters[0] = new Character[_teamsContent[0].Length];
        teamCharacters[1] = new Character[_teamsContent[1].Length];

        for (var i = 0; i < 2; i++) {
            for (var j = 0; j < _teamsContent[i].Length; j++) {
                var unit = _teamsContent[i][j];

                var unitName = GetUnitName(unit);
                var unitLoadedSkills = GetLoadedSkills(unit);

                // Search for the character in the list
                var characterInfo = characters.Find(character => character.Name.Trim() == unitName.Trim());
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
                teamCharacters[i][j] = character; }
        }

        return teamCharacters;
    }

    private IBaseSkill[] GetLoadedSkills(string unit) {
        if (AreThereSkills(unit)) {
            var unitSkillNames = GetUnitSkillNames(unit);
            var loadedSkills = SkillLoader.GetLoadedSkills(unitSkillNames);
            return loadedSkills.ToArray();
        }
        return Array.Empty<IBaseSkill>();
    } 

    public void Execute() {
        var userOption = AskForTeam();
        ReadTeamFile(userOption);
        VerifyTeams();
        Console.WriteLine(TeamsAreValid);
        if (!TeamsAreValid) {
            _view.WriteLine("Archivo de equipos no válido");
        }
        else {
            TeamsCharacters = LoadTeams();
        }
    }
}