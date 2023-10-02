using UnityEngine;

public abstract class ScriptableDataKapa : ScriptableObject, IKapasDatas
{
    public abstract string KapaName { get; }
    public abstract int ID { get; }
    public abstract string Description { get; }
    public abstract int Cost { get; }
    public abstract DamageEffectType EffectType { get; }
    public abstract KapaType KapaType { get; }
}
