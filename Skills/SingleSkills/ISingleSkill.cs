using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SingleSkills;

public interface ISingleSkill: IBaseSkill {
    new void Apply(RoundStatus roundStatus);
    new void Reset();
}