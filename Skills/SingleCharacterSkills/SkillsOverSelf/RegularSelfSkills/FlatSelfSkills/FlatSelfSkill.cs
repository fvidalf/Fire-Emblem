using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.RegularSkillTypes;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills;

public abstract class FlatSelfSkill: FlatSkill, ITargetedSkill {
    
    protected FlatSelfSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    public override void DetermineTarget(GameStatus gameStatus) {
        Character = gameStatus.ActivatingCharacterModel;
    }
}