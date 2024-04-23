namespace Fire_Emblem.GameFiles;

public class TeamIsEmptyException : Exception {
    public TeamIsEmptyException(string message) : base(message) { }
}