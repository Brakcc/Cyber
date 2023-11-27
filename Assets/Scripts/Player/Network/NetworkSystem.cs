using System.Collections.Generic;
using UnityEngine;

public class NetworkSystem
{
    #region fields
    PathResult turretRange = new();
    #endregion

    #region methodes
    PathResult GetTurretRange(Entity ent, HexGridStore hexGrid) => PathFind.PathKapaVerif(hexGrid, ent.CurrentHexPos, ent.NetworkRange);

    public void OnActivateNetwork(Entity ent, HexGridStore hexGrid)
    {
        bool canMix = false;
        List<Network> netList = new();
        turretRange = GetTurretRange(ent, hexGrid);
        foreach (var i in turretRange.GetRangePositions())
        {
            if (!hexGrid.GetTile(i).CurrentNetwork.Contains(Network.None))
            {
                canMix = true;
                netList = hexGrid.GetTile(i).CurrentNetwork;
                break;
            }
        }
        if (canMix)
        {
            foreach (var i in turretRange.GetRangePositions())
            {
                hexGrid.GetTile(i).AddMixedNetwork(netList);
            }
        }
    }
    #endregion
}
