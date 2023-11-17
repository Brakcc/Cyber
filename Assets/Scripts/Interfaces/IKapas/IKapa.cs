using System.Collections.Generic;
using UnityEngine;

public interface IKapa
{
    public List<Vector3Int> OnSelectGraphTiles(Unit unit, HexGridStore hexGrid, Vector3Int[] tilesArray);
    public void OnDeselectTiles(HexGridStore hexGrid);
    public void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, Unit unit);
    public List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, Unit unit);
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