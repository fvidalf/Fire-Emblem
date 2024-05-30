using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.PenaltyNeutralizers;

public abstract class PenaltyNeutralizer: Neutralizer {

    public PenaltyNeutralizer(string name, List<Stat> statsToNeutralize)
        : base(name, statsToNeutralize) {
    }
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        SetResponseForNeutralizing(EffectType.PenaltyNeutralizer);
        NeutralizeStats();
    }
    
    public override void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
}