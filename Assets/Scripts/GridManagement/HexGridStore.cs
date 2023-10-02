using System.Collections.Generic;
using UnityEngine;

public class HexGridStore: MonoBehaviour
{
    #region fields
    public Dictionary<Vector3Int, Hex> hexTiles = new Dictionary<Vector3Int, Hex>();
    private Dictionary<Vector3Int, List<Vector3Int>> neighbourgs = new Dictionary<Vector3Int, List<Vector3Int>>();
    #endregion

    #region methodes
    void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hexTiles[hex.hexCoords] = hex;
        }
    }

    public Hex GetTile(Vector3Int hexCoords)
    {
        Hex results = null;
        hexTiles.TryGetValue(hexCoords, out results);
        return results;
    }

    public List<Vector3Int> GetNeighbourgs(Vector3Int coords)
    {
        if (!hexTiles.ContainsKey(coords)) { return new List<Vector3Int>(); }
        if (neighbourgs.ContainsKey(coords)) { return neighbourgs[coords]; }

        neighbourgs.Add(coords, new List<Vector3Int>());

        foreach (var i in Direction.GetDirectionList(coords.x))
        {
            if (hexTiles.ContainsKey(coords + i)) { neighbourgs[coords].Add(coords + i); }
        }

        return neighbourgs[coords];
    }

    public Vector3Int GetClosestHex(Vector3 worldPos)
    {
        worldPos.z = 0;
        int x = Mathf.CeilToInt(worldPos.x);
        int y = Mathf.CeilToInt(worldPos.y);
        int z = Mathf.CeilToInt(worldPos.z);
        return new Vector3Int(x, y, z);
    }
    #endregion
}
