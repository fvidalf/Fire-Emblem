using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills;

public abstract class FlatSelfSkill: FlatSkill{
    
    protected FlatSelfSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    public override void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
}