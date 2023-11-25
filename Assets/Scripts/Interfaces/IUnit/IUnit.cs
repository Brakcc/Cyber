using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    #region accessors
    public AUnitSO UnitData { get; }
    public PlayerStatsUI StatUI { get; }
    #region current stats
    public float CurrentHealth { get; }
    public int CurrentMP { get; }
    public int CurrentCritRate { get; }
    public int CurrentDef { get; }
    public int CurrentAtk { get; }
    #endregion
    public bool IsOnTurret { get; }
    public int CompPoints { get; }
    public int UltPoints { get; }
    public bool IsDead { get; }
    public bool CanPlay { get; }
    public bool IsPersoLocked { get; }
    public bool CanKapa { get; }
    public Vector3Int CurrentHexPos { get; }
    #endregion

    #region methodes
    void OnInit();
    void Select();
    void MoveOnPath(List<Vector3> currentPath);
    void OnKapa();
    void Deselect();
    void OnDie();
    void OnRez();
    #endregion
}
