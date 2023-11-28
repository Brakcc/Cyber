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
        
    }
    #endregion
}
