using UnityEngine;

public abstract class AKapaSO : ScriptableObject, IKapa, IKapasDatas
{
    #region fields to herit
    public abstract string KapaName { get; }
    public abstract int ID { get; }
    public abstract int Cost { get; }
    public abstract string Description { get; }
    public abstract DamageEffectType EffectType { get; }
    public abstract KapaType KapaType { get; }
    public abstract KapaUISO KapaUI { get; }
    #endregion

    #region methodes to herit
    public virtual void DeselectTiles() => Debug.Log("Lel");
    public abstract void Execute();
    public virtual void SelectTiles() => Debug.Log("Lul");
    #endregion
}