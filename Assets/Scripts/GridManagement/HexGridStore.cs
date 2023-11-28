using System.Collections.Generic;
using UnityEngine;

public class HexGridStore: MonoBehaviour
{
    #region fields
    public Dictionary<Vector3Int, Hex> hexTiles = new();
    private readonly Dictionary<Vector3Int, List<Vector3Int>> neighbourgs = new();

    [HideInInspector] public List<Entity> emiters = new();

    public List<Vector3Int>[] NetworkList {  get => networkList; set { networkList = value; } }
    List<Vector3Int>[] networkList = new List<Vector3Int>[(int)Network.None];

    public static HexGridStore hGS;
    #endregion

    #region methodes
    void Awake() => hGS = this;

    void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>()) { hexTiles[hex.HexCoords] = hex; }
        OnInitNetwork();
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

    #region network params
    void OnInitNetwork()
    {
        networkList = new List<Vector3Int>[(int)Network.None];
        for (int i = 0; i < networkList.Length; i++) { networkList[i] = new(); }

        foreach (var hex in FindObjectsOfType<Hex>())
        {
            if (hex.CurrentNetwork != Network.None) { networkList[(int)hex.CurrentNetwork].Add(hex.HexCoords); } 
        }
        
        foreach (Entity ent in FindObjectsOfType<Entity>())
        {
            if (ent.IsNetworkEmiter) emiters.Add(ent);
        }
    }


    public void OnAddEmiter(Entity ent) => emiters.Add(ent);

    public void OnDelEmiter(Entity ent)
    {
        if (emiters == null) return;
        emiters.Remove(ent);
    }

    public void OnAddToNetwork(Network net, IEnumerable<Vector3Int> newNet) => networkList[(int)net].AddRange(newNet);
    public void OnAddToNetwork(Network net, Vector3Int newNet) => networkList[(int)net].Add(newNet);

    public void OnDelFromNetwork(Network net, List<Vector3Int> oldNet)
    {
        foreach (var i in oldNet) { networkList[(int)net].Remove(i); }
    }
    public void OnDelFromNetwork(Network net, Vector3Int oldNet) => networkList[(int)net].Remove(oldNet);

    public List<Vector3Int> GetNetwork(Vector3Int pos)
    {
        List<Vector3Int> allNet = new();
        foreach (var net in networkList)
        {
            if (net.Contains(pos)) { allNet.AddRange(net); }
        }
        return allNet;
    }
    public bool IsOnNetwork(Vector3Int pos)
    {
        foreach (var i in networkList)
        {
            if (i.Contains(pos)) { return true; }
        }
        return false;
    }

    /*public bool OnIntersect(List<Vector3Int> net)
    {
        
    }*/
    #endregion
    #endregion
}