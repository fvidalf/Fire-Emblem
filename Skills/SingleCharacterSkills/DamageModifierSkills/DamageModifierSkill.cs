using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills;

public abstract class DamageModifierSkill: ISingleCharacterSkill{
    
    public string Name { get; set; }
    public CharacterModel? Character;
    protected RoundStatus RoundStatus;
    
    protected DamageModifierSkill(string name) {
        Name = name;
    }

    public abstract void Apply(RoundStatus roundStatus);
    public void Reset() {
    }
}