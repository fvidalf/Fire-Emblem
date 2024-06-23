using System.Collections;
using Fire_Emblem.Skills.SingleSkills;
namespace Fire_Emblem.Skills.SkillCollections;

public class SingleSkillCollection : IEnumerable<ISingleSkill> {
    
    private ISingleSkill[] Skills { get; }
    
    public SingleSkillCollection(ISingleSkill[] skills) {
        Skills = skills;
    }
    
    public IEnumerator<ISingleSkill> GetEnumerator() {
        foreach (var skill in Skills) {
            yield return skill;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
    
}