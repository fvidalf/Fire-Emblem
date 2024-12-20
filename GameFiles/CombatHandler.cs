﻿using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles.GameStatusFiles;
namespace Fire_Emblem.GameFiles;

public class CombatHandler {

    private bool _doesRoundEnd;
    private CharacterModel _firstPlayerCharacter;
    private CharacterModel _secondPlayerCharacter;
    private int _roundPhase;
    private readonly CharacterHandler _characterHandler;
    private readonly SkillHandler _skillHandler;
    private readonly GameStatus _gameStatus;
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
        HandlePreCombatSkills();
        ExecuteCommonRound(_firstPlayerCharacter, _secondPlayerCharacter);
        HandleCombatSkills();
        HandleRoundEnd();
        if (_doesRoundEnd) return;
        ExecuteCommonRound(_secondPlayerCharacter, _firstPlayerCharacter);
        HandleCombatSkills();
        HandleRoundEnd();
        if (_doesRoundEnd) return;
        ExecuteFollowUpRound();
        HandleCombatSkills();
        HandleRoundEnd();
    }
    
    private void HandlePreCombatSkills() {
        SetCharacterRoundStatus(_firstPlayerCharacter, _secondPlayerCharacter);
        SetCharacterRoundStatus(_secondPlayerCharacter, _firstPlayerCharacter);
        _skillHandler.ApplyPreCombatSkills(_firstPlayerCharacter, _secondPlayerCharacter);
        _skillHandler.HandlePreCombatSkillEffectsNotification(_firstPlayerCharacter, _secondPlayerCharacter);
    }
    
    private void SetCharacterRoundStatus(CharacterModel activatingCharacter, CharacterModel rivalCharacter) {
        var roundStatus = GetRoundStatus(activatingCharacter, rivalCharacter);
        activatingCharacter.SetRoundStatus(roundStatus);
    }
    
    private RoundStatus GetRoundStatus(CharacterModel activatingCharacter, CharacterModel rivalCharacter) {
        return new RoundStatus(activatingCharacter, rivalCharacter, _firstPlayerCharacter, _roundPhase);
    }
    
    private void HandleCombatSkills() {
        SetCharacterRoundStatus(_firstPlayerCharacter, _secondPlayerCharacter);
        SetCharacterRoundStatus(_secondPlayerCharacter, _firstPlayerCharacter);
        _skillHandler.ApplyCombatSkills(_firstPlayerCharacter, _secondPlayerCharacter);
        _skillHandler.HandleCombatSkillEffectsNotification(_firstPlayerCharacter, _secondPlayerCharacter);
    }
    
    private void HandlePostCombatSkills() {
        SetCharacterRoundStatus(_firstPlayerCharacter, _secondPlayerCharacter);
        SetCharacterRoundStatus(_secondPlayerCharacter, _firstPlayerCharacter);
        _skillHandler.ApplyPostCombatSkills(_firstPlayerCharacter, _secondPlayerCharacter);
        _skillHandler.HandlePostCombatSkillEffectsNotification(_firstPlayerCharacter, _secondPlayerCharacter);
    }

    private void ExecuteCommonRound(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        HandleRegularAttack(firstPlayerCharacter, secondPlayerCharacter);
    }
    
    private void HandleRegularAttack(CharacterModel attackingCharacter, CharacterModel defendingCharacter) {
        _characterHandler.Attack(attackingCharacter, defendingCharacter, _roundPhase);
        if (defendingCharacter.IsDead) {
            _gameStatus.RemoveCharacter(defendingCharacter);
            _doesRoundEnd = true;
        } 
    }
    
    private void HandleRoundEnd() {
        if (_roundPhase == 2 || _doesRoundEnd) {
            HandlePostCombatSkills();
            ReportHp(_firstPlayerCharacter, _secondPlayerCharacter);
            SwapPlayers();
            UpdateCharacters();
        } else {
            AdvanceRoundPhase();
        }
    }
    
    private void ReportHp(CharacterModel atkCharacterModel, CharacterModel defCharacterModel) {
        _view.WriteLine($"{atkCharacterModel.Name} ({atkCharacterModel.Hp}) : {defCharacterModel.Name} ({defCharacterModel.Hp})");
    }
    
    private void SwapPlayers() {
        _gameStatus.SwapPlayers();
    }
    
    private void AdvanceRoundPhase() {
        _roundPhase++; 
    }

    private void ExecuteFollowUpRound() {
        HandleFollowUpAttack();
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
}