using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Teams {
    private CharacterModel[][] _teams;
    private TeamsLoader _teamsLoader;
    private Dictionary<int, CharacterModel> _charactersByPlayerIndex = new Dictionary<int, CharacterModel>();
    
    public Teams(View view, string teamsFolder) {
        _teamsLoader = new TeamsLoader(view, teamsFolder);
    }
    
    public void LoadTeams() {
        _teams = _teamsLoader.GetTeams();
    }
    
    public CharacterModel GetPlayerCurrentCharacter(int playerIndex) {
        return _charactersByPlayerIndex[playerIndex];
    }
    
    public void RemoveCurrentPlayerCharacter(int playerIndex) {
        var characterToRemove = GetPlayerCurrentCharacter(playerIndex);
        var team = _teams[playerIndex];
        
        var tempTeam = team.ToList();
        tempTeam.Remove(characterToRemove);
        _teams[playerIndex] = tempTeam.ToArray();
    }
    
    public void SetCharacterForPlayer(int playerIndex, CharacterModel characterModel) {
        _charactersByPlayerIndex[playerIndex] = characterModel;
    }
    
    public CharacterModel GetCharacterFromTeam(int playerIndex, int characterIndex) {
        return _teams[playerIndex][characterIndex];
    }
    
    public CharacterModel[] GetTeam(int playerIndex) {
        return _teams[playerIndex];
    }
    
    public bool IsTeamEmpty(int playerIndex) {
        return _teams[playerIndex].Length == 0;
    }
    
}
