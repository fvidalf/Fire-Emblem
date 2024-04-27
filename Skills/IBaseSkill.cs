using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills;

public interface IBaseSkill {
    string Name { get; set; }
    bool IsActivated { get; set; }
    public void Apply(GameStatus gameStatus);
    
    void Reset();
}