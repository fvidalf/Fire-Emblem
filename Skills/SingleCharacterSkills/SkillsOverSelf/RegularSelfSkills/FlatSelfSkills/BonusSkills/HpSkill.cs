using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.BonusSkills;

public class HpSkill: FlatSelfSkill {
    public HpSkill()
        : base(
            "HP +15",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Hp, 15),
                }}
            }) {}

    protected override void UpdateStat(Character character, EffectType effectType, StatEffect statEffect) {
        var characterStat = GetCharacterStat(Character, statEffect.Stat);
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);

        int newStatValue = (int) characterStatValue + statEffect.Amount;
        characterStat.SetValue(Character, newStatValue);
    }
}