using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills;

public interface IHasSkillEffect {
    
    public Dictionary<CharacterModel, SkillEffect> GetSkillEffect();
    
}