using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public interface IMultiCharacterSkill: IBaseSkill {
    new void Apply(RoundStatus roundStatus);
}