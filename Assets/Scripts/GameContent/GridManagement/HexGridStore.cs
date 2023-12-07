using System.Collections.Generic;
using UnityEngine;

public class HexGridStore : MonoBehaviour
{
    #region fields
    #region Grid Data
    public Dictionary<Vector3Int, Hex> hexTiles = new();
    private readonly Dictionary<Vector3Int, List<Vector3Int>> neighbourgs = new();
    #endregion

    #region ComputerList
    List<Vector3Int>[] ComputerToHack { get; set; } = new List<Vector3Int>[3];
    public Computer[] computerList;
    #endregion

    #region Network
    public List<Vector3Int>[] NetworkList { get => networkList; set { networkList = value; } }
    List<Vector3Int>[] networkList = new List<Vector3Int>[(int)NetworkType.None];
    public int EmptySockets { get; set; }

    [HideInInspector] public List<IEntity> emiters = new();
    #endregion

    public static HexGridStore hGS;
    #endregion

    #region methodes
    void Awake() => hGS = this;

    void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>()) { hexTiles[hex.HexCoords] = hex; }
        OnInitNetwork();
        OnInitComputers();
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
        for (int i = 0; i < networkList.Length; i++) { networkList[i] = new(); }

        foreach (var hex in FindObjectsOfType<Hex>())
        {
            if (hex.LocalNetwork != NetworkType.None)
            {
                networkList[(int)hex.LocalNetwork].Add(hex.HexCoords);
                hex.EnableGlowBaseNet();
            }
        }
        foreach (var i in networkList)
        {
            if (i.Count == 0) { EmptySockets++; }
        }

        foreach (Entity ent in FindObjectsOfType<Entity>())
        {
            if (ent.IsNetworkEmiter) emiters.Add(ent);
        }
    }


    public void OnAddEmiter(IEntity ent) => emiters.Add(ent);

    public void OnDelEmiter(IEntity ent)
    {
        if (emiters == null) return;
        emiters.Remove(ent);
    }

    public void OnAddToNetwork(NetworkType net, IEnumerable<Vector3Int> newNet) => networkList[(int)net].AddRange(newNet);
    public void OnAddToNetwork(NetworkType net, Vector3Int newNet) => networkList[(int)net].Add(newNet);

    public void OnDelFromNetwork(NetworkType net, List<Vector3Int> oldNet)
    {
        foreach (var i in oldNet) { networkList[(int)net].Remove(i); }
    }
    public void OnDelFromNetwork(NetworkType net, Vector3Int oldNet) => networkList[(int)net].Remove(oldNet);

    public List<Vector3Int> GetNetwork(Vector3Int pos)
    {
        List<Vector3Int> allNet = new();
        foreach (var net in networkList)
        {
            if (net.Contains(pos)) { allNet.AddRange(net); }
        }
        return allNet;
    }

    public bool IsOnNetwork(Vector3Int pos, NetworkType net) => networkList[(int)net].Contains(pos);
    public bool IsOnNetwork(Vector3Int pos)
    {
        foreach (var i in networkList)
        {
            if (i.Contains(pos)) { return true; }
        }
        return false;
    }
    #endregion

    #region Computer
    void OnInitComputers()
    {
        for (int i = 0; i < ComputerToHack.Length; i++) { ComputerToHack[i] = new(); }

        computerList = new Computer[3];

        foreach (var hex in FindObjectsOfType<Hex>())
        {
            if (hex.IsComputer())
            {
                ComputerToHack[(int)hex.ComputerTarget].Add(hex.HexCoords);
            }
        }
        foreach (var c in FindObjectsOfType<Computer>())
        {
            computerList[(int)c.ComputerTarget] = c;
        }
    }

    public void HandlePCHacked(ComputerTarget whichPC)
    {
        if (computerList[(int)whichPC].GotHacked) return;

        foreach (var i in ComputerToHack[(int)whichPC])
        {
            GetTile(i).CurrentType = HexType.Walkable;
        }
        computerList[(int)whichPC].HandleComputerHack();
        computerList[(int)whichPC].GotHacked = true;
    }
    #endregion
    #endregion
}