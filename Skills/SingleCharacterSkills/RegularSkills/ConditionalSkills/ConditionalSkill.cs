﻿using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills;

public abstract class ConditionalSkill: RegularSkill, ITargetedSkill {
    
    protected ConditionalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        DetermineTarget(roundStatus);
        if (IsConditionMet()) {
            Console.WriteLine($"{roundStatus.ActivatingCharacterModel.Name} aplica efectivamente {Name}");
            foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
                var effectType = statEffects.Key;
                foreach (var statEffect in statEffects.Value) {
                    UpdateStat(Character, effectType, statEffect);
                }
            }
        }
    }

    public abstract void DetermineTarget(RoundStatus roundStatus);
    
    protected abstract bool IsConditionMet();
}