using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem;

public class GameStatus {
    public Character AttackingCharacter { get; private set; }
    public Character DefendingCharacter { get; private set; }
    public int Round { get; private set; }
    public int RoundPhase { get; private set; }
    
    public GameStatus(Character attackingCharacter, Character defendingCharacter, int round, int roundPhase) {
        AttackingCharacter = attackingCharacter;
        DefendingCharacter = defendingCharacter;
        Round = round;
        RoundPhase = roundPhase;
    }
    
    public void HandlePhaseAdvance() {
        if (RoundPhase == 2) {
            Round++;
            RoundPhase = 1;
            SwapCharacters();
        } else {
            RoundPhase++;
        }
    }
    
    private void SwapCharacters() {
        (AttackingCharacter, DefendingCharacter) = (DefendingCharacter, AttackingCharacter);
    }
}