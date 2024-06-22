using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.GameFiles;

public class Team {
    private CharacterModel[] _team;
    
    public Team() {
        _team = Array.Empty<CharacterModel>();
    }
    
    public void AddCharacter(CharacterModel character) {
        var tempTeam = _team.ToList();
        tempTeam.Add(character);
        _team = tempTeam.ToArray();
    }
    
    public void RemoveCharacter(CharacterModel character) {
        var tempTeam = _team.ToList();
        tempTeam.Remove(character);
        _team = tempTeam.ToArray();
    }
    
    public bool Contains(CharacterModel character) {
        return _team.Contains(character);
    }
    
    public CharacterModel GetCharacterByIndex(int index) {
        return _team[index];
    }
    
    public bool IsEmpty() {
        return _team.Length == 0;
    }
    
    public int Length() {
        return _team.Length;
    }
}