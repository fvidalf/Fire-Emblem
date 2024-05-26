﻿using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills;

// Regular Bonus: A bonus skill that is applied at the start of the turn and lasts indefinitely.
public abstract class RegularSkill: SingleCharacterSkill {
    
    protected Dictionary<EffectType, List<StatEffect>> StatsToModify;
    
    protected RegularSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name) {
        StatsToModify = statsToModify;
    }
    
    public override void Apply(RoundStatus roundStatus) {
        base.Apply(roundStatus);
        ConcreteApply(roundStatus);
        IsActivated = true;
    }
    
    protected abstract void ConcreteApply(RoundStatus roundStatus);
}