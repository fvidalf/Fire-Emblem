﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf;

public abstract class PenaltyNeutralizer: BaseSkill {
    
    protected List<Stat> StatsToNeutralize;

    public PenaltyNeutralizer(string name, List<Stat> statsToNeutralize)
        : base(name) {
        StatsToNeutralize = statsToNeutralize;
    }
    
    public override void Apply(GameStatus gameStatus) {
        base.Apply(gameStatus);
        Character = gameStatus.ActivatingCharacter;
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