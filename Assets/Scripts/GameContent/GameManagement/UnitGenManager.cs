using UnityEngine;
using GameContent.GridManagement;
using GameContent.Entity.Unit.UnitWorking;
using UI.InGameUI;
using DataManagement;
using TMPro;

namespace GameContent.GameManagement
{
    public class UnitGenManager : MonoBehaviour
    {
        #region fields

        [SerializeField] private UnitListSo unitList;
        [SerializeField] private PlayerStatsUI[] playerStats;
        public TeamList TeamLists => teamLists;
        [SerializeField] private TeamList teamLists;

        [System.Serializable]
        public class TeamList
        {
            public GameObject[] heroPlayer0;
            public GameObject[] heroPlayer1;
        }

        //Teams IDs
        [HideInInspector] public int[] team0;
        [HideInInspector] public int[] team1;
        
        public static UnitGenManager gGm;
        
        #endregion
        
        #region methodes

        private void Awake()
        {
            gGm = this;
            
            OnGetTeamDatas();
        }

        private void Start() => OnInitGameScene();
        
        #region Inits

        private void OnInitGameScene()
        {
            OnInitSceneUnits();
                    
            HexGridStore.hGs.OnIntMapAndEntities();
                    
            GameLoopManager.gLm.InitTeam(GameLoopManager.gLm.teamInits.firstTeamPlaying);
                    
            OnInitUi();
        }

        private void OnGetTeamDatas()
        {
            team0 = TeamDatasSaveAndLoad.OnLoadSingleTeam(0);
            team1 = TeamDatasSaveAndLoad.OnLoadSingleTeam(1);
        }

        private void OnInitUi()
        {
            foreach (var playStat in playerStats)
            {
                playStat.OnInit();
            }
        }
                
        private void OnInitSceneUnits()
        {
            for (var i = 0; i < 4; i++)
            {
                teamLists.heroPlayer0[i].GetComponent<Unit>().UnitData = unitList.GetUnitData(team0[i]);
                teamLists.heroPlayer0[i].GetComponentInChildren<TextMeshPro>().text =
                    unitList.GetUnitData(team0[i]).Name;
            }
            for (var i = 0; i < 4; i++)
            {
                teamLists.heroPlayer1[i].GetComponent<Unit>().UnitData = unitList.GetUnitData(team1[i]);
                teamLists.heroPlayer1[i].GetComponentInChildren<TextMeshPro>().text =
                    unitList.GetUnitData(team1[i]).Name;
            }
        }

        #endregion
        
        #endregion
    }
}