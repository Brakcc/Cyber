using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Hex Grid")]
public class ScriptableHexGrid : ScriptableGrid 
{

    [SerializeField,Range(1,50)] private int _gridWidth = 16;
    [SerializeField,Range(1,50)] private int _gridDepth = 9;
    
    public override Dictionary<Vector2, NodeBase> GenerateGrid() {
        var tiles = new Dictionary<Vector2, NodeBase>();
        var grid = new GameObject {
            name = "Grid"
        };
        for (var r = 0; r < _gridDepth ; r++) {
            var rOffset = r >> 1;
            for (var q = -rOffset; q < _gridWidth - rOffset; q++) {
                var tile = Instantiate(nodeBasePrefab,grid.transform);
                tile.Init(DecideIfObstacle(), new HexCoords(q,r));
                tiles.Add(tile.Coords.Pos, tile);
                Debug.Log(tile.Coords.Pos);
            }
        }
       
        return tiles;
    }
}