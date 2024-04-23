using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem;

public class GameStatus {
    public Character AttackingCharacter { get; private set; }
    public Character DefendingCharacter { get; private set; }
    public int RoundPhase { get; private set; }
    
    public void SetGameStatus(Character attackingCharacter, Character defendingCharacter, int roundPhase) {
        AttackingCharacter = attackingCharacter;
        DefendingCharacter = defendingCharacter;
        RoundPhase = roundPhase;
    }
    
    public void AdvancePhase() { 
        RoundPhase++;
    }
    
    public void SwapCharacters() {
        (AttackingCharacter, DefendingCharacter) = (DefendingCharacter, AttackingCharacter);
    }
}