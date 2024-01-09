using System.Collections.Generic;
using Inputs;
using TMPro;
using UI.InGameUI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using IUnit = Interfaces.Unit.IUnit;
using Unit = GameContent.Entity.Unit.UnitWorking.Unit;

namespace GameContent.GameManagement
{
    public class GameLoopManager : MonoBehaviour
    {
        #region fields

        public TeamInits teamInits;
        public TeamInventory teamInventory;
        public Objectifs objectifs;
        public UIFields uiFields;
        private int _turnNb;
        
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
            public GameObject[][] playerList = new GameObject[2][];
            public int[] CompPoints { get; set; }
            public int[] TurretNumber { get; set; }
        }
        
        [System.Serializable]
        public class Objectifs
        {
            //general objectifs
            //public int ComputerNumber { get; set; }
            public int maxTurnNb;
            public TMP_Text turnText;
            public TMP_Text endText;
            public GameObject endGameUI;
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
        
        private void Awake()
        {
            gLm = this;

            teamInventory.playerList = new[] { teamInits.heroPlayer0, teamInits.heroPlayer1 };
            foreach (var i in teamInits.heroPlayer0)
            {
                i.GetComponent<IUnit>().TeamNumber = 0;
            }
            foreach (var i in teamInits.heroPlayer1)
            {
                i.GetComponent<IUnit>().TeamNumber = 1;
            }
            teamInits.countPlayer = new[] { teamInventory.playerList[0].Length, teamInventory.playerList[1].Length };
            teamInventory.CompPoints = new[] { Constants.StartingCompPoints, Constants.StartingCompPoints };
            teamInventory.TurretNumber = new[] { Constants.StartingTurretNb, Constants.StartingTurretNb };
            teamInits.teamPlaying = teamInits.firstTeamPlaying;
            foreach (var i in uiFields.cPui)
            {
                i.text = Constants.StartingCompPoints.ToString();
                i.color = Color.green;
            }
            foreach (var i in uiFields.tNbUI)
            {
                i.text = Constants.StartingTurretNb.ToString();
                i.color = Color.green;
            }
            //foreach (var i in uiFields.computerUI) { i.color = Color.red; }

            objectifs.turnText.text = $"TURNS  LEFT  :  {objectifs.maxTurnNb.ToString()}";
            _turnNb = objectifs.maxTurnNb;
            objectifs.endGameUI.SetActive(false);
        }

        #region During Game Logic

        /// <summary>
        /// action à réaliser dans un switch de team
        /// PLACE HOLDER POSSIBLE POUR UN MOUVEMENT DE CAM OU AUTRE FEEDBACK DE SWITCH
        /// </summary>
        /// <param name="newTeam"></param>
        private void SwitchTeam(int newTeam)
        {
            if (newTeam % 2 == 0)
            {
                _turnNb--;
                objectifs.turnText.text = $"TURNS  LEFT  :  {_turnNb.ToString()}";
                switch (_turnNb)
                {
                    case 0:
                        objectifs.turnText.text = "LAST  TURN";
                        break;
                    case < 0:
                        objectifs.turnText.text = "GAME  ENDED";
                        OnEndGame(0);
                        break;
                }
            }
            InitTeam(newTeam);
        }
        
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
                if (u.IsDead)
                {
                    u.OnCheckRez(u, out var rezed);
                    if (rezed)
                    {
                        u.CanPlay = true;
                        continue;
                    }
                    teamInits.countPlayer[i]--; 
                    continue;
                }
        
                u.CanPlay = true;
            }

            foreach (var player in teamInventory.playerList[1 - i])
            {
                var u = player.GetComponent<IUnit>();
                u.OnCheckEffectCounter(u);
            }
            
            teamInits.deActButton.SetActive(i == 1);
        
            uiFields.camM.OnFollowPlayer(teamInventory.playerList[teamInits.teamPlaying][0].GetComponent<Unit>());
        }
        
        public void HandleCompPointValueChange(int teamNb, int pC)
        {
            if (teamInventory.CompPoints[teamNb] >= 5 && pC > 0)
                return;
            
            teamInventory.CompPoints[teamNb] += pC;
            uiFields.cPui[teamNb].text = teamInventory.CompPoints[teamNb].ToString();

            uiFields.cPui[teamNb].color = teamInventory.CompPoints[teamNb] switch
            {
                <= 0 => Color.red,
                > 0 => Color.green
            };
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
        
        public static void HandleComputerValueChange()
        {
            //changer la logique de Hack des relays
        }

        public void OnEndGame(int teamNb)
        {
            Time.timeScale = 0;
            GetComponent<PlayerInput>().enabled = false;
            objectifs.endText.text = teamNb switch
            {
                0 => "DEFENDERS  WIN",
                1 => "ATTACKERS  WIN",
                _ => "BRUH"
            };
            objectifs.endGameUI.SetActive(true);
        }

        #endregion
        
        #endregion
    }
}