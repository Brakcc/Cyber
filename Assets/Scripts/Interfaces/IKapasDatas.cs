public interface IKapasDatas
{
    public string KapaName { get; }
    public int ID { get; }
    public int Cost { get; }
    public string Description { get; }
    public DamageEffectType EffectType { get; }
    public  KapaType KapaType { get; }
}

public enum DamageEffectType
{
    SingleDamage,
    DOT,
    Hacked,
    Buff
}

public enum KapaType
{
    NormalAttack,
    Competence,
    Ultimate,
    Turret, 
    Skip
}