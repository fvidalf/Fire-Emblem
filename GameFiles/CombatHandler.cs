using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem.GameFiles;

public class CombatHandler {

    private bool _doesRoundEnd;
    private CharacterModel _firstPlayerCharacter;
    private CharacterModel _secondPlayerCharacter;
    private int _roundPhase;
    private CharacterHandler _characterHandler;
    private SkillHandler _skillHandler;
    private GameStatus _gameStatus;
    private readonly View _view;
    
    public CombatHandler(GameStatus gameStatus, View view) {
        _doesRoundEnd = false;
        _roundPhase = 0;
        _characterHandler = new CharacterHandler(view);
        _skillHandler = new SkillHandler(view, _characterHandler);
        _gameStatus = gameStatus;
        UpdateCharacters();
        _view = view;
    }

    private void UpdateCharacters() {
        _firstPlayerCharacter = _gameStatus.GetFirstPlayerCharacter();
        _secondPlayerCharacter = _gameStatus.GetSecondPlayerCharacter();
    }
    
    public void HandleCombat() {
        ExecuteCommonRound(_firstPlayerCharacter, _secondPlayerCharacter);
        if (_doesRoundEnd) return;
        ExecuteCommonRound(_secondPlayerCharacter, _firstPlayerCharacter);
        if (_doesRoundEnd) return;
        ExecuteFollowUpRound();
    }

    private void ExecuteCommonRound(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        HandleCharacterSkills();
        HandleRegularAttack(firstPlayerCharacter, secondPlayerCharacter);
        HandleRoundEnd();
    }

    private void ExecuteFollowUpRound() {
        HandleCharacterSkills();
        HandleFollowUpAttack();
        HandleRoundEnd();
    }

    private void HandleCharacterSkills() {
        SetCharacterRoundStatus(_firstPlayerCharacter, _secondPlayerCharacter);
        SetCharacterRoundStatus(_secondPlayerCharacter, _firstPlayerCharacter);
        
        _skillHandler.ApplyCharacterSkills(_firstPlayerCharacter, _secondPlayerCharacter);
        _skillHandler.HandleSkillEffectsNotification(_firstPlayerCharacter, _secondPlayerCharacter);
    }

    private void SetCharacterRoundStatus(CharacterModel activatingCharacter, CharacterModel rivalCharacter) {
        var roundStatus = GetRoundStatus(activatingCharacter, rivalCharacter);
        activatingCharacter.SetRoundStatus(roundStatus);
    }
    
    private RoundStatus GetRoundStatus(CharacterModel activatingCharacter, CharacterModel rivalCharacter) {
        return new RoundStatus(activatingCharacter, rivalCharacter, _firstPlayerCharacter, _roundPhase);
    }
    
    private void HandleRegularAttack(CharacterModel attackingCharacter, CharacterModel defendingCharacter) {
        _characterHandler.Attack(attackingCharacter, defendingCharacter);
        if (defendingCharacter.IsDead) {
            _gameStatus.RemoveCharacter(defendingCharacter);
            _doesRoundEnd = true;
        } 
    }

    private void HandleRoundEnd() {
        if (_roundPhase == 2 || _doesRoundEnd) {
            ReportHp(_firstPlayerCharacter, _secondPlayerCharacter);
            SwapPlayers();
            UpdateCharacters();
        } else {
            AdvanceRoundPhase();
        }
    }
    
    private void AdvanceRoundPhase() {
        _roundPhase++; 
    }
    
    private void SwapPlayers() {
        _gameStatus.SwapPlayers();
    }

    private void HandleFollowUpAttack() {
        var followUpCharacter = DetermineFollowUpCharacter(_firstPlayerCharacter, _secondPlayerCharacter);
        if (followUpCharacter == null) return;
        
        if (followUpCharacter == _firstPlayerCharacter) {
            HandleRegularAttack(_firstPlayerCharacter, _secondPlayerCharacter);
        } else {
            HandleRegularAttack(_secondPlayerCharacter, _firstPlayerCharacter);
        }
    }
    
    private CharacterModel? DetermineFollowUpCharacter(CharacterModel attackingCharacter, CharacterModel defendingCharacter) {
        
        if (attackingCharacter.Spd - defendingCharacter.Spd >= 5) {
            return attackingCharacter;
        } else if (defendingCharacter.Spd - attackingCharacter.Spd >= 5) {
            return defendingCharacter;
        } else {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
            return null;
        }
    }

    private void ReportHp(CharacterModel atkCharacterModel, CharacterModel defCharacterModel) {
        _view.WriteLine($"{atkCharacterModel.Name} ({atkCharacterModel.Hp}) : {defCharacterModel.Name} ({defCharacterModel.Hp})");
    }
}