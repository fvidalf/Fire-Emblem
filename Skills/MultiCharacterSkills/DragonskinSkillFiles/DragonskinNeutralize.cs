using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills.DragonskinSkillFiles;

public class DragonskinNeutralize: Neutralizer {
    
    public DragonskinNeutralize()
        : base("Dragonskin Neutralize",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus, Stat.ResBonus, Stat.DefBonus}) {}

    protected override void ConcreteApply(GameStatus gameStatus) {
        if (IsConditionMet()) {
            SetResponseForNeutralizing(EffectType.BonusNeutralizer);
            NeutralizeStats();
        }
    }

    protected override void DetermineTarget(GameStatus gameStatus) {
        Character = gameStatus.RivalCharacter;
    }
    
    private bool IsConditionMet() {
        var isRivalFirstToAttack = GameStatus.RivalCharacter == GameStatus.FirstCharacter;
        var isRivalHpAtOrAbove75 = GameStatus.RivalCharacter.Hp >= GameStatus.RivalCharacter.BaseHp * 0.75;
        return isRivalFirstToAttack || isRivalHpAtOrAbove75;
    }
}