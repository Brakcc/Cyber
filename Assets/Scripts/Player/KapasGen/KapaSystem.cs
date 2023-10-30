using System.Collections.Generic;
using UnityEngine;

public class KapaSystem
{
    #region fields
    private PathResult kapaRange = new();
    private PathResult perfectRange = new();
    private List<Vector3Int> activeTiles = new();
    private List<Vector3Int> perfectTiles = new();
    #endregion

    #region methodes
    PathResult GetKapaRange(Unit unit, HexGridStore hexGrid) => PathFind.PathKapaVerif(hexGrid, HexCoordonnees.GetClosestHex(unit.transform.position), unit.UnitData.MovePoints + 10);
    PathResult GetPerfectPath(Unit unit, HexGridStore hexGrid) => PathFind.PerfectPath(hexGrid, HexCoordonnees.GetClosestHex(unit.transform.position), unit.UnitData.MovePoints + 10);

    /// <summary>
    /// Compare la magnitude du vecteur entre l'Unit et une Tile parmis toutes celles d'une Kapa. 
    /// Si le nombre de Tile vers la tile de la Kapa est plus petite que (magitude + 0.5), le Tile est valide 
    /// </summary>
    /// <param name="kapaTile"></param>
    /// <param name="u"></param>
    /// <param name="hgs"></param>
    /// <returns></returns>
    public bool VerifyKapaRange(Vector3Int kapaTile, Unit u, HexGridStore hgs)
    {
        kapaRange = GetKapaRange(u, hgs);
        perfectRange = GetPerfectPath(u, hgs);

        activeTiles = kapaRange.GetPathTo(HexCoordonnees.GetClosestHex(kapaTile));
        perfectTiles = perfectRange.GetPathTo(HexCoordonnees.GetClosestHex(kapaTile));
        if (activeTiles.Count == perfectTiles.Count) { return true; }

        return false;
    }
    #endregion
}