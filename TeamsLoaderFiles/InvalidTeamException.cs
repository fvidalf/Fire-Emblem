namespace Fire_Emblem.TeamsLoaderFiles;

public class InvalidTeamException: Exception {
    public override string Message => "Archivo de equipos no válido";
}