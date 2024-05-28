using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Teams {
    private CharacterModel[][] _teams;
    private TeamsLoader _teamsLoader;
    
    public Teams(View view, string teamsFolder) {
        _teamsLoader = new TeamsLoader(view, teamsFolder);
    }
    
    public void LoadTeams() {
        _teams = _teamsLoader.GetTeams();
    }

    public void RemoveCharacter(CharacterModel character) {
        var teamIndexToRemoveFrom = FindTeamIndexWithCharacter(character);
        var teamToRemoveFrom = _teams[teamIndexToRemoveFrom];
        
        var tempTeam = teamToRemoveFrom.ToList();
        tempTeam.Remove(character);
        _teams[teamIndexToRemoveFrom] = tempTeam.ToArray();
    }
    
    private int FindTeamIndexWithCharacter(CharacterModel character) {
        for (var i = 0; i < _teams.Length; i++) {
            if (_teams[i].Contains(character)) {
                return i;
            }
        }
        return -1;
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
