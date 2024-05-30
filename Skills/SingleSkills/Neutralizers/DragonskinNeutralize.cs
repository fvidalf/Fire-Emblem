using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.Neutralizers;

public class DragonskinNeutralize: BonusNeutralizer {
    
    public DragonskinNeutralize()
        : base("Dragonskin Neutralize",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus, Stat.ResBonus, Stat.DefBonus}) {}

    protected override void ConcreteApply(RoundStatus roundStatus) {
        if (IsConditionMet()) {
            SetResponseForNeutralizing(EffectType.BonusNeutralizer);
            NeutralizeStats();
        }
    }
    
    private bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalHpAtOrAbove75 = RoundStatus.RivalCharacterModel.Hp >= RoundStatus.RivalCharacterModel.BaseHp * 0.75;
        return (isRivalFirstToAttack || isRivalHpAtOrAbove75) && RoundStatus.RoundPhase == 0;
    }
}