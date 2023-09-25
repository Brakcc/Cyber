using UnityEngine;

public abstract class ScriptableSkill : ScriptableObject, ISkillsDatas
{
    public abstract string SkillName { get; }
    public abstract int ID { get; }
    public abstract string Description { get; }
    public abstract int Cost { get; }
    public abstract SkillEffectType EffectType { get; }
}
