using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills;

public interface ITargetedSkill {
    
    void DetermineTarget(GameStatus gameStatus);
}