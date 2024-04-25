using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem;

public class GameStatus {
    public Character ActivatingCharacter { get; private set; }
    public Character RivalCharacter { get; private set; }
    public Character FirstCharacter { get; private set; }
    public int RoundPhase { get; private set; }
    
    public GameStatus(Character activatingCharacter, Character rivalCharacter, Character firstCharacter, int roundPhase) {
        ActivatingCharacter = activatingCharacter;
        RivalCharacter = rivalCharacter;
        FirstCharacter = firstCharacter;
        RoundPhase = roundPhase;
    }
}