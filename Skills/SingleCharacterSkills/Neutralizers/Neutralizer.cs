using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers;

public abstract class Neutralizer: SingleCharacterSkill, ITargetedSkill {
    
    protected List<Stat> StatsToNeutralize;

    public Neutralizer(string name, List<Stat> statsToNeutralize)
        : base(name) {
        StatsToNeutralize = statsToNeutralize;
    }
    
    public override void Apply(RoundStatus roundStatus) {
        base.Apply(roundStatus);
        DetermineTarget(roundStatus);
        ConcreteApply(roundStatus);
        IsActivated = true;
    }
    
    protected void SetResponseForNeutralizing(EffectType effectType) {
        SkillEffect.StatEffectsByEffectType[effectType] = new List<StatEffect>();
        foreach (var stat in StatsToNeutralize) {
            var statEffect = new StatEffect(stat, 0);
            SkillEffect.StatEffectsByEffectType[effectType].Add(statEffect);
        }
    }
    
    protected void NeutralizeStats() {
        Character.NeutralizeStats(StatsToNeutralize);
    }

    public abstract void DetermineTarget(RoundStatus roundStatus);
    
    protected abstract void ConcreteApply(RoundStatus roundStatus);
}