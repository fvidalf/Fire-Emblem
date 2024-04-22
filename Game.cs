using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private Character[][]? _teams;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play() {
        // Initialize teams loader. Loads teams and characters if valid
        var teamsLoader = new TeamsLoader(_view, _teamsFolder);
        teamsLoader.Execute();
        if (!teamsLoader.TeamsAreValid) return;

        var teams = teamsLoader.TeamsCharacters;

        // Start game loop
        var game = true;
        var round = 1;
        var atkPlayerIndex = 0;
        
        while (game) {
            
            // Ask for characters
            var defPlayerIndex = (atkPlayerIndex + 1) % 2;
            
            // Check if any team is empty
            if (teams[0].Length == 0) {
                _view.WriteLine("Player 2 ganó");
                game = false;
                continue;
            }
            if (teams[1].Length == 0) {
                _view.WriteLine("Player 1 ganó");
                game = false;
                continue;
            }
            
            var atkCharacter = AskForCharacter(teams, atkPlayerIndex);
            var defCharacter = AskForCharacter(teams, defPlayerIndex);
            
            _view.WriteLine($"Round {round}: {atkCharacter.Name} (Player {atkPlayerIndex + 1}) comienza");
            WriteWta(atkCharacter, defCharacter);
            
            // Opener attack
            atkCharacter.Attack(defCharacter);
            // Check if char is alive (convert into its own method)
            
            if (defCharacter.IsDead) {
                teams[defPlayerIndex] = RemoveCharacter(teams[defPlayerIndex], defCharacter);
                ReportHp(atkCharacter, defCharacter);
                atkPlayerIndex = ChangeAtkCharacter(atkPlayerIndex);
                round++;
                continue;
            }
            
            // Counter attack
            defCharacter.Attack(atkCharacter);
            // Check if char is alive
            if (atkCharacter.IsDead) {
                teams[atkPlayerIndex] = RemoveCharacter(teams[atkPlayerIndex], atkCharacter);
                ReportHp(atkCharacter, defCharacter);
                atkPlayerIndex = ChangeAtkCharacter(atkPlayerIndex);
                round++;
                continue;
            }

            // Option for followup attack
            var followUpIndex = DetermineFollowUp(atkCharacter, defCharacter, atkPlayerIndex, defPlayerIndex);
            if (followUpIndex != -1) {
                if (followUpIndex == atkPlayerIndex) {
                    atkCharacter.Attack(defCharacter);
                    if (defCharacter.IsDead) {
                        teams[defPlayerIndex] = RemoveCharacter(teams[defPlayerIndex], defCharacter);
                    }
                } else {
                    defCharacter.Attack(atkCharacter);
                    if (atkCharacter.IsDead) {
                        teams[atkPlayerIndex] = RemoveCharacter(teams[atkPlayerIndex], atkCharacter);
                    }
                }
            }
            
            ReportHp(atkCharacter, defCharacter);
            atkPlayerIndex = ChangeAtkCharacter(atkPlayerIndex);
            round++;
        }
    }

    private void LoadTeams() {
        var teamsLoader = new TeamsLoader(_view, _teamsFolder);
        teamsLoader.Execute();
    }

    private Character[] RemoveCharacter(Character[] team, Character character) {
        /*
         * Reallocates a new team array without the character that died
         */
        var newTeam = new Character[team.Length - 1];
        var j = 0;
        for (var i = 0; i < team.Length; i++) {
            if (team[i] != character) {
                newTeam[j] = team[i];
                j++;
            }
        }
        return newTeam;
    }
    
    private int ChangeAtkCharacter(int atkPlayerIndex) {
        return atkPlayerIndex == 0 ? 1 : 0;
    }
    
    private void WriteWta(Character atkCharacter, Character defCharacter) {
        switch (atkCharacter.Weapon) {
            case "Sword" when defCharacter.Weapon == "Axe":
            case "Axe" when defCharacter.Weapon == "Lance":
            case "Lance" when defCharacter.Weapon == "Sword":
                _view.WriteLine($"{atkCharacter.Name} ({atkCharacter.Weapon}) tiene ventaja con respecto a {defCharacter.Name} ({defCharacter.Weapon})");
                break;
            case "Axe" when defCharacter.Weapon == "Sword":
            case "Lance" when defCharacter.Weapon == "Axe":
            case "Sword" when defCharacter.Weapon == "Lance":
                _view.WriteLine($"{defCharacter.Name} ({defCharacter.Weapon}) tiene ventaja con respecto a {atkCharacter.Name} ({atkCharacter.Weapon})");
                break;
            default:
                _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
                break;
        }
    }

    private int DetermineFollowUp(Character atkCharacter, Character defCharacter, int atkPlayerIndex, int defPlayerIndex) {
        if (atkCharacter.Spd - defCharacter.Spd >= 5) {
            return atkPlayerIndex;
        } else if (defCharacter.Spd - atkCharacter.Spd >= 5) {
            return defPlayerIndex;
        } else {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
            return -1;
        }
    }

    private void ReportHp(Character atkCharacter, Character defCharacter) {
        _view.WriteLine($"{atkCharacter.Name} ({atkCharacter.Hp}) : {defCharacter.Name} ({defCharacter.Hp})");
    }

    private Character AskForCharacter(Character[][] teams, int playerIndex) {
        _view.WriteLine($"Player {playerIndex + 1} selecciona una opción");
        for (var i = 0; i < teams[playerIndex].Length; i++) {
            // We don't need to check if the character is dead here
            // We assume all the characters in the array are alive
            var unit = teams[playerIndex][i];
            _view.WriteLine($"{i}: {teams[playerIndex][i].Name}");
        }

        string? userString;
        do {
            userString = _view.ReadLine();
        } while (!int.TryParse(userString, out var _));

        return teams[playerIndex][int.Parse(userString)];
    }
}