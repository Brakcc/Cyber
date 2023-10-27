using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    #region accessors
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
