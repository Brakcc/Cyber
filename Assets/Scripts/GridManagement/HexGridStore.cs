using System.Collections.Generic;
using UnityEngine;

public class HexGridStore: MonoBehaviour
{
    #region fields
    public Dictionary<Vector3Int, Hex> hexTiles = new();
    private readonly Dictionary<Vector3Int, List<Vector3Int>> neighbourgs = new();

    public List<Entity> emiters = new();

    public static HexGridStore hGS;
    #endregion

    #region methodes
    void Awake() => hGS = this;

    void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>()) { hexTiles[hex.HexCoords] = hex; }
        foreach (Entity ent in FindObjectsOfType<Entity>()) 
        {
            if (ent.IsNetworkEmiter) emiters.Add(ent);
        }
    }

    public Hex GetTile(Vector3Int hexCoords)
    {
        hexTiles.TryGetValue(hexCoords, out Hex results);
        return results;
    }

    public List<Vector3Int> GetNeighbourgs(Vector3Int coords)
    {
        if (!hexTiles.ContainsKey(coords)) { return new List<Vector3Int>(); }
        if (neighbourgs.ContainsKey(coords)) { return neighbourgs[coords]; }

        neighbourgs.Add(coords, new List<Vector3Int>());

        foreach (var i in Direction.GetDirectionList(coords.x))
        {
            if (!hexTiles.ContainsKey(coords + i)) continue;
            neighbourgs[coords].Add(coords + i);
        }

        return neighbourgs[coords];
    }

    #region network emiters
    public void OnAddEmiter(Entity ent)
    {
        emiters.Add(ent);
    }

    public void OnDelEmiter(Entity ent)
    {
        if (emiters == null) return;
        emiters.Remove(ent);
    }
    #endregion
    #endregion
}
