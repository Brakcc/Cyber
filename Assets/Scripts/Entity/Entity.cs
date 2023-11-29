using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity
{
    #region fields
    public abstract Vector3Int CurrentHexPos { get; set; }
    public abstract bool IsNetworkEmiter { get; set; }
    public abstract int NetworkRange { get; set; }
    public abstract List<Vector3Int> LocalNetwork { get; set; }
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
    #endregion
}