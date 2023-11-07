using System.Collections.Generic;
using UnityEngine;

public class OutlineSystem
{
    #region fields
    #endregion

    #region methodes
    PathResult GetOutlineRange(Vector3Int pos, HexGridStore hexGrid, int range) => PathFind.PathKapaVerif(hexGrid, pos, range);
    
    /// <summary>
    /// revoit le dictionnaire contenant en cl� les Pos et en value les Hex d'une range � outline. 
    /// </summary>
    /// <param name="pos">position centrale de la range</param>
    /// <param name="hexGrid">ref au HexGridStore, qui contient le dictionnaire de la map</param>
    /// <param name="range">taille en la range � calculer</param>
    /// <returns></returns>
    public Dictionary<Vector3Int, Hex> GetOutlineList(Vector3Int pos, HexGridStore hexGrid, int range)
    {
        Dictionary<Vector3Int, Hex> outlinedTiles = new();
        PathResult outlineRange = GetOutlineRange(pos, hexGrid, range);
        foreach (var i in outlineRange.GetRangePositions())
        {
            outlinedTiles[i] = hexGrid.GetTile(i);
        }
        return outlinedTiles;
    }
    #endregion
}