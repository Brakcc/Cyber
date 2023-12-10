using System.Collections.Generic;
using UnityEngine;

namespace Interfaces.Unit
{
    public interface IEntity
    {
        #region fields
        Vector3Int CurrentHexPos { get; set; }
        bool IsNetworkEmiter { get; }
        bool IsOnNetwork { get; }
        int NetworkRange { get; }
        List<Vector3Int> GlobalNetwork { get; }
        #endregion

        #region methodes

        void OnInit();
        
        void OnGenerateNet();

        void OnSelectNetworkTiles();

        void OnDeselectNetworkTiles();
        #endregion
    }
}
