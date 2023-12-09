using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnitSO : ScriptableObject, IUnitData
{
    #region fields/accessors to herit
    public abstract int ID { get; }
    public abstract string Name { get; }
    public abstract UnitType Type { get; }
    public abstract int NetworkRange { get; }
    public abstract int MovePoints { get; }
    public abstract int Attack { get; }
    public abstract int Defense { get; }
    public abstract int CritRate { get; }
    public abstract int Speed { get; }
    public abstract float HealthPoint { get; }
    public abstract Sprite Sprite { get; set; }
    public abstract List<AbstractKapaSO> KapasList { get; set; }
    #endregion
}
