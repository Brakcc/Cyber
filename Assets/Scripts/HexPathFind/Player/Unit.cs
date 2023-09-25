using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    public abstract int MovePoints { get; }

    public abstract void Select();
    public abstract void MoveOnPath(List<Vector3> currentPath);
    public abstract void Deselect();
}
