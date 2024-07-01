using Fire_Emblem.Skills.SingleSkills;
namespace Fire_Emblem.Skills;

public interface ITimedSkill : ISingleSkill {
    
    TimeToApply TimeToApply { get; }
    
}