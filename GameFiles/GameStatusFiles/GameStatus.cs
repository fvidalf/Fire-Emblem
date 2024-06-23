using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem.GameFiles.GameStatusFiles;

public class GameStatus(Teams teams) {
    public int FirstPlayerIndex;
    public int SecondPlayerIndex = 1;
    private readonly CharacterByPlayer _currentCharacterByPlayer = new CharacterByPlayer();
    public int Round = 1;
    
    public CharacterModel GetFirstPlayerCharacter() {
        var firstPlayerCharacter = GetPlayerCurrentCharacter(FirstPlayerIndex);
        return firstPlayerCharacter;
    }
    
    private CharacterModel GetPlayerCurrentCharacter(int playerIndex) {
        return _currentCharacterByPlayer.GetCharacterForPlayer(playerIndex);
    }
    
    public CharacterModel GetSecondPlayerCharacter() {
        var secondPlayerCharacter = GetPlayerCurrentCharacter(SecondPlayerIndex);
        return secondPlayerCharacter;
    }
    
    public void SetCharacterForPlayer(int playerIndex, CharacterModel characterModel) {
        _currentCharacterByPlayer.SetCharacterForPlayer(playerIndex, characterModel);
    }
    
    public void RemoveCharacter(CharacterModel character) {
        teams.RemoveCharacter(character);
    }
    
    public bool HasPlayerWon(Player player) {
        var rivalPlayerIndex = GetRivalPlayerIndex(player);
        return teams.IsTeamEmpty(rivalPlayerIndex);
    }
    
    private static int GetRivalPlayerIndex(Player player) {
        var rivalPlayerIndex = ((int)player + 1) % 2;
        return rivalPlayerIndex;
    }
    
    public void AdvanceRound() {
        Round++;
    }
    
    public void SwapPlayers() {
        FirstPlayerIndex = FirstPlayerIndex == 0 ? 1 : 0;
        SecondPlayerIndex = SecondPlayerIndex == 0 ? 1 : 0;
    }
}