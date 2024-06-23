using System.Collections;
namespace Fire_Emblem.Skills.SkillCollections;

public class BaseSkillCollection : IEnumerable<IBaseSkill> {
    
    private IBaseSkill[] Skills { get; }
    
    public BaseSkillCollection(IBaseSkill[] skills) {
        Skills = skills;
    }
    
    public IEnumerator<IBaseSkill> GetEnumerator() {
        foreach (var skill in Skills) {
            yield return skill;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
    
}