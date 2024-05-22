using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills;

public abstract class Neutralizer: SingleCharacterSkill {
    
    protected List<Stat> StatsToNeutralize;

    public Neutralizer(string name, List<Stat> statsToNeutralize)
        : base(name) {
        StatsToNeutralize = statsToNeutralize;
    }
    
    public override void Apply(GameStatus gameStatus) {
        base.Apply(gameStatus);
        DetermineTarget(gameStatus);
        ConcreteApply(gameStatus);
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

    protected abstract void DetermineTarget(GameStatus gameStatus);
    
    protected abstract void ConcreteApply(GameStatus gameStatus);
}