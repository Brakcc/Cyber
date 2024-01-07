using System.Collections.Generic;
using System.Linq;
using DataManagement;
using Enums.UnitEnums.UnitEnums;
using GameContent;
using GameContent.GameManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.InGameUI;

namespace UI.MenuUI
{
    public class TeamSelections : MonoBehaviour
    {
        #region fields

        [HideInInspector]
        public int[] team0 = new int[4];
        [HideInInspector]
        public int[] team1 = new int[4];

        #region Team Selec Limit

        private int _t0SelecTankLeft;
        private int _t0SelecDpsLeft;
        private int _t0SelecHackLeft;

        
        private int _t1SelecTankLeft;
        private int _t1SelecDpsLeft;
        private int _t1SelecHackLeft;

        #endregion

        #region ButtonRefs

        [SerializeField] private Button[] tankButtons;
        [SerializeField] private Button[] dpsButtons;
        [SerializeField] private Button[] hackButtons;
        private Button[] _fullbuttons;

        #endregion
        
        private int _selectionCounter;
        private int _preselecId;
        [SerializeField] private GameObject[] unitImages;
        [SerializeField] private TMP_Text currentSelecUnitName;
        [SerializeField] private UnitListSo unitList;
        [SerializeField] private GameObject mapSelec;
        [SerializeField] private GameObject teamSelec;

        #endregion

        #region methodes

        private void Start()
        {
            _t0SelecTankLeft = Constants.MaxHackerAndTankNb;
            _t0SelecDpsLeft = Constants.MaxDpsNb;
            _t0SelecHackLeft = Constants.MaxHackerAndTankNb;
            _t1SelecTankLeft = Constants.MaxHackerAndTankNb;
            _t1SelecDpsLeft = Constants.MaxDpsNb;
            _t1SelecHackLeft = Constants.MaxHackerAndTankNb;
            
            _preselecId = 18;
            _selectionCounter = 7;
            OnShowUnitInSelec(_selectionCounter);
            mapSelec.SetActive(false);

            team0 = new []{Constants.unitNb, Constants.unitNb, Constants.unitNb, Constants.unitNb};
            team1 = new []{Constants.unitNb, Constants.unitNb, Constants.unitNb, Constants.unitNb};
        }

