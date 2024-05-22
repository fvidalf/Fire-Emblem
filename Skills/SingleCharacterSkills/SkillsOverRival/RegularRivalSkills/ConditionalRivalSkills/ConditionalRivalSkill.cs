﻿using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.RegularSkillTypes;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

// Conditional Bonus: A regular bonus skill with a condition that must be met before the bonus is applied.
public abstract class ConditionalRivalSkill: ConditionalSkill, ITargetedSkill {
    
    protected ConditionalRivalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}
    
    public override void DetermineTarget(GameStatus gameStatus) {
        Character = gameStatus.RivalCharacter;
    }
}