using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Game
{ 
    private Teams _teams;
    private View _view;
    private GameStatus _gameStatus;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _gameStatus = new GameStatus();
        _teams = new Teams(view, teamsFolder);
    }

    public void Play() {
        try {
            LoadTeams();
            StartGameLoop();
        } catch (InvalidTeamException e) {
            _view.WriteLine(e.Message);
        } catch (PlayerHasWonException e) {
            _view.WriteLine(e.Message);
        }
    }

    private void LoadTeams() {
        _teams.LoadTeams();
        _gameStatus.SetTeams(_teams);
    }

    private void StartGameLoop() {
        while (true) {
            CheckIfPlayerHasWon();
            PrepareCharacters();
            HandleRoundStart();
            HandleCombat();
            AdvanceRound();
        }
    }

    private void HandleRoundStart() {
        WriteRoundStartMessage();
    }
    
    private void WriteRoundStartMessage() {
        var firstPlayerIndex = _gameStatus.FirstPlayerIndex;
        var firstPlayerCharacter = _gameStatus.GetFirstPlayerCharacter();
        _view.WriteLine($"Round {_gameStatus.Round}: {firstPlayerCharacter.Name} (Player {firstPlayerIndex + 1}) comienza");
        WriteWeaponTriangleAdvantage();
    }

    public void HandleCombat() {
        var combatHandler = new CombatHandler(_gameStatus, _view);
        combatHandler.HandleCombat();
    }
    
    private void AdvanceRound() {
        _gameStatus.AdvanceRound();
    }
    
    private void CheckIfPlayerHasWon() {
        if (_gameStatus.HasPlayerWon(Player.Player1)) {
            throw new PlayerHasWonException("Player 1 ganó");
        }
        if (_gameStatus.HasPlayerWon(Player.Player2)) {
            throw new PlayerHasWonException("Player 2 ganó");
        }
    }

    private void PrepareCharacters() {
        SetCharacters();
        ResetCharacterSkills();
    }

    private void SetCharacters() {
        SetCharacterForPlayer(_gameStatus.FirstPlayerIndex);
        SetCharacterForPlayer(_gameStatus.SecondPlayerIndex);
    }
    
    private void ResetCharacterSkills() {
        var firstPlayerCharacter = _gameStatus.GetFirstPlayerCharacter();
        var secondPlayerCharacter = _gameStatus.GetSecondPlayerCharacter();
        
        firstPlayerCharacter.ResetSkills();
        secondPlayerCharacter.ResetSkills();
    }

    private void SetCharacterForPlayer(int playerIndex) {    
        var character = AskForCharacter(playerIndex);
        _gameStatus.SetCharacterForPlayer(playerIndex, character); 
    }
    
    private void WriteWeaponTriangleAdvantage() {
        var firstPlayerCharacter = _gameStatus.GetFirstPlayerCharacter();
        var secondPlayerCharacter = _gameStatus.GetSecondPlayerCharacter();
        var advantageMessage = WeaponTriangleAdvantage.GetAdvantageMessage(firstPlayerCharacter, secondPlayerCharacter);
        _view.WriteLine(advantageMessage);
    }

    private CharacterModel AskForCharacter(int playerIndex) {
        _view.WriteLine($"Player {playerIndex + 1} selecciona una opción");
        ShowCharacterOptions(playerIndex);

        var userOption = GetUserOption();
        var character = _teams.GetCharacterFromTeam(playerIndex, userOption);
        return character;
    }

    private void ShowCharacterOptions(int playerIndex) {
        var team = _teams.GetTeam(playerIndex);
        for (var unitIndex = 0; unitIndex < team.Length; unitIndex++) {
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