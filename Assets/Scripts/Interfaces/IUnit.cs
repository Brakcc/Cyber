using System.Collections.Generic;
using System;
using UnityEngine;

public interface IUnit
{
    #region accessors
    int MovePoints { get; }
    int Speed { get; }
    int HealthPoint { get; }
    bool IsDead { get; }
    bool CanPlay { get; }
    bool IsPersoLocked {  get; }
    #endregion

    #region methodes
    void Select();
    void MoveOnPath(List<Vector3> currentPath);
    void OnKapa();
    void Deselect();
    #endregion
}
