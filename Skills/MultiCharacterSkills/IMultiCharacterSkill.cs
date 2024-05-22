using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public interface IMultiCharacterSkill: IBaseSkill {
    new void Apply(GameStatus gameStatus);
}