using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BrazenSkills;

public class BrazenAttackAndDefenseSkill : BrazenSkill {

    public BrazenAttackAndDefenseSkill()
        : base(
            "Brazen Atk/Def",
            new Dictionary<EffectType, List<StatEffect>> {
                {
                    EffectType.RegularBonus, new List<StatEffect> {
                        new StatEffect(Stat.Atk, 10),
                        new StatEffect(Stat.Def, 10)
                    }
                }
            }) {}
}
            