using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills;

public interface ITargetedSkill {
    
    void DetermineTarget(Character character);
}