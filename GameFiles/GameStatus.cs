namespace Fire_Emblem.GameFiles;

public class GameStatus {
    public int FirstPlayerIndex;
    public int SecondPlayerIndex;
    public int Round;
    
    public GameStatus() {
        FirstPlayerIndex = 0;
        SecondPlayerIndex = 1;
        Round = 1; 
    }
    
    public void AdvanceRound() {
        Round++;
    }
    
    public void SwapPlayers() {
        FirstPlayerIndex = FirstPlayerIndex == 0 ? 1 : 0;
        SecondPlayerIndex = SecondPlayerIndex == 0 ? 1 : 0;
    }
}