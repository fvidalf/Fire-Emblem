namespace Fire_Emblem.GameFiles;

public class PlayerHasWonException : Exception {
    public PlayerHasWonException(string message) : base(message) { }
}