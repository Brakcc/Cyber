using System.Collections.Generic;
using System;
using UnityEngine;

public interface IUnit
{
    int MovePoints { get; }

    void Select();
    void MoveOnPath(List<Vector3> currentPath);
    void Deselect();
}