        public void IdSelection(int id)
        {
            if (id != _preselecId)
            {
                OnPreselecUnit(id);
                return;
            }
            
            switch (_selectionCounter)
            {
                case 7:
                    SelectionAction(team1, 0, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 6:
                    SelectionAction(team0, 0, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 5:
                    SelectionAction(team0, 1, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 4:
                    SelectionAction(team1, 1, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 3:
                    SelectionAction(team1, 2, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 2:
                    SelectionAction(team0, 2, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 1:
                    SelectionAction(team0, 3, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
                case 0:
                    SelectionAction(team1, 3, id, unitImages, unitList);
                    _preselecId = 18;
                    break;
            }
        }
        
        #region selection methodes

        private void SelectionAction(IList<int> whichTeam, int whichU, int iD, IReadOnlyList<GameObject> images, UnitListSo unitL)
        {
            OnSelect(whichTeam, whichU, iD, images[_selectionCounter], unitL);
            OnCheckTeamComp(unitL.GetUnitData(iD).Type, _selectionCounter);
            OnValidateSelec(_selectionCounter);
            _selectionCounter--;
            OnCheckIfCanClic(_selectionCounter);
            if (_selectionCounter >= 0)
                OnShowUnitInSelec(_selectionCounter);
        }
        
        private void OnPreselecUnit(int id)
        {
            _preselecId = id;
            currentSelecUnitName.text = unitList.GetUnitData(id).Name;
        }
        private void OnShowUnitInSelec(int selec)
        {
            unitImages[selec].transform.localScale += new Vector3(Constants.UIImageScaleUp, 
                Constants.UIImageScaleUp, 
                Constants.UIImageScaleUp);
            unitImages[selec].GetComponent<Outline>().enabled = true;
        }

        private void OnCheckIfCanClic(int counter)
        {
            OnEnableButtons();
            
            if (IsCounterTeam0(counter))
            {
                if (_t0SelecDpsLeft == 0)
                    OnDisableDps();
                
                if (_t0SelecHackLeft == 0)
                    OnDisableHack();
                
                if (_t0SelecTankLeft == 0)
                    OnDisableTank();
                
                CheckDoubleUnit(0);
            }
            else
            {
                if (_t1SelecDpsLeft == 0)
                    OnDisableDps();
                
                if (_t1SelecHackLeft == 0)
                    OnDisableHack();
                
                if (_t1SelecTankLeft == 0)
                    OnDisableTank();
                
                CheckDoubleUnit(1);
            }
        }

        private void CheckDoubleUnit(int teamNb)
        {
            foreach (var but in dpsButtons)
            {
                switch (teamNb)
                {
                    case 0:
                        if (team0.Contains(but.GetComponent<PlayerInfoHover>().iDRef))
                        {
                            OnDisableButton(but);
                        }
                        break;
                    case 1:
                        if (team1.Contains(but.GetComponent<PlayerInfoHover>().iDRef))
                        {
                            OnDisableButton(but);
                        }
                        break;
                }
            }
        }

        private void OnEnableButtons()
        {
            OnEnableDps();
            OnEnableHack();
            OnEnableTank();
        }

        private void OnValidateSelec(int selec)
        {
            unitImages[selec].transform.localScale = Vector3.one;
        }

        private void OnCheckTeamComp(UnitType unitType, int counter)
        {
            switch (unitType)
            {
                case UnitType.Tank when IsCounterTeam0(counter):
                    _t0SelecTankLeft--;
                    break;
                case UnitType.DPS when IsCounterTeam0(counter):
                    _t0SelecDpsLeft--;
                    break;
                case UnitType.Hacker when IsCounterTeam0(counter):
                    _t0SelecHackLeft--;
                    break;
                case UnitType.Tank when !IsCounterTeam0(counter):
                    _t1SelecTankLeft--;
                    break;
                case UnitType.DPS when !IsCounterTeam0(counter):
                    _t1SelecDpsLeft--;
                    break;
                case UnitType.Hacker when !IsCounterTeam0(counter):
                    _t1SelecHackLeft--;
                    break;
            }
        }
        
        private static void OnSelect(IList<int> team, int whichUnit, int id, GameObject goImage, UnitListSo list)
        {
            team[whichUnit] = id;
            goImage.GetComponent<Image>().sprite = list.GetUnitData(id).Sprite;
        }
        
        private static bool IsCounterTeam0(int count) => count is 1 or 2 or 5 or 6;
        
        #region Buttons

        private static void OnDisableButton(Selectable but)
        {
            but.interactable = false;
            var block = but.colors;
            block.normalColor = Color.grey;
            but.colors = block;
        }

        private void OnDisableTank()
        {
            foreach (var but in tankButtons)
            {
                but.interactable = false;
                var block = but.colors;
                block.normalColor = Color.grey;
                but.colors = block;
            }
        }
        private void OnEnableTank()
        {
            foreach (var but in tankButtons)
            {
                but.interactable = true;
                var block = but.colors;
                block.normalColor = Color.white;
                but.colors = block;
            }
        }
        
        private void OnDisableDps()
        {
            foreach (var but in dpsButtons)
            {
                but.interactable = false;
                var block = but.colors;
                block.normalColor = Color.grey;
                but.colors = block;
            }
        }
        private void OnEnableDps()
        {
            foreach (var but in dpsButtons)
            {
                but.interactable = true;
                var block = but.colors;
                block.normalColor = Color.white;
                but.colors = block;
            }
        }
        
        private void OnDisableHack()
        {
            foreach (var but in hackButtons)
            {
                but.interactable = false;
                var block = but.colors;
                block.normalColor = Color.grey;
                but.colors = block;
            }
        }
        private void OnEnableHack()
        {
            foreach (var but in hackButtons)
            {
                but.interactable = true;
                var block = but.colors;
                block.normalColor = Color.white;
                but.colors = block;
            }
        }
        
        #endregion

        #endregion

        #region team Management

        public void ValidateTeams()
        {
            if (_selectionCounter >= 0)
                return;
            
            TeamDatasSaveAndLoad.OnSaveTeamDatas(this);
            
            teamSelec.SetActive(false);
            mapSelec.SetActive(true);
        }
        
        #endregion
        
        #endregion
    }
}