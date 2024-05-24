using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival.FirstAttackRivalSkills;

public class LunaSkill: FirstAttackSkill {

    public LunaSkill()
        : base("Luna") { }

    protected override void ConcreteApply() {
        var characterDef = GetCharacterStat(Character, Stat.Def);
        var characterDefValue = GetCharacterStatValue(characterDef, Stat.Def);
        var decreaseInDef = (double) - 0.5d * characterDefValue;
        var newDefEffect = new StatEffect(Stat.FirstAttackDef, (int) decreaseInDef);
        UpdateStat(Character, EffectType.FirstAttackPenalty, newDefEffect);
        
        var characterRes = GetCharacterStat(Character, Stat.Res);
        var characterResValue = GetCharacterStatValue(characterRes, Stat.Res);
        var decreaseInRes = (double) - 0.5d * characterResValue;
        var newResEffect = new StatEffect(Stat.FirstAttackRes, (int) decreaseInRes);
        UpdateStat(Character, EffectType.FirstAttackPenalty, newResEffect);
    }

    public override void DetermineTarget(GameStatus gameStatus) {
        Character = gameStatus.RivalCharacterModel;
    }
}