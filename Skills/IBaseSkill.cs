using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills;

public interface IBaseSkill {
    string Name { get; set; }
    public void Apply(RoundStatus roundStatus);
}
