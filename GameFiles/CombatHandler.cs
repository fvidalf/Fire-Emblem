using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.GameFiles;

public class CombatHandler {

    private bool _doesRoundEnd;
    private int _firstPlayerIndex;
    private int _secondPlayerIndex;
    private int _roundPhase;
    private CharacterHandler _characterHandler;
    private SkillHandler _skillHandler;
    private Teams _teams;
    private Game _game;
    private readonly View _view;
    
    public CombatHandler(CharacterHandler characterHandler, SkillHandler skillHandler, Teams teams, Game game, View view) {
        _doesRoundEnd = false;
        _roundPhase = 0;
        _characterHandler = characterHandler;
        _skillHandler = skillHandler;
        _teams = teams;
        _game = game;
        _view = view;
    }
    
    public void HandleCombat() {
        ResetRoundParameters();
        _firstPlayerIndex = _game.FirstPlayerIndex;
        _secondPlayerIndex = _game.SecondPlayerIndex;
        ExecuteCommonRound(_firstPlayerIndex, _secondPlayerIndex);
        if (_doesRoundEnd) return;
        _firstPlayerIndex = _game.FirstPlayerIndex;
        _secondPlayerIndex = _game.SecondPlayerIndex;
        ExecuteCommonRound(_secondPlayerIndex, _firstPlayerIndex);
        if (_doesRoundEnd) return;
        _firstPlayerIndex = _game.FirstPlayerIndex;
        _secondPlayerIndex = _game.SecondPlayerIndex;
        ExecuteFollowUpRound();
    }
    
    private void ResetRoundParameters() {
        _doesRoundEnd = false;
        _roundPhase = 0;
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

    private void ApplyCharacterSkills(CharacterModel firstPlayerCharacterModel, CharacterModel secondPlayerCharacterModel) {
        firstPlayerCharacterModel.ResetModifiedStats();
        secondPlayerCharacterModel.ResetModifiedStats();
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacterModel, secondPlayerCharacterModel});
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    private Tuple<CharacterModel, SingleCharacterSkill>[] JoinSkills(CharacterModel[] characters) {
        var skillsPairedToCharacter = new List<Tuple<CharacterModel, SingleCharacterSkill>>();
        foreach (var character in characters) {
            foreach (var skill in character.SingleSkills) {
                skillsPairedToCharacter.Add(new Tuple<CharacterModel, SingleCharacterSkill>(character, skill));
            }
        }
        return skillsPairedToCharacter.ToArray();
    }
    
    private Tuple<CharacterModel, SingleCharacterSkill>[] OrderSkills(Tuple<CharacterModel, SingleCharacterSkill>[] skillsPairedToCharacter) {
        var firstSkillsToApply = new List<Tuple<CharacterModel, SingleCharacterSkill>>();
        var lastSkillsToApply = new List<Tuple<CharacterModel, SingleCharacterSkill>>();
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            if (skill is BonusNeutralizer or PenaltyNeutralizer) {
                lastSkillsToApply.Add(new Tuple<CharacterModel, SingleCharacterSkill>(character, skill));
            }
            else {
                firstSkillsToApply.Add(new Tuple<CharacterModel, SingleCharacterSkill>(character, skill));
            }
        }
        firstSkillsToApply.AddRange(lastSkillsToApply);
        return firstSkillsToApply.ToArray();
    }

    private void ApplySkillsJointly(Tuple<CharacterModel, SingleCharacterSkill>[] skillsPairedToCharacter) {
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            //if (!skill.IsActivated) character.ApplySkill(skill, character.GameStatus);
            if (!skill.IsActivated) _characterHandler.ApplySkill(character, skill, character.RoundStatus);
        }
    }
    
    private void HandleSkillEffectsNotification(CharacterModel firstPlayerCharacterModel, CharacterModel secondPlayerCharacterModel) {
        var firstPlayerSkillEffects = firstPlayerCharacterModel.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacterModel.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        NotifySkillEffects(skillEffects);
    }
    
    private Dictionary<CharacterModel, List<Tuple<EffectType, Stat, int>>> JoinPlayerSkillEffects(Dictionary<CharacterModel, SkillEffect> firstPlayerSkillEffects, Dictionary<CharacterModel, SkillEffect> secondPlayerSkillEffects) {
        var joinedSkillEffects = new Dictionary<CharacterModel, List<Tuple<EffectType, Stat, int>>>();
        foreach (var character in firstPlayerSkillEffects.Keys) {
            var firstPlayerEffects = firstPlayerSkillEffects[character];
            var secondPlayerEffects = secondPlayerSkillEffects[character];

            firstPlayerEffects.Join(secondPlayerEffects); 
            var sortedEffects = GetSortedEffects(firstPlayerEffects);
            joinedSkillEffects[character] = sortedEffects;
        }
        return joinedSkillEffects;
    }
    
    private List<Tuple<EffectType, Stat, int>> GetSortedEffects(SkillEffect effects) {
        var simpleEffects = effects.CollapseIntoList();
        var effectsSortedByEffectType = simpleEffects.OrderBy(effect => effect.Item1);
        var effectsSortedByStat = effectsSortedByEffectType.ThenBy(effect => effect.Item2);
       
        return effectsSortedByStat.ToList();
    }
    
    private void NotifySkillEffects(Dictionary<CharacterModel,  List<Tuple<EffectType, Stat, int>>> skillEffect) {
        foreach (var character in skillEffect.Keys) {
            foreach (var effect in skillEffect[character]) {
                var effectType = effect.Item1;
                var stat = effect.Item2;
                var amount = effect.Item3;
                switch (effectType) {
                    case EffectType.FirstAttackBonus or EffectType.FirstAttackPenalty:
                        NotifyFirstAttackSkill(character, stat, amount);
                        break;
                    case EffectType.RegularBonus or EffectType.RegularPenalty:
                        NotifyRegularSkill(character, stat, amount);
                        break;
                    case EffectType.PenaltyNeutralizer:
                        NotifyPenaltyNeutralizer(character, stat);
                        break;
                    case EffectType.BonusNeutralizer:
                        NotifyBonusNeutralizer(character, stat);
                        break;
                }
            }
        }
    }
    
    private void NotifyFirstAttackSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            _view.WriteLine($"{characterModel.Name} obtiene {StatToString.RegularizeMap[stat]}{diffSign}{amount} en su primer ataque");
        } 
    }
    
    private void NotifyRegularSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            _view.WriteLine($"{characterModel.Name} obtiene {StatToString.Map[stat]}{diffSign}{amount}");
        }
    }
    
    private void NotifyPenaltyNeutralizer(CharacterModel characterModel, Stat stat) {
        _view.WriteLine($"Los penalty de {StatToString.RegularizeMap[stat]} de {characterModel.Name} fueron neutralizados");
    }
    
    private void NotifyBonusNeutralizer(CharacterModel characterModel, Stat stat) {
        _view.WriteLine($"Los bonus de {StatToString.RegularizeMap[stat]} de {characterModel.Name} fueron neutralizados");
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
            ChangeFirstPlayer();
            _game.AdvanceRound();
        }
        AdvanceRoundPhase();
    }
    
    private void AdvanceRoundPhase() {
        _roundPhase++; 
    }
    
    private void RemoveCurrentPlayerCharacter(int playerIndex) {
        _teams.RemoveCurrentPlayerCharacter(playerIndex);
    }
    
    private void ChangeFirstPlayer() {
        _game.ChangeFirstPlayer();
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