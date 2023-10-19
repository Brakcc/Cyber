using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    #region fields
    private PathResult moveRange = new();
    private List<Vector3Int> currentPath = new();
    #endregion

    #region methodes
    public void HideRange(HexGridStore hexGrid)
    {
        foreach (Vector3Int hexPos in moveRange.GetRangePositions()) { hexGrid.GetTile(hexPos).DisableGlowPath(); hexGrid.GetTile(hexPos).DisableGlow(); }
        moveRange = new PathResult();
    }

    public void ShowRange(Unit selected, HexGridStore hexGrid)
    {
        CalculateRange(selected, hexGrid);
        Vector3Int unitPos = HexCoordonnees.GetClosestHex(selected.transform.position);

        foreach (Vector3Int hexPos in moveRange.GetRangePositions()) 
        {
            if (unitPos == hexPos) continue;
            hexGrid.GetTile(hexPos).EnableGlow(); 
        }
    }

    public void CalculateRange(Unit selects, HexGridStore hexGrid) => moveRange = PathFind.PathGetRange(hexGrid, HexCoordonnees.GetClosestHex(selects.transform.position), selects.MovePoints);

    public void ShowPath(Vector3Int selects,  HexGridStore hexGrid)
    {
        if (moveRange.GetRangePositions().Contains(selects))
        {
            foreach (Vector3Int hexPos in currentPath) { hexGrid.GetTile(hexPos).DisableGlowPath(); }
            currentPath = moveRange.GetPathTo(selects);
            foreach (Vector3Int hexPos in currentPath) { hexGrid.GetTile(hexPos).EnableGlowPath(); }
        }
    }

    public void MoveUnit(Unit selects, HexGridStore hexGrid) => selects.MoveOnPath(currentPath.Select(pos => hexGrid.GetTile(pos).transform.position).ToList());

    public bool IsHexInRange(Vector3Int hexPos) => moveRange.IsHexPosInRange(hexPos);
    #endregion
}
