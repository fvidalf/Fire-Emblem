using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.GameFiles;

public class GameStatus {
    public int FirstPlayerIndex;
    public int SecondPlayerIndex;
    private Dictionary<int, CharacterModel> _currentCharacterByPlayer = new();
    private readonly Teams _teams;
    public int Round;
    
    public GameStatus(Teams teams) {
        FirstPlayerIndex = 0;
        SecondPlayerIndex = 1;
        Round = 1;
        _teams = teams;
    }
    
    private CharacterModel GetPlayerCurrentCharacter(int playerIndex) {
        return _currentCharacterByPlayer[playerIndex];
    }
    
    public void SetCharacterForPlayer(int playerIndex, CharacterModel characterModel) {
        _currentCharacterByPlayer[playerIndex] = characterModel;
    }
    
    public void AdvanceRound() {
        Round++;
    }
    
    public void SwapPlayers() {
        FirstPlayerIndex = FirstPlayerIndex == 0 ? 1 : 0;
        SecondPlayerIndex = SecondPlayerIndex == 0 ? 1 : 0;
    }
    
    public CharacterModel GetFirstPlayerCharacter() {
        var firstPlayerCharacter = GetPlayerCurrentCharacter(FirstPlayerIndex);
        return firstPlayerCharacter;
    }
    
    public CharacterModel GetSecondPlayerCharacter() {
        var secondPlayerCharacter = GetPlayerCurrentCharacter(SecondPlayerIndex);
        return secondPlayerCharacter;
    }
    
    public void RemoveCharacter(CharacterModel character) {
        _teams.RemoveCharacter(character);
    }

    public bool HasPlayerWon(Player player) {
        var rivalPlayerIndex = GetRivalPlayerIndex(player);
        return _teams.IsTeamEmpty(rivalPlayerIndex);
    }
    
    private int GetRivalPlayerIndex(Player player) {
        var rivalPlayerIndex = ((int)player + 1) % 2;
        return rivalPlayerIndex;
    }
}