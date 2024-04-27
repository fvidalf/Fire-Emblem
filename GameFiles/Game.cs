﻿using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf;
using Fire_Emblem.Skills.SkillEffectFiles;
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
            PrepareCharacters();
            HandleRoundStart();
            HandleCombat();
        }
    }

    private void HandleRoundStart() {
        WriteRoundStartMessage();
        ResetRoundParameters();
    }

    private void ResetRoundParameters() {
        _roundPhase = 0;
        _doesRoundEnd = false;
    }

    private void HandleCombat() {
        ApplyCharacterSkills(true);
        HandleRegularAttack(_firstPlayerIndex, _secondPlayerIndex);
        HandleRoundEnd();
        if (_doesRoundEnd) return;
        
        ApplyCharacterSkills(false);
        HandleRegularAttack(_secondPlayerIndex, _firstPlayerIndex);
        HandleRoundEnd();
        if (_doesRoundEnd) return;

        ApplyCharacterSkills(false);
        HandleFollowUpAttack();
        HandleRoundEnd();
    }

    private void ApplyCharacterSkills(bool notify) {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        var firstPlayerGameStatus = GetGameStatus(_firstPlayerIndex, _secondPlayerIndex);
        firstPlayerCharacter.ReceiveGameStatus(firstPlayerGameStatus);
        
        // firstPlayerCharacter.ApplySkills();
        
        var secondPlayerCharacter = GetCharacterByPlayerIndex(_secondPlayerIndex);
        var secondPlayerGameStatus = GetGameStatus(_secondPlayerIndex, _firstPlayerIndex);
        secondPlayerCharacter.ReceiveGameStatus(secondPlayerGameStatus);
        
        // secondPlayerCharacter.ApplySkills();
        
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacter, secondPlayerCharacter});
        ApplySkillsJointly(joinedSkills);
        
        
        var firstPlayerSkillEffects = firstPlayerCharacter.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacter.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        if (notify) NotifySkillEffects(skillEffects);
    }
    
    private Tuple<Character, SingleCharacterSkill>[] JoinSkills(Character[] characters) {
        foreach (var character in characters) {
            character.ResetModifiedStats();
        }
        var skillsPairedToCharacter = new List<Tuple<Character, SingleCharacterSkill>>();
        foreach (var skill in characters[0].SingledSkills) {
            skillsPairedToCharacter.Add(new Tuple<Character, SingleCharacterSkill>(characters[0], skill));
        }

        foreach (var skill in characters[1].SingledSkills) {
            skillsPairedToCharacter.Add(new Tuple<Character, SingleCharacterSkill>(characters[1], skill));
        }
        
        var orderedSkills = OrderSkills(skillsPairedToCharacter.ToArray());
        return orderedSkills;
    }
    
    private Tuple<Character, SingleCharacterSkill>[] OrderSkills(Tuple<Character, SingleCharacterSkill>[] skillsPairedToCharacter) {
        var firstSkillsToApply = new List<Tuple<Character, SingleCharacterSkill>>();
        var lastSkillsToApply = new List<Tuple<Character, SingleCharacterSkill>>();
        
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            if (skill is BonusNeutralizer or PenaltyNeutralizer) {
                lastSkillsToApply.Add(new Tuple<Character, SingleCharacterSkill>(character, skill));
            }
            else {
                firstSkillsToApply.Add(new Tuple<Character, SingleCharacterSkill>(character, skill));
            }
        }
        firstSkillsToApply.AddRange(lastSkillsToApply);
        return firstSkillsToApply.ToArray();
    }

    private void ApplySkillsJointly(Tuple<Character, SingleCharacterSkill>[] skillsPairedToCharacter) {
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            if (!skill.IsActivated) character.ApplySkill(skill, character.GameStatus);
        }
    }
    
    private Dictionary<Character, List<Tuple<EffectType, Stat, int>>> JoinPlayerSkillEffects(Dictionary<Character, SkillEffect> firstPlayerSkillEffects, Dictionary<Character, SkillEffect> secondPlayerSkillEffects) {
        var joinedSkillEffects = new Dictionary<Character, List<Tuple<EffectType, Stat, int>>>();
        foreach (var character in firstPlayerSkillEffects.Keys) {
            var firstPlayerEffects = firstPlayerSkillEffects[character];
            var secondPlayerEffects = secondPlayerSkillEffects[character];

            firstPlayerEffects.Join(secondPlayerEffects);
            var list = firstPlayerEffects.CollapseIntoList();
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
    
    private void NotifySkillEffects(Dictionary<Character,  List<Tuple<EffectType, Stat, int>>> skillEffect) {
        foreach (var character in skillEffect.Keys) {
            foreach (var effect in skillEffect[character]) {
                var effectType = effect.Item1;
                var stat = effect.Item2;
                var amount = effect.Item3;
                switch (effectType) {
                    case EffectType.FirstAttackBonus:
                        NotifyFirstAttackBonus(character, stat, amount);
                        break;
                    case EffectType.RegularBonus:
                        NotifyRegularBonus(character, stat, amount);
                        break;
                    case EffectType.FollowUpAttackBonus:
                        break;
                    case EffectType.RegularPenalty:
                        NotifyRegularPenalty(character, stat, amount);
                        break;
                    case EffectType.FirstAttackPenalty:
                        NotifyFirstAttackPenalty(character, stat, amount);
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
    
    private void NotifyFirstAttackBonus(Character character, Stat stat, int amount) {
        if (amount != 0) {
            _view.WriteLine($"{character.Name} obtiene {StatToString.RegularizeMap[stat]}+{amount} en su primer ataque");
        } 
    }
    
    private void NotifyRegularBonus(Character character, Stat stat, int amount) {
        if (amount != 0) {
            _view.WriteLine($"{character.Name} obtiene {StatToString.Map[stat]}+{amount}");
        }
    }
    
    private void NotifyRegularPenalty(Character character, Stat stat, int amount) {
        if (amount != 0) {
            _view.WriteLine($"{character.Name} obtiene {StatToString.Map[stat]}{amount}");
        }
    }
    
    private void NotifyFirstAttackPenalty(Character character, Stat stat, int amount) {
        if (amount != 0) {
            _view.WriteLine($"{character.Name} obtiene {StatToString.RegularizeMap[stat]}{amount} en su primer ataque");
        }
    }
    
    private void NotifyPenaltyNeutralizer(Character character, Stat stat) {
        _view.WriteLine($"Los penalty de {StatToString.RegularizeMap[stat]} de {character.Name} fueron neutralizados");
    }
    
    private void NotifyBonusNeutralizer(Character character, Stat stat) {
        _view.WriteLine($"Los bonus de {StatToString.RegularizeMap[stat]} de {character.Name} fueron neutralizados");
    }

    private GameStatus GetGameStatus(int activatingPlayerIndex, int rivalPlayerIndex) {
        var activatingCharacter = GetCharacterByPlayerIndex(activatingPlayerIndex);
        var rivalCharacter = GetCharacterByPlayerIndex(rivalPlayerIndex);
        var firstCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        return new GameStatus(activatingCharacter, rivalCharacter, firstCharacter, _roundPhase);
    }
    
    private void CheckIfTeamsAreEmpty() {
        if (_teams[0].Length == 0) {
            throw new TeamIsEmptyException("Player 2 ganó");
        }
        if (_teams[1].Length == 0) {
            throw new TeamIsEmptyException("Player 1 ganó");
        }
    }

    private void PrepareCharacters() {
        SetCharacters();
        ResetCharacterSkills();
    }

    private void SetCharacters() {
        SetCharacterForPlayer(_firstPlayerIndex);
        SetCharacterForPlayer(_secondPlayerIndex);
    }
    
    private void ResetCharacterSkills() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(_secondPlayerIndex);
        
        firstPlayerCharacter.ResetSkills();
        secondPlayerCharacter.ResetSkills();
    }
    
    private void SetCharacterForPlayer(int playerIndex) {
        var character = AskForCharacter(playerIndex);
        _charactersByPlayerIndex[playerIndex] = character;
    }
    
    private void WriteRoundStartMessage() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        _view.WriteLine($"Round {_round}: {firstPlayerCharacter.Name} (Player {_firstPlayerIndex + 1}) comienza");
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
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(_secondPlayerIndex);
        if (_roundPhase == 2 || _doesRoundEnd) {
            ReportHp(firstPlayerCharacter, secondPlayerCharacter);
            ChangeFirstPlayer();
            _round++;
        }
        AdvanceRoundPhase();
    }
    
    private void AdvanceRoundPhase() {
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
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(_secondPlayerIndex);
        
        switch (firstPlayerCharacter.Weapon) {
            case "Sword" when secondPlayerCharacter.Weapon == "Axe":
            case "Axe" when secondPlayerCharacter.Weapon == "Lance":
            case "Lance" when secondPlayerCharacter.Weapon == "Sword":
                _view.WriteLine($"{firstPlayerCharacter.Name} ({firstPlayerCharacter.Weapon}) tiene ventaja con respecto a {secondPlayerCharacter.Name} ({secondPlayerCharacter.Weapon})");
                break;
            case "Axe" when secondPlayerCharacter.Weapon == "Sword":
            case "Lance" when secondPlayerCharacter.Weapon == "Axe":
            case "Sword" when secondPlayerCharacter.Weapon == "Lance":
                _view.WriteLine($"{secondPlayerCharacter.Name} ({secondPlayerCharacter.Weapon}) tiene ventaja con respecto a {firstPlayerCharacter.Name} ({firstPlayerCharacter.Weapon})");
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