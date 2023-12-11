using UnityEngine;
using GameContent.GridManagement;
using GameContent.Entity.Unit.UnitWorking;
using UI.InGameUI;

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

        public static GameGenManager gGm;
        
        #endregion
        
        #region methodes

        void Awake() => gGm = this;
        
        void Start() => OnInitGameScene();
        
        #region Inits

        void OnInitGameScene()
        {
            OnInitSceneUnits();
                    
            HexGridStore.hGs.OnIntMapAndEntities();
                    
            GameLoopManager.gLm.InitTeam(GameLoopManager.gLm.teamInits.firstTeamPlaying);
                    
            OnInitUi();
        }
        
        void OnInitUi()
        {
            foreach (var playstat in playerStats)
            {
                playstat.OnInit();
            }
        }
                
        void OnInitSceneUnits()
        {
            foreach (var unitHolder in teamLists.heroPlayer0)
            {
                unitHolder.GetComponent<Unit>().UnitData = unitList.GetUnitData(11);
            }
        
            foreach (var unitHolder in teamLists.heroPlayer1)
            {
                unitHolder.GetComponent<Unit>().UnitData = unitList.GetUnitData(8);
            }
        }

        #endregion
        
        #endregion
        
    }
}