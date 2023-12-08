using System.Collections.Generic;
using UnityEngine;

public interface IUnit : IEntity
{
    #region accessors
    AUnitSO UnitData { get; }
    PlayerStatsUI StatUI { get; }

    #region current stats
    float CurrentHealth { get; set; }
    int CurrentMP { get; }
    int CurrentCritRate { get; }
    int CurrentDef { get; }
    int CurrentAtk { get; }
    //additional precision stat
    int CurrentPrecision { get; }
    Vector3 CurrentWorldPos { get; }
    #endregion

    int TeamNumber { get; }
    bool IsOnTurret { get; set; }
    int UltPoints { get; set; }
    bool IsDead { get; }
    bool CanPlay { get; set; }
    bool IsPersoLocked { get; set; }
    bool CanKapa { get; }
    bool IsOnComputer { get; }
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
