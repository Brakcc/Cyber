using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    public Vector3Int offsetCoords;

    void Awake() => offsetCoords = new HexCoordonnees(gameObject).offsetCoordonnees;
}
