using UnityEngine;
using GameContent.GridManagement;
using GameContent.Entity.Unit.UnitWorking;
using UI.InGameUI;
using DataManagement;

namespace GameContent.GameManagement
{
    public class GameGenManager : MonoBehaviour
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
        
        public static GameGenManager gGm;
        
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
            for (var i = 0; i < 3; i++)
            {
                teamLists.heroPlayer0[i].GetComponent<Unit>().UnitData = unitList.GetUnitData(team0[i]);
            }
            for (var i = 0; i < 3; i++)
            {
                teamLists.heroPlayer1[i].GetComponent<Unit>().UnitData = unitList.GetUnitData(team1[i]);
            }
        }

        #endregion
        
        #endregion
    }
}