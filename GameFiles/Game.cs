using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Game
{
    private View _view;
    private string _teamsFolder;
    private Character[][]? _teams;
    private int _firstPlayerIndex;
    private int _secondPlayerIndex;
    private int _round;
    private int _roundPhase;
    private Dictionary<int, Character> _charactersByPlayerIndex = new Dictionary<int, Character>();
    private Character _firstPlayerCharacter;
    private Character _secondPlayerCharacter;
    private bool _doesRoundEnd;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _firstPlayerIndex = 0;
        _secondPlayerIndex = 1;
        _round = 1;
        _roundPhase = 0;
        _doesRoundEnd = false;
    }

    public void Play() {
        // Initialize teams loader. Loads teams and characters if valid
        try {
            LoadTeams();
            StartGameLoop();
        } catch (InvalidTeamException e) {
            _view.WriteLine(e.Message);
        } catch (TeamIsEmptyException e) {
            _view.WriteLine(e.Message);
        }
    }

    private void LoadTeams() {
        var teamsLoader = new TeamsLoader(_view, _teamsFolder);
        teamsLoader.Execute();
        _teams = teamsLoader.TeamsCharacters;
    }

    private void StartGameLoop() {
        while (true) {
            CheckIfTeamsAreEmpty();
            SetCharacters();
            WriteRoundStartMessage();
            ResetRoundParameters();

            HandleRegularAttack(_firstPlayerIndex, _secondPlayerIndex);
            HandleRoundEnd();
            if (_doesRoundEnd) continue;
            
            HandleRegularAttack(_secondPlayerIndex, _firstPlayerIndex);
            HandleRoundEnd();
            if (_doesRoundEnd) continue;

            HandleFollowUpAttack();
            HandleRoundEnd();
        }
    }

    private void ResetRoundParameters() {
        _roundPhase = 0;
        _doesRoundEnd = false;
    }
    
    private void CheckIfTeamsAreEmpty() {
        if (_teams[0].Length == 0) {
            throw new TeamIsEmptyException("Player 2 ganó");
        }
        if (_teams[1].Length == 0) {
            throw new TeamIsEmptyException("Player 1 ganó");
        }
    }

    private void SetCharacters() {
        SetCharacterForPlayer(_firstPlayerIndex);
        _firstPlayerCharacter = GetCharacterForPlayer(_firstPlayerIndex);
        
        SetCharacterForPlayer(_secondPlayerIndex);
        _secondPlayerCharacter = GetCharacterForPlayer(_secondPlayerIndex);
    }
    
    private void SetCharacterForPlayer(int playerIndex) {
        var character = AskForCharacter(playerIndex);
        _charactersByPlayerIndex[playerIndex] = character;
    }
    
    private Character GetCharacterForPlayer(int playerIndex) {
        return _charactersByPlayerIndex[playerIndex];
    }
    
    private void WriteRoundStartMessage() {
        _view.WriteLine($"Round {_round}: {_firstPlayerCharacter.Name} (Player {_firstPlayerIndex + 1}) comienza");
        WriteWta();
    }

    private void HandleRegularAttack(int attackingPlayerIndex, int defendingPlayerIndex) {
        var attackingCharacter = GetCharacterByPlayerIndex(attackingPlayerIndex);
        var defendingCharacter = GetCharacterByPlayerIndex(defendingPlayerIndex);
        
        attackingCharacter.Attack(defendingCharacter);
        if (defendingCharacter.IsDead) {
            RemoveCurrentPlayerCharacter(defendingPlayerIndex);
            _doesRoundEnd = true;
        } 
    }

    private void HandleRoundEnd() {
        if (_roundPhase == 2 || _doesRoundEnd) {
            ReportHp(_firstPlayerCharacter, _secondPlayerCharacter);
            ChangeFirstPlayer();
            _round++;
        }
        _roundPhase++;
    }
    
    private Character GetCharacterByPlayerIndex(int playerIndex) {
        return _charactersByPlayerIndex[playerIndex];
    }

    private void RemoveCurrentPlayerCharacter(int playerIndex) {
        var characterToRemove = _charactersByPlayerIndex[playerIndex];
        var team = _teams[playerIndex];
        
        var tempTeam = team.ToList();
        tempTeam.Remove(characterToRemove);
        _teams[playerIndex] = tempTeam.ToArray();
    }
    
    private void ChangeFirstPlayer() {
        _firstPlayerIndex = _firstPlayerIndex == 0 ? 1 : 0;
        _secondPlayerIndex = _secondPlayerIndex == 0 ? 1 : 0;
    }

    private void HandleFollowUpAttack() {
        var followUpPlayerIndex = DetermineFollowUpPlayer(_firstPlayerIndex, _secondPlayerIndex);
        if (followUpPlayerIndex == -1) return;
        
        if (followUpPlayerIndex == _firstPlayerIndex) {
            HandleRegularAttack(_firstPlayerIndex, _secondPlayerIndex);
        } else {
            HandleRegularAttack(_secondPlayerIndex, _firstPlayerIndex);
        }
    }
    
    private void WriteWta() {
        
        switch (_firstPlayerCharacter.Weapon) {
            case "Sword" when _secondPlayerCharacter.Weapon == "Axe":
            case "Axe" when _secondPlayerCharacter.Weapon == "Lance":
            case "Lance" when _secondPlayerCharacter.Weapon == "Sword":
                _view.WriteLine($"{_firstPlayerCharacter.Name} ({_firstPlayerCharacter.Weapon}) tiene ventaja con respecto a {_secondPlayerCharacter.Name} ({_secondPlayerCharacter.Weapon})");
                break;
            case "Axe" when _secondPlayerCharacter.Weapon == "Sword":
            case "Lance" when _secondPlayerCharacter.Weapon == "Axe":
            case "Sword" when _secondPlayerCharacter.Weapon == "Lance":
                _view.WriteLine($"{_secondPlayerCharacter.Name} ({_secondPlayerCharacter.Weapon}) tiene ventaja con respecto a {_firstPlayerCharacter.Name} ({_firstPlayerCharacter.Weapon})");
                break;
            default:
                _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
                break;
        }
    }

    private int DetermineFollowUpPlayer(int atkPlayerIndex, int defPlayerIndex) {
        var attackingCharacter = GetCharacterByPlayerIndex(atkPlayerIndex);
        var defendingCharacter = GetCharacterByPlayerIndex(defPlayerIndex);
        
        if (attackingCharacter.Spd - defendingCharacter.Spd >= 5) {
            return atkPlayerIndex;
        } else if (defendingCharacter.Spd - attackingCharacter.Spd >= 5) {
            return defPlayerIndex;
        } else {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
            return -1;
        }
    }

    private void ReportHp(Character atkCharacter, Character defCharacter) {
        _view.WriteLine($"{atkCharacter.Name} ({atkCharacter.Hp}) : {defCharacter.Name} ({defCharacter.Hp})");
    }

    private Character AskForCharacter(int playerIndex) {
        _view.WriteLine($"Player {playerIndex + 1} selecciona una opción");
        ShowCharacterOptions(playerIndex);

        var userOption = GetUserOption();
        return _teams[playerIndex][userOption];
    }

    private void ShowCharacterOptions(int playerIndex) {
        var team = _teams[playerIndex];
        for (var unitIndex = 0; unitIndex < team.Length; unitIndex++) {
            var unit = team[unitIndex];
            _view.WriteLine($"{unitIndex}: {team[unitIndex].Name}");
        }
    }
    
    private int GetUserOption() {
        string? userString;
        do {
            userString = _view.ReadLine();
        } while (!int.TryParse(userString, out var _));
        return int.Parse(userString);
    }
}