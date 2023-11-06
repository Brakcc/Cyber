public interface IKapasDatas
{
    public string KapaName { get; }
    public int ID { get; }
    public int Cost { get; }
    public string Description { get; }
    public EffectType EffectType { get; }
    public KapaType KapaType { get; }
    public KapaFunctionType KapaFunctionType { get; }
}

public enum EffectType
{
    SingleDamage,
    DOT,
    Hacked,
    Buff,
    Rez,
    None
}

public enum KapaType
{
    NormalAttack,
    Competence,
    Ultimate,
    Turret, 
    Skip, 
    Default
}