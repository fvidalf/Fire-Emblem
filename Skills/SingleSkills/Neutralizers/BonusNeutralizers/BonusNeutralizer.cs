﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;

public abstract class BonusNeutralizer: Neutralizer {
    
    public BonusNeutralizer(string name, List<Stat> statsToNeutralize)
        : base(name, statsToNeutralize) {
    }

    protected override void ConcreteApply(RoundStatus roundStatus) {
        SetResponseForNeutralizing(EffectType.BonusNeutralizer);
        NeutralizeStats();
    }
    
    public override void DetermineTarget() {
        Character = RoundStatus.RivalCharacterModel;
    }
}