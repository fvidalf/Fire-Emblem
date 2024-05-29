using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class BeliefInLoveSkill: ConditionalRivalSkill {
    public BeliefInLoveSkill()
        : base("Belief in Love", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Atk, -5),
                new StatEffect(Stat.Def, -5)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = Character == RoundStatus.FirstCharacterModel;
        var isRivalFullHp = Character.Hp == Character.BaseHp;
        Console.WriteLine(isRivalFirstToAttack && isRivalFullHp);
        return isRivalFirstToAttack || isRivalFullHp;
    }
}