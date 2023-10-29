using System.Collections.Generic;
using UnityEngine;

public class KapaSystem
{
    #region fields
    private PathResult kapaRange = new();
    private List<Vector3Int> activeTiles = new();
    #endregion

    #region methodes
    PathResult GetKapaRange(Unit unit, HexGridStore hexGrid) => PathFind.PathKapaVerif(hexGrid, HexCoordonnees.GetClosestHex(unit.transform.position), unit.UnitData.MovePoints + 10);

    /// <summary>
    /// Compare la magnitude du vecteur entre l'Unit et une Tile parmis toutes celles d'une Kapa. 
    /// Si le nombre de Tile vers la tile de la Kapa est plus petite que (magitude + 0.5), le Tile est valide 
    /// </summary>
    /// <param name="kapaTile"></param>
    /// <param name="u"></param>
    /// <param name="hgs"></param>
    /// <returns></returns>
    public bool VerifyKapaRange(Vector3 kapaTile, Unit u, HexGridStore hgs)
    {
        kapaRange = GetKapaRange(u, hgs);
        Vector3Int uPos = HexCoordonnees.GetClosestHex(u.transform.position);

        var m = (kapaTile - uPos).magnitude + 0.5f;
        activeTiles = kapaRange.GetPathTo(HexCoordonnees.GetClosestHex(kapaTile));
        if (activeTiles.Count < m) { return true; }

        return false;
    }
    #endregion
}