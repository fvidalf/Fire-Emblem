using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf;

public abstract class PenaltyNeutralizer: SingleCharacterSkill {
    
    protected List<Stat> StatsToNeutralize;

    public PenaltyNeutralizer(string name, List<Stat> statsToNeutralize)
        : base(name) {
        StatsToNeutralize = statsToNeutralize;
    }
    
    public override void Apply(RoundStatus roundStatus) {
        base.Apply(roundStatus);
        Character = roundStatus.ActivatingCharacterModel;
        SetResponseForNeutralizing();
        NeutralizeStats();
        IsActivated = true;
    }
    
    private void SetResponseForNeutralizing() {
        SkillEffect.StatEffectsByEffectType[EffectType.PenaltyNeutralizer] = new List<StatEffect>();
        foreach (var stat in StatsToNeutralize) {
            var statEffect = new StatEffect(stat, 0);
            SkillEffect.StatEffectsByEffectType[EffectType.PenaltyNeutralizer].Add(statEffect);
        }
    }
    
    private void NeutralizeStats() {
        Character.NeutralizeStats(StatsToNeutralize);
    }
}