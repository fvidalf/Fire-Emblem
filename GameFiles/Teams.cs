using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.TeamsLoaderFiles;

namespace Fire_Emblem.GameFiles;

public class Teams {
    private Team[] _teams;
    private TeamsLoader _teamsLoader;
    
    public Teams(View view, string teamsFolder) {
        _teamsLoader = new TeamsLoader(view, teamsFolder);
    }
    
    public void LoadTeams() {
        _teams = _teamsLoader.GetTeams();
    }

    /*
    public void RemoveCharacter(CharacterModel character) {
        var teamIndexToRemoveFrom = FindTeamIndexWithCharacter(character);
        var teamToRemoveFrom = _teams[teamIndexToRemoveFrom];
        
        var tempTeam = teamToRemoveFrom.ToList();
        tempTeam.Remove(character);
        _teams[teamIndexToRemoveFrom] = tempTeam.ToArray();
    }
    */

    public void RemoveCharacter(CharacterModel character) {
        var teamIndexToRemoveFrom = FindTeamIndexWithCharacter(character);
        var teamToRemoveFrom = _teams[teamIndexToRemoveFrom];
        teamToRemoveFrom.RemoveCharacter(character);
    }
    
    
    private int FindTeamIndexWithCharacter(CharacterModel character) {
        for (var i = 0; i < _teams.Length; i++) {
            var currentTeam = _teams[i];
            if (currentTeam.Contains(character)) {
                return i;
            }
        }
        return -1;
    }
    
    public CharacterModel GetCharacterFromTeam(int playerIndex, int characterIndex) {
        var team = _teams[playerIndex];
        return team.GetCharacterByIndex(characterIndex);
    }
    
    public Team GetTeam(int playerIndex) {
        return _teams[playerIndex];
    }
    
    public bool IsTeamEmpty(int playerIndex) {
        var team = _teams[playerIndex];
        return team.IsEmpty();
    }
}
