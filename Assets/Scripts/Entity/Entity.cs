using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity
{
    #region interface fields
    public abstract Vector3Int CurrentHexPos { get; set; }
    public abstract bool IsNetworkEmiter { get; set; }
    public abstract bool IsOnNetwork { get; set; }
    public abstract int NetworkRange { get; set; }
    public abstract List<Vector3Int> GlobalNetwork { get; set; }
    #endregion

    #region methodes
    /// <summary>
    /// A placer dans toutes les awakes des Entities /!\
    /// </summary>
    protected virtual void OnInit() => CurrentHexPos = HexCoordonnees.GetClosestHex(transform.position);

    /// <summary>
    /// Renvoie la liste (IEnumerable) complete de tiles dans la range d'un Network
    /// </summary>
    /// <param name="hexPos">Position de depart pour calculer des tiles de reseau</param>
    /// <param name="hexGrid">Ref au HexGridStore.hGS</param>
    /// <param name="range">Range max du reseau de l'Entity</param>
    /// <returns>IEnumerable des tiles d'un Network</returns>
    protected virtual IEnumerable<Vector3Int> GetRangeList(Vector3Int hexPos, HexGridStore hexGrid, int range) => PathFind.PathKapaVerif(hexGrid, hexPos, range).GetRangePositions();

    protected virtual bool IsIntersecting(Vector3Int pos, HexGridStore hexGrid, int range, out List<Network> net)
    {
        net = IsInterOnNet(pos, hexGrid, range);
        return net != null;
    }

    protected virtual List<Network> IsInterOnNet(Vector3Int pos, HexGridStore hexGrid, int range)
    {
        List<Network> toMerge = new();
        foreach (var i in GetRangeList(pos, hexGrid, range))
        {
            var t = hexGrid.GetTile(i);
            if (t.LocalNetwork != Network.None && !toMerge.Contains(t.LocalNetwork)) { toMerge.Add(t.LocalNetwork); }
        }
        return toMerge;
    }

    protected virtual List<Vector3Int> OnIntersect(Vector3Int pos, HexGridStore hexGrid, int range, List<Network> toMerge)
    {
        if (toMerge.Count == 0) { return null; }

        List<Vector3Int> newRange = GetRangeList(pos, hexGrid, range).ToList();
        foreach (var i in toMerge)
        {
            foreach (var j in hexGrid.NetworkList[(int)i])
            {
                if (!newRange.Contains(j)) { newRange.Add(j); }
            }
        }
        foreach (var l in newRange) { hexGrid.GetTile(l).EnableGlowDynaNet(); }

        return newRange;
    }

    public void OnGenerateNet()
    {
        if (IsIntersecting(CurrentHexPos, HexGridStore.hGS, NetworkRange, out List<Network> net))
        {
            GlobalNetwork = OnIntersect(CurrentHexPos, HexGridStore.hGS, NetworkRange, net);
        }
    }
    #endregion
}