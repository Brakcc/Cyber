using System.Collections.Generic;
using GameContent.Entity.Unit.UnitWorking;
using Inputs;
using Interfaces.Unit;
using TMPro;
using UI.InGameUI;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.GameManagement
{
    public class GameLoopManager : MonoBehaviour
    {
        #region fields

        public TeamInits teamInits;
        public TeamInventory teamInventory;
        public Objectifs objectifs;
        public UIFields uiFields;
        
        #region Datas Classes

        [System.Serializable]
        public class TeamInits
        {
            [Range(0, 1, order = 1)]public int firstTeamPlaying;
            public GameObject deActButton;
            [HideInInspector] public int teamPlaying;
            public GameObject[] heroPlayer0;
            public GameObject[] heroPlayer1;
            [HideInInspector] public int[] countPlayer = new int[2];
        }
        
        [System.Serializable]
        public class TeamInventory
        {
            //All Chara Datas;
            public UnitListSo unitList;
                    
            public GameObject[][] playerList = new GameObject[2][];
            public int[] CompPoints { get; set; }
            public int[] TurretNumber { get; set; }
        }
        
        [System.Serializable]
        public class Objectifs
        {
            //general objectifs
            public int ComputerNumber { get; set; }
            public int maxObjectif;
        }

        [System.Serializable]
        public class UIFields
        {
            public CameraMovement camM;
            public List<TMP_Text> cPui;
            public List<TMP_Text> tNbUI;
            public Image[] computerUI;
            public PlayerStatsUI[] playerStats;
        }
        
        #endregion

        public static GameLoopManager gLm;

        #endregion

        #region methodes
        
        void Awake()
        {
            gLm = this;

            teamInventory.playerList = new[] { teamInits.heroPlayer0, teamInits.heroPlayer1 };
            foreach (var i in teamInits.heroPlayer0) { i.GetComponent<IUnit>().TeamNumber = 0; }
            foreach (var i in teamInits.heroPlayer1) { i.GetComponent<IUnit>().TeamNumber = 1; }
            teamInits.countPlayer = new[] { teamInventory.playerList[0].Length, teamInventory.playerList[1].Length };
            teamInventory.CompPoints = new[] { 0, 0 };
            teamInventory.TurretNumber = new[] { 2, 2 };
            teamInits.teamPlaying = teamInits.firstTeamPlaying;
            foreach (var i in uiFields.cPui) { i.text = 0.ToString(); i.color = Color.red; }
            foreach (var i in uiFields.tNbUI) { i.text = 2.ToString(); i.color = Color.green; }
            foreach (var i in uiFields.computerUI) { i.color = Color.red; }
        }

        public void OnInitSceneUnits()
        {
            foreach (var unitHolder in teamInits.heroPlayer0)
            {
                unitHolder.GetComponent<Unit>().UnitData = teamInventory.unitList.GetUnitData(11);
            }

            foreach (var unitHolder in teamInits.heroPlayer1)
            {
                unitHolder.GetComponent<Unit>().UnitData = teamInventory.unitList.GetUnitData(8);
            }
        }

        public void OnInitUi()
        {
            foreach (var playstat in uiFields.playerStats)
            {
                playstat.OnInit();
            }
        }

        #region During Game Logic

        /// <summary>
        /// action à réaliser dans un switch de team
        /// PLACE HOLDER POSSIBLE POUR UN MOUVEMENT DE CAM OU AUTRE FEEDBACK DE SWITCH
        /// </summary>
        /// <param name="newTeam"></param>
        void SwitchTeam(int newTeam) => InitTeam(newTeam);
        
        /// <summary>
        /// appelee à la fin d'une action de kapa d'une des Units.
        /// </summary>
        public void OnPlayerAction()
        {
            teamInits.countPlayer[teamInits.teamPlaying]--;
            if (teamInits.countPlayer[teamInits.teamPlaying] == 0)
            {
                SwitchTeam(teamInits.teamPlaying == 1? 0 : 1);
            }
        }
        
        /// <summary>
        /// initialisation d'une team apres fin de tour d'une autre team
        /// </summary>
        /// <param name="i"></param>
        public void InitTeam(int i)
        {
            teamInits.teamPlaying = i;
            teamInits.countPlayer[i] = teamInventory.playerList[i].Length;
            foreach (var player in teamInventory.playerList[i])
            {
                var u = player.GetComponent<IUnit>();
                if (u.IsDead) { teamInits.countPlayer[i]--;  continue; }
        
                u.CanPlay = true;
            }
        
            teamInits.deActButton.SetActive(i == 1);
        
            uiFields.camM.OnFollowPlayer(teamInventory.playerList[teamInits.teamPlaying][0].GetComponent<Unit>());
        }
        
        public void HandleCompPointValueChange(int teamNb, int pC)
        {
            teamInventory.CompPoints[teamNb] += pC;
            uiFields.cPui[teamNb].text = teamInventory.CompPoints[teamNb].ToString();
            if (teamInventory.CompPoints[teamNb] > 0)
            {
                uiFields.cPui[teamNb].color = Color.green;
            }
        }
        
        public void HandleTurretUse(int teamNb)
        {
            teamInventory.TurretNumber[teamNb]--;
            uiFields.tNbUI[teamNb].text = teamInventory.TurretNumber[teamNb].ToString();
            if (teamInventory.TurretNumber[teamNb] == 0)
            {
                uiFields.tNbUI[teamNb].color = Color.red; 
            }
        }
        
        public void HandleComputerValueChange()
        {
            objectifs.ComputerNumber++;
            uiFields.computerUI[objectifs.ComputerNumber - 1].color = Color.green;
            //ajouter la fin du jeu d'urgence /!\
            if (objectifs.ComputerNumber == objectifs.maxObjectif)
            {
                Time.timeScale = 0;
            }
        }

        #endregion
        
        #endregion
    }
}