using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf;
using Fire_Emblem.Skills.SkillEffectFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Game
{
    private View _view;
    private TeamsLoader _teamsLoader; 
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
        _teamsLoader = new TeamsLoader(view, teamsFolder);
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
        _teams = _teamsLoader.GetTeams();
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
    
    private void WriteRoundStartMessage() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        _view.WriteLine($"Round {_round}: {firstPlayerCharacter.Name} (Player {_firstPlayerIndex + 1}) comienza");
        WriteWeaponTriangleAdvantage();
    }
    
    private void ResetRoundParameters() {
        _roundPhase = 0;
        _doesRoundEnd = false;
    }
    
    private void HandleCombat() {
        ExecuteCommonRound(_firstPlayerIndex, _secondPlayerIndex);
        if (_doesRoundEnd) return;
        ExecuteCommonRound(_secondPlayerIndex, _firstPlayerIndex);
        if (_doesRoundEnd) return;
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
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(_secondPlayerIndex);
        SetCharacterGameStatus(firstPlayerCharacter, _firstPlayerIndex, _secondPlayerIndex);
        SetCharacterGameStatus(secondPlayerCharacter, _secondPlayerIndex, _firstPlayerIndex);
        
        ApplyCharacterSkills(firstPlayerCharacter, secondPlayerCharacter);
        HandleSkillEffectsNotification(firstPlayerCharacter, secondPlayerCharacter);
    }
    
    private Character GetCharacterByPlayerIndex(int playerIndex) {
        return _charactersByPlayerIndex[playerIndex];
    }

    private void SetCharacterGameStatus(Character character, int characterIndex, int rivalIndex) {
        var gameStatus = GetGameStatus(characterIndex, rivalIndex);
        character.ReceiveGameStatus(gameStatus);
    }
    
    private GameStatus GetGameStatus(int activatingPlayerIndex, int rivalPlayerIndex) {
        var activatingCharacter = GetCharacterByPlayerIndex(activatingPlayerIndex);
        var rivalCharacter = GetCharacterByPlayerIndex(rivalPlayerIndex);
        var firstCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        return new GameStatus(activatingCharacter, rivalCharacter, firstCharacter, _roundPhase);
    }

    private void ApplyCharacterSkills(Character firstPlayerCharacter, Character secondPlayerCharacter) {
        firstPlayerCharacter.ResetModifiedStats();
        secondPlayerCharacter.ResetModifiedStats();
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacter, secondPlayerCharacter});
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    private Tuple<Character, SingleCharacterSkill>[] JoinSkills(Character[] characters) {
        var skillsPairedToCharacter = new List<Tuple<Character, SingleCharacterSkill>>();
        foreach (var character in characters) {
            foreach (var skill in character.SingleSkills) {
                skillsPairedToCharacter.Add(new Tuple<Character, SingleCharacterSkill>(character, skill));
            }
        }
        return skillsPairedToCharacter.ToArray();
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
    
    private void HandleSkillEffectsNotification(Character firstPlayerCharacter, Character secondPlayerCharacter) {
        var firstPlayerSkillEffects = firstPlayerCharacter.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacter.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        NotifySkillEffects(skillEffects);
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
    
    private void NotifyFirstAttackSkill(Character character, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            _view.WriteLine($"{character.Name} obtiene {StatToString.RegularizeMap[stat]}{diffSign}{amount} en su primer ataque");
        } 
    }
    
    private void NotifyRegularSkill(Character character, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            _view.WriteLine($"{character.Name} obtiene {StatToString.Map[stat]}{diffSign}{amount}");
        }
    }
    
    private void NotifyPenaltyNeutralizer(Character character, Stat stat) {
        _view.WriteLine($"Los penalty de {StatToString.RegularizeMap[stat]} de {character.Name} fueron neutralizados");
    }
    
    private void NotifyBonusNeutralizer(Character character, Stat stat) {
        _view.WriteLine($"Los bonus de {StatToString.RegularizeMap[stat]} de {character.Name} fueron neutralizados");
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
    
    private void WriteWeaponTriangleAdvantage() {
        var firstPlayerCharacter = GetCharacterByPlayerIndex(_firstPlayerIndex);
        var secondPlayerCharacter = GetCharacterByPlayerIndex(_secondPlayerIndex);
        var advantageMessage = WeaponTriangleAdvantage.GetAdvantageMessage(firstPlayerCharacter, secondPlayerCharacter);
        _view.WriteLine(advantageMessage);
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