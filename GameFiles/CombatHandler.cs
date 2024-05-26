using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem.GameFiles;

public class CombatHandler {

    private bool _doesRoundEnd;
    private int _firstPlayerIndex;
    private int _secondPlayerIndex;
    private int _roundPhase;
    private CharacterHandler _characterHandler;
    private SkillHandler _skillHandler;
    private Teams _teams;
    private GameStatus _gameStatus;
    private readonly View _view;
    
    public CombatHandler(Teams teams, GameStatus gameStatus, View view) {
        _doesRoundEnd = false;
        _roundPhase = 0;
        _characterHandler = new CharacterHandler(view);
        _skillHandler = new SkillHandler(view, _characterHandler);
        _teams = teams;
        _gameStatus = gameStatus;
        _view = view;
    }
    
    public void HandleCombat() {
        _firstPlayerIndex = _gameStatus.FirstPlayerIndex;
        _secondPlayerIndex = _gameStatus.SecondPlayerIndex;
        ExecuteCommonRound(_firstPlayerIndex, _secondPlayerIndex);
        if (_doesRoundEnd) return;
        _firstPlayerIndex = _gameStatus.FirstPlayerIndex;
        _secondPlayerIndex = _gameStatus.SecondPlayerIndex;
        ExecuteCommonRound(_secondPlayerIndex, _firstPlayerIndex);
        if (_doesRoundEnd) return;
        _firstPlayerIndex = _gameStatus.FirstPlayerIndex;
        _secondPlayerIndex = _gameStatus.SecondPlayerIndex;
        ExecuteFollowUpRound();
    }

    private void ExecuteCommonRound(int attackingPlayerIndex, int defendingPlayerIndex) {
        HandleCharacterSkills();
        HandleRegularAttack(attackingPlayerIndex, defendingPlayerIndex);
        HandleRoundEnd();
    }

    private void ExecuteFollowUpRound() {
        HandleCharacterSkills();
        HandleFollowUpAttack();
        HandleRoundEnd();
    }

    private void HandleCharacterSkills() {
        var firstPlayerCharacter = _teams.GetPlayerCurrentCharacter(_firstPlayerIndex);
        var secondPlayerCharacter = _teams.GetPlayerCurrentCharacter(_secondPlayerIndex);
        SetCharacterRoundStatus(firstPlayerCharacter, _firstPlayerIndex, _secondPlayerIndex);
        SetCharacterRoundStatus(secondPlayerCharacter, _secondPlayerIndex, _firstPlayerIndex);
        
        _skillHandler.ApplyCharacterSkills(firstPlayerCharacter, secondPlayerCharacter);
        _skillHandler.HandleSkillEffectsNotification(firstPlayerCharacter, secondPlayerCharacter);
    }

    private void SetCharacterRoundStatus(CharacterModel characterModel, int characterIndex, int rivalIndex) {
        var roundStatus = GetRoundStatus(characterIndex, rivalIndex);
        characterModel.SetRoundStatus(roundStatus);
    }
    
    private RoundStatus GetRoundStatus(int activatingPlayerIndex, int rivalPlayerIndex) {
        var activatingCharacter = _teams.GetPlayerCurrentCharacter(activatingPlayerIndex);
        var rivalCharacter = _teams.GetPlayerCurrentCharacter(rivalPlayerIndex);
        var firstCharacter = _teams.GetPlayerCurrentCharacter(_firstPlayerIndex);
        return new RoundStatus(activatingCharacter, rivalCharacter, firstCharacter, _roundPhase);
    }
    
    private void HandleRegularAttack(int attackingPlayerIndex, int defendingPlayerIndex) {
        var attackingCharacter = _teams.GetPlayerCurrentCharacter(attackingPlayerIndex);
        var defendingCharacter = _teams.GetPlayerCurrentCharacter(defendingPlayerIndex);
        
        _characterHandler.Attack(attackingCharacter, defendingCharacter);
        if (defendingCharacter.IsDead) {
            RemoveCurrentPlayerCharacter(defendingPlayerIndex);
            _doesRoundEnd = true;
        } 
    }

    private void HandleRoundEnd() {
        var firstPlayerCharacter = _teams.GetPlayerCurrentCharacter(_firstPlayerIndex);
        var secondPlayerCharacter = _teams.GetPlayerCurrentCharacter(_secondPlayerIndex);
        if (_roundPhase == 2 || _doesRoundEnd) {
            ReportHp(firstPlayerCharacter, secondPlayerCharacter);
            SwapPlayers();
        } else {
            AdvanceRoundPhase();
        }
    }
    
    private void AdvanceRoundPhase() {
        _roundPhase++; 
    }
    
    private void RemoveCurrentPlayerCharacter(int playerIndex) {
        _teams.RemoveCurrentPlayerCharacter(playerIndex);
    }
    
    private void SwapPlayers() {
        _gameStatus.SwapPlayers();
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
    
    private int DetermineFollowUpPlayer(int atkPlayerIndex, int defPlayerIndex) {
        var attackingCharacter = _teams.GetPlayerCurrentCharacter(atkPlayerIndex);
        var defendingCharacter = _teams.GetPlayerCurrentCharacter(defPlayerIndex);
        
        if (attackingCharacter.Spd - defendingCharacter.Spd >= 5) {
            return atkPlayerIndex;
        } else if (defendingCharacter.Spd - attackingCharacter.Spd >= 5) {
            return defPlayerIndex;
        } else {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
            return -1;
        }
    }

    private void ReportHp(CharacterModel atkCharacterModel, CharacterModel defCharacterModel) {
        _view.WriteLine($"{atkCharacterModel.Name} ({atkCharacterModel.Hp}) : {defCharacterModel.Name} ({defCharacterModel.Hp})");
    }
}