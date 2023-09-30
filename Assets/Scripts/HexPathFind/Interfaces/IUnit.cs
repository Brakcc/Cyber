using System.Collections.Generic;
using System;
using UnityEngine;

public interface IUnit
{
    int MovePoints { get; }
    int Speed { get; }

    void Select();
    void MoveOnPath(List<Vector3> currentPath);
    void Deselect();
}
