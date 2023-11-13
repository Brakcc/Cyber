using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public interface IKapa
{
    public List<Vector3Int> SelectGraphTiles(Unit unit, HexGridStore hexGrid, Vector3Int[] tilesArray);
    public bool Execute(Unit unit);
    public void DeselectTiles(HexGridStore hexGrid);
}

public enum KapaFunctionType
{
    DoubleAttack,
    TripleAttack,
    Grab,
    Dash,
    SelectionAttack,
    //en attente de plus
    Default
}