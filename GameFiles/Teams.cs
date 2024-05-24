using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Teams {
    private CharacterModel[][] _teams;
    private Dictionary<int, CharacterModel> _charactersByPlayerIndex = new Dictionary<int, CharacterModel>();
    
    public Teams(View view, string teamsFolder) {
        var teamsLoader = new TeamsLoader(view, teamsFolder);
        _teams = teamsLoader.GetTeams();
    }
    
    public CharacterModel GetCharacterByPlayerIndex(int playerIndex) {
        return _charactersByPlayerIndex[playerIndex];
    }
    
    public void RemoveCurrentPlayerCharacter(int playerIndex) {
        var characterToRemove = GetCharacterByPlayerIndex(playerIndex);
        var team = _teams[playerIndex];
        
        var tempTeam = team.ToList();
        tempTeam.Remove(characterToRemove);
        _teams[playerIndex] = tempTeam.ToArray();
    }
    
    public void SetCharacterForPlayer(int playerIndex, CharacterModel characterModel) {
        _charactersByPlayerIndex[playerIndex] = characterModel;
    }
    
}
