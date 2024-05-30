using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.FirstAttackSkills;

public class LunaSkill: FirstAttackSkill {

    public LunaSkill()
        : base("Luna") { }

    protected override void ConcreteApply() {
        var characterDef = Character.BaseDef;
        var decreaseInDef = (double) - 0.5d * characterDef;
        var newDefEffect = new StatEffect(Stat.FirstAttackDef, (int) decreaseInDef);
        UpdateStat(Character, EffectType.FirstAttackPenalty, newDefEffect);
        
        var characterRes = Character.BaseRes;
        var decreaseInRes = (double) - 0.5d * characterRes;
        var newResEffect = new StatEffect(Stat.FirstAttackRes, (int) decreaseInRes);
        UpdateStat(Character, EffectType.FirstAttackPenalty, newResEffect);
    }

    public override void DetermineTarget() {
        Character = RoundStatus.RivalCharacterModel;
    }
}