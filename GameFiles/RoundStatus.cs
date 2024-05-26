using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.GameFiles;

public class RoundStatus {
    public CharacterModel ActivatingCharacterModel { get; private set; }
    public CharacterModel RivalCharacterModel { get; private set; }
    public CharacterModel FirstCharacterModel { get; private set; }
    public int RoundPhase { get; private set; }
    
    public RoundStatus(CharacterModel activatingCharacterModel, CharacterModel rivalCharacterModel, CharacterModel firstCharacterModel, int roundPhase) {
        ActivatingCharacterModel = activatingCharacterModel;
        RivalCharacterModel = rivalCharacterModel;
        FirstCharacterModel = firstCharacterModel;
        RoundPhase = roundPhase;
    }
}