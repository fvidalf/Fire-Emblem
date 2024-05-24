using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Game
{
    private View _view;
    private CombatHandler _combatHandler;
    private CharacterHandler _characterHandler;
    private Teams _newTeams;
    public int FirstPlayerIndex;
    public int SecondPlayerIndex;
    private int _round;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _newTeams = new Teams(view, teamsFolder);
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
        _newTeams.LoadTeams();
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
        var firstPlayerCharacter = _newTeams.GetPlayerCurrentCharacter(FirstPlayerIndex);
        _view.WriteLine($"Round {_round}: {firstPlayerCharacter.Name} (Player {FirstPlayerIndex + 1}) comienza");
        WriteWeaponTriangleAdvantage();
    }
    
    public void AdvanceRound() {
        _round++;
    }
    
    private void CheckIfTeamsAreEmpty() {

        if (_newTeams.IsTeamEmpty(0)) {
            throw new TeamIsEmptyException("Player 2 ganó");
        }
        
        if (_newTeams.IsTeamEmpty(1)) {
            throw new TeamIsEmptyException("Player 1 ganó");
        }
    }
    
    private void PrepareHandlers() {
        _characterHandler = new CharacterHandler(_view);
        _combatHandler = new CombatHandler(_characterHandler, _newTeams, this, _view);
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
        var firstPlayerCharacter = _newTeams.GetPlayerCurrentCharacter(FirstPlayerIndex);
        var secondPlayerCharacter = _newTeams.GetPlayerCurrentCharacter(SecondPlayerIndex);
        
        firstPlayerCharacter.ResetSkills();
        secondPlayerCharacter.ResetSkills();
    }
    
    public void SetCharacterForPlayer(int playerIndex) {    
        var character = AskForCharacter(playerIndex);
        _newTeams.SetCharacterForPlayer(playerIndex, character); 
    }
    
    private void WriteWeaponTriangleAdvantage() {
        var firstPlayerCharacter = _newTeams.GetPlayerCurrentCharacter(FirstPlayerIndex);
        var secondPlayerCharacter = _newTeams.GetPlayerCurrentCharacter(SecondPlayerIndex);
        var advantageMessage = WeaponTriangleAdvantage.GetAdvantageMessage(firstPlayerCharacter, secondPlayerCharacter);
        _view.WriteLine(advantageMessage);
    }
    
    public void ChangeFirstPlayer() {
        FirstPlayerIndex = FirstPlayerIndex == 0 ? 1 : 0;
        SecondPlayerIndex = SecondPlayerIndex == 0 ? 1 : 0;
    }

    private CharacterModel AskForCharacter(int playerIndex) {
        _view.WriteLine($"Player {playerIndex + 1} selecciona una opción");
        ShowCharacterOptions(playerIndex);

        var userOption = GetUserOption();
        var character = _newTeams.GetCharacterFromTeam(playerIndex, userOption);
        return character;
    }

    private void ShowCharacterOptions(int playerIndex) {
        var team = _newTeams.GetTeam(playerIndex);
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