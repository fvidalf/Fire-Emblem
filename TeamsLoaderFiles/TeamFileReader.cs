namespace Fire_Emblem.TeamsLoaderFiles;

public class TeamFilesReader {
    
    private readonly string _teamsFolder;
    public TeamFilesReader(string teamsFolder) {
        _teamsFolder = teamsFolder;
    }
    
    public string[] GetAllFileNames() {
        var files = GetAllFiles();
        var fileNames = new string[files.Length];
        for (var i = 0; i < files.Length; i++) {
            fileNames[i] = Path.GetFileName(files[i]);
        }
        return fileNames;
    }
    
    private string[] GetAllFiles() {
        return Directory.GetFiles(_teamsFolder);
    }
    
    public string[] ReadTeamFile(int option) {
        var lines = GetFileLinesFromOption(option);
        return lines;
    }
    
    private string[] GetFileLinesFromOption(int option) {
        var file = GetFileFromOption(option);
        var fileContent = File.ReadAllText(file);
        var lines = fileContent.Trim().Split("\n");
        return lines;
    }

    private string GetFileFromOption(int option) {
        var files = GetAllFiles();
        return files[option];
    }
}