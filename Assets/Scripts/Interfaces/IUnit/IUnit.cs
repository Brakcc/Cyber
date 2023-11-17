using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    #region accessors
    public AUnitSO UnitData { get; }
    public float Health { get; }
    public bool IsOnTurret { get; }
    public int CompPoints { get; }
    public int UltPoints { get; }
    public bool IsDead { get; }
    public bool CanPlay { get; }
    public bool IsPersoLocked { get; }
    public Vector3Int CurrentHexPos { get; }
    #endregion

    #region methodes
    void Select();
    void MoveOnPath(List<Vector3> currentPath);
    void OnKapa();
    void Deselect();
    void OnDie();
    void OnRez();
    #endregion
}
