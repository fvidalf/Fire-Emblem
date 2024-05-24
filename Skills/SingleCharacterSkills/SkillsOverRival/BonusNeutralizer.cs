using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

public abstract class BonusNeutralizer: Neutralizer {
    
    public BonusNeutralizer(string name, List<Stat> statsToNeutralize)
        : base(name, statsToNeutralize) {
    }

    protected override void ConcreteApply(GameStatus gameStatus) {
        SetResponseForNeutralizing(EffectType.BonusNeutralizer);
        NeutralizeStats();
    }
    
    protected override void DetermineTarget(GameStatus gameStatus) {
        Character = gameStatus.RivalCharacterModel;
    }
}