using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills.DragonskinSkillFiles;

public class DragonskinNeutralize: Neutralizer {
    
    public DragonskinNeutralize()
        : base("Dragonskin Neutralize",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus, Stat.ResBonus, Stat.DefBonus}) {}

    protected override void ConcreteApply(RoundStatus roundStatus) {
        if (IsConditionMet()) {
            SetResponseForNeutralizing(EffectType.BonusNeutralizer);
            NeutralizeStats();
        }
    }

    protected override void DetermineTarget(RoundStatus roundStatus) {
        Character = roundStatus.RivalCharacterModel;
    }
    
    private bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalHpAtOrAbove75 = RoundStatus.RivalCharacterModel.Hp >= RoundStatus.RivalCharacterModel.BaseHp * 0.75;
        return isRivalFirstToAttack || isRivalHpAtOrAbove75;
    }
}