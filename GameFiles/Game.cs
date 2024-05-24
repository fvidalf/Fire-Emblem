using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Game
{
    private View _view;
    private TeamsLoader _teamsLoader; 
    private CombatHandler _combatHandler;
    private CharacterHandler _characterHandler;
    private CharacterModel[][]? _teams;
    public int FirstPlayerIndex;
    public int SecondPlayerIndex;
    private int _round;
    private Dictionary<int, CharacterModel> _charactersByPlayerIndex = new Dictionary<int, CharacterModel>();
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsLoader = new TeamsLoader(view, teamsFolder);
        FirstPlayerIndex = 0;
        SecondPlayerIndex = 1;
        _round = 1;
    }

    public void Play() {
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
        _teams = _teamsLoader.GetTeams();
    }

    private void StartGameLoop() {
        PrepareHandlers();
        while (true) {
            CheckIfTeamsAreEmpty();
            PrepareCharacters();
            HandleRoundStart();
            _combatHandler.HandleCombat();
        }
    }

    private void HandleRoundStart() {
        WriteRoundStartMessage();
    }
    
    private void WriteRoundStartMessage() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(FirstPlayerIndex);
        _view.WriteLine($"Round {_round}: {firstPlayerCharacter.Name} (Player {FirstPlayerIndex + 1}) comienza");
        WriteWeaponTriangleAdvantage();
    }
    
    public CharacterModel GetCharacterByPlayerIndex(int playerIndex) {
        return _charactersByPlayerIndex[playerIndex];
    }

    public void AdvanceRound() {
        _round++;
    }
    
    private void CheckIfTeamsAreEmpty() {
        if (_teams[0].Length == 0) {
            throw new TeamIsEmptyException("Player 2 ganó");
        }
        if (_teams[1].Length == 0) {
            throw new TeamIsEmptyException("Player 1 ganó");
        }
    }
    
    private void PrepareHandlers() {
        _characterHandler = new CharacterHandler(_view);
        _combatHandler = new CombatHandler(_characterHandler, this, _view);
    }

    private void PrepareCharacters() {
        SetCharacters();
        ResetCharacterSkills();
    }

    private void SetCharacters() {
        SetCharacterForPlayer(FirstPlayerIndex);
        SetCharacterForPlayer(SecondPlayerIndex);
    }
    
    private void ResetCharacterSkills() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(FirstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(SecondPlayerIndex);
        
        firstPlayerCharacter.ResetSkills();
        secondPlayerCharacter.ResetSkills();
    }
    
    public void SetCharacterForPlayer(int playerIndex) {
        var character = AskForCharacter(playerIndex);
        _charactersByPlayerIndex[playerIndex] = character;
    }
    
    private void WriteWeaponTriangleAdvantage() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(FirstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(SecondPlayerIndex);
        var advantageMessage = WeaponTriangleAdvantage.GetAdvantageMessage(firstPlayerCharacter, secondPlayerCharacter);
        _view.WriteLine(advantageMessage);
    }

    public void RemoveCurrentPlayerCharacter(int playerIndex) {
        var characterToRemove = _charactersByPlayerIndex[playerIndex];
        var team = _teams[playerIndex];
        
        var tempTeam = team.ToList();
        tempTeam.Remove(characterToRemove);
        _teams[playerIndex] = tempTeam.ToArray();
    }
    
    public void ChangeFirstPlayer() {
        FirstPlayerIndex = FirstPlayerIndex == 0 ? 1 : 0;
        SecondPlayerIndex = SecondPlayerIndex == 0 ? 1 : 0;
    }

    private CharacterModel AskForCharacter(int playerIndex) {
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