﻿using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;

public class SpeedAndDefenceNeutralizerSkill: BonusNeutralizer {
    public SpeedAndDefenceNeutralizerSkill()
        : base("Spd/Def Neutralizer",
            new List<Stat> {Stat.SpdBonus, Stat.DefBonus}) {}
}