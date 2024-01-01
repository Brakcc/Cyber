using System.Collections.Generic;
using GameContent.GameManagement;
using UI.InGameUI;
using UnityEngine;

namespace UI.MenuUI
{
    public class PlayerInfoMenuManager : MonoBehaviour
    {
        #region  fields
        
        [SerializeField] private UnitListSo unitDatas;
        [SerializeField] private List<PlayerStatsUI> playerStats;

        #endregion

        #region methodes

        private void Start()
        {
            foreach (var stat in playerStats)
            {
                
            }
        }

        #endregion
    }
}