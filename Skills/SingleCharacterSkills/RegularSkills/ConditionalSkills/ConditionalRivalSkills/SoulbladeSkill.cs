using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class SoulbladeSkill: ConditionalRivalSkill {

    public SoulbladeSkill()
        : base(
            "Wrath",
            new Dictionary<EffectType, List<StatEffect>>()
        ) {}
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        SetStatsToModify();
        DetermineTarget(roundStatus);
        if (IsConditionMet()) {
            foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
                var effectType = statEffects.Key;
                foreach (var statEffect in statEffects.Value) {
                    UpdateStat(Character, effectType, statEffect);
                }
            }
        }
    }

    protected override bool IsConditionMet() {
        return RoundStatus.ActivatingCharacterModel.Weapon == "Sword";
    }
    
    private void SetStatsToModify() {
        var averageRivalDefAndRes = (RoundStatus.RivalCharacterModel.BaseDef + RoundStatus.RivalCharacterModel.BaseRes) / 2;
        var addedDef = averageRivalDefAndRes - RoundStatus.RivalCharacterModel.BaseDef;
        var addedRes = averageRivalDefAndRes - RoundStatus.RivalCharacterModel.BaseRes;
        
        var effectTypeForDef = GetEffectTypeForDef(addedDef);
        var effectTypeForRes = GetEffectTypeForRes(addedRes);
        
        if (effectTypeForDef == effectTypeForRes) {
            StatsToModify[effectTypeForDef] = new List<StatEffect> {
                new (Stat.Def, addedDef),
                new (Stat.Res, addedRes)
            };
        } else {
            StatsToModify[effectTypeForDef] = new List<StatEffect> {
                new (Stat.Def, addedDef)
            };
        
            StatsToModify[effectTypeForRes] = new List<StatEffect> {
                new (Stat.Res, addedRes)
            };
        }
    }

    private EffectType GetEffectTypeForDef(int addedDef) {
        if (addedDef >= 0) {
            return EffectType.RegularBonus;
        } else {
            return EffectType.RegularPenalty;
        }
    }
    
    private EffectType GetEffectTypeForRes(int addedRes) {
        if (addedRes >= 0) {
            return EffectType.RegularBonus;
        } else {
            return EffectType.RegularPenalty;
        }
    }
}