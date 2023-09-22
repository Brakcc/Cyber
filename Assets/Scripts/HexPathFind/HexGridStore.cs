using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridStore: MonoBehaviour
{
    private Dictionary<Vector3Int, Hex> hexTiles = new Dictionary<Vector3Int, Hex>();
    private Dictionary<Vector3Int, List<Vector3Int>> neighbourgs = new Dictionary<Vector3Int, List<Vector3Int>>();

    void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hexTiles[hex.offsetCoords] = hex;
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
}
