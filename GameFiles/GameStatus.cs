namespace Fire_Emblem.GameFiles;

public class GameStatus {
    public int FirstPlayerIndex;
    public int SecondPlayerIndex;
    public int Round;
    public RoundStatus RoundStatus;
    
    public GameStatus() {
        FirstPlayerIndex = 0;
        SecondPlayerIndex = 1;
        Round = 0; 
    }
    
    public void SetRoundStatus(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
    }
    
}