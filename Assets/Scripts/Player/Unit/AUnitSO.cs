using System.Collections.Generic;
using UnityEngine;

public abstract class AUnitSO : ScriptableObject, IUnitData
{
    #region fields/accessors to herit
    public abstract int MovePoints { get; }
    public abstract int Speed { get; }
    public abstract int HealthPoint { get; set; }
    public abstract Sprite Sprite { get; set; }
    public abstract List<AKapaSO> KapasList { get; set; }
    #endregion
}
