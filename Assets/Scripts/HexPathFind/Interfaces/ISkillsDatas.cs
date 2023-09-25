public interface ISkillsDatas
{
    public string SkillName { get; }
    public int ID { get; }
    public int Cost { get; }
    public string Description { get; }
    public SkillEffectType EffectType { get; }
}

public enum SkillEffectType
{
    SingleDamage,
    DOT,
    hacked,
    buff
}