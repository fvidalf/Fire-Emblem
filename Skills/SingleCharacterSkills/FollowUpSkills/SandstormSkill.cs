using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.FollowUpSkills;

public class SandstormSkill: FollowUpSkill {
    
    public SandstormSkill()
        : base("Sandstorm") {
    }
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        var amplifiedDef = roundStatus.ActivatingCharacterModel.BaseDef * 1.5;
        var baseAtk = roundStatus.ActivatingCharacterModel.BaseAtk;
        var addedAtk = amplifiedDef - baseAtk;
        
        var effectTypeForAtk = GetEffectTypeForAtk(addedAtk);
        var newStatEffect = new StatEffect(Stat.FollowUpAtk, (int) Math.Floor(addedAtk));
        UpdateStat(Character, effectTypeForAtk, newStatEffect);
    }
    
    private EffectType GetEffectTypeForAtk(double addedAtk) {
        if (addedAtk >= 0) {
            return EffectType.FollowUpBonus;
        } else {
            return EffectType.FollowUpPenalty;
        }
    }
    
    public override void DetermineTarget(RoundStatus roundStatus) {
        Character = roundStatus.ActivatingCharacterModel;
    }
}