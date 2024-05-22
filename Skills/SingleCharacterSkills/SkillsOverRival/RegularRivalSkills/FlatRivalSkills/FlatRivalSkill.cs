using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.RegularSkillTypes;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.FlatRivalSkills;

public abstract class FlatRivalSkill: FlatSkill, ITargetedSkill {
    
    protected FlatRivalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    public override void DetermineTarget(GameStatus gameStatus) {
        Character = gameStatus.RivalCharacter;
    }
}