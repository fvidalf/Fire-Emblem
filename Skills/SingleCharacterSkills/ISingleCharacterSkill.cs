﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills;

public interface ISingleCharacterSkill: IBaseSkill {
    new void Apply(GameStatus gameStatus);
    SkillEffect SkillEffect { get; set; }
}