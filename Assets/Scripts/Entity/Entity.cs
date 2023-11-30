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

    /// <summary>
    /// Verif d'intersection entre diff reseaux
    /// </summary>
    /// <param name="pos">position d'origine de la range locale</param>
    /// <param name="hexGrid">Ref au HexGridStoree.hGS</param>
    /// <param name="range">range du reseau local</param>
    /// <param name="net">list des reseaux de base impactes par le merge, s'il il y a intersection</param>
    /// <returns>bool de validation d'intersection</returns>
    protected virtual bool IsIntersecting(Vector3Int pos, HexGridStore hexGrid, int range, out List<Network> net)
    {
        net = IsInterOnNet(pos, hexGrid, range);
        return net != null;
    }

    /// <summary>
    /// S'il il y a intersection entre reseau Local et Base, lance un merge de reseau
    /// </summary>
    /// <param name="pos">pos de depart de la range de reseau local</param>
    /// <param name="hexGrid">Ref de HexGridStore.hGS</param>
    /// <param name="range">range max du reseau local</param>
    /// <returns>liste de network impactee par le merge</returns>
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

    /// <summary>
    /// Merge les reseaux local et bases
    /// </summary>
    /// <param name="pos">pos de depart de la range de reseau local</param>
    /// <param name="hexGrid">Ref de HexGridStore.hGS</param>
    /// <param name="range">range max du reseau local</param>
    /// <param name="toMerge">liste de network devant etre merge</param>
    /// <returns>la nouvelle list de reseau local du hacker</returns>
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

    /// <summary>
    /// Remplace l'ancien global network par le nouveau // Fonction generale de generation de reseau /!\
    /// </summary>
    public virtual void OnGenerateNet()
    {
        if (IsIntersecting(CurrentHexPos, HexGridStore.hGS, NetworkRange, out List<Network> net))
        {
            GlobalNetwork = OnIntersect(CurrentHexPos, HexGridStore.hGS, NetworkRange, net);
        }
    }
    #endregion
}