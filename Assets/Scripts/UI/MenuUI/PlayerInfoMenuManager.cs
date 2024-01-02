using System.Collections.Generic;
using GameContent.GameManagement;
using UnityEngine;

namespace UI.MenuUI
{
    public class PlayerInfoMenuManager : MonoBehaviour
    {
        #region  fields
        
        [SerializeField] private UnitListSo unitDatas;
        [SerializeField] private List<PlayerStatMenuUI> playerStats;

        #endregion

        #region methodes

        private void Start()
        {
            for (var i = 0; i < unitDatas.GetUnitListSize(); i++)
            {
                playerStats[i].OnInitUI(unitDatas.GetUnitData(i));
            }
        }

        #endregion
    }
}