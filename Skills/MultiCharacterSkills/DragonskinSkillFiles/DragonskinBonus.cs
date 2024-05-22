using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills.DragonskinSkillFiles;

public class DragonskinBonus: ConditionalSelfSkill {
    
    public DragonskinBonus()
        : base("Dragonskin Bonus", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Atk, 6),
                new StatEffect(Stat.Spd, 6),
                new StatEffect(Stat.Def, 6),
                new StatEffect(Stat.Res, 6)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = GameStatus.RivalCharacter == GameStatus.FirstCharacter;
        var isRivalHpAtOrAbove75 = GameStatus.RivalCharacter.Hp >= GameStatus.RivalCharacter.BaseHp * 0.75;
        return isRivalFirstToAttack || isRivalHpAtOrAbove75;
    }
}