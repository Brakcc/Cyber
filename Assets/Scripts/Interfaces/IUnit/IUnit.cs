using System.Collections.Generic;
using UnityEngine;

public interface IUnit : IEntity
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
    public int TeamNumber { get; }
    public bool IsOnTurret { get; }
    public int UltPoints { get; }
    public bool IsDead { get; }
    public bool CanPlay { get; }
    public bool IsPersoLocked { get; }
    public bool CanKapa { get; }
    public bool IsOnComputer { get; }
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
