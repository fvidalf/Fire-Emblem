using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class HpSkill: FlatSelfSkill {
    public HpSkill()
        : base(
            "HP +15",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Hp, 15),
                }}
            }) {}

    protected override void UpdateStat(CharacterModel characterModel, EffectType effectType, StatEffect statEffect) {
        if (Character.HasUsedHpSkill) return; 
        Character.HasUsedHpSkill = true;
        var characterStat = GetCharacterStat(Character, statEffect.Stat);
        var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
        UpdateCharacterStat(characterStat, Character, newStatEffect);
    }
}