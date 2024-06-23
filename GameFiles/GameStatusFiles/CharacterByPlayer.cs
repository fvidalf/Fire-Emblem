using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem.GameFiles.GameStatusFiles;

public class CharacterByPlayer {
    private Dictionary<int, CharacterModel> CurrentCharacterByPlayer { get; } = new Dictionary<int, CharacterModel>();

    public CharacterModel GetCharacterForPlayer(int playerIndex) {
        return CurrentCharacterByPlayer[playerIndex];
    }
    public void SetCharacterForPlayer(int playerIndex, CharacterModel characterModel) {
        CurrentCharacterByPlayer[playerIndex] = characterModel;
    }
}