﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class TomePrecisionSkill: ConditionalSelfSkill {
    
    public TomePrecisionSkill()
        : base(
            "Tome Precision", 
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 6),
                    new StatEffect(Stat.Spd, 6)
                }}
            }) {}

    protected override bool IsConditionMet() {
        return Character.Weapon == "Magic";
    }
}