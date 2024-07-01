using Fire_Emblem.CharacterFiles.StatFiles;
namespace Fire_Emblem.Skills.SkillEffectFiles.SortedEffectsFiles;

public class SimpleEffect
{
    public EffectType Effect { get; }
    public Stat Stat { get; }
    public int Amount { get; set; }

    public SimpleEffect(EffectType effect, Stat stat, int amount) {
        Effect = effect;
        Stat = stat;
        Amount = amount;
    }
    
    public void Deconstruct(out EffectType effect, out Stat stat, out int amount) {
        effect = Effect;
        stat = Stat;
        amount = Amount;
    }
}