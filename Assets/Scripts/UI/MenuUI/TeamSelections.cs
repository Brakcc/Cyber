using DataManagement;
using GameContent.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace UI.MenuUI
{
    public class TeamSelections : MonoBehaviour
    {
        #region fields

        [HideInInspector]
        public int[] team1 = new int[4];
        [HideInInspector]
        public int[] team2 = new int[4];
        
        private int _selectionCounter;
        private int _preselecId;
        [SerializeField] private Image[] unitImages;
        [SerializeField] private Outline[] unitImageOutlines;
        [SerializeField] private TMP_Text currentSelecUnitName;
        [SerializeField] private UnitListSo unitList;
        [SerializeField] private string sceneName;

        #endregion

        #region methodes

        private void Start()
        {
            _selectionCounter = 8;
            OnShowUnitInSelec(_selectionCounter);
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
                case 8:
                    
                    break;
                case 7:
                    break;
                case 6:
                    break;
                case 5:
                    break;
                case 4:
                    break;
                case 3:
                    break;
                case 2:
                    break;
                case 1:
                    break;
            }
        }

        private void OnPreselecUnit(int id)
        {
            _preselecId = id;
            currentSelecUnitName.text = unitList.GetUnitData(id).Name;
        }
        private void OnShowUnitInSelec(int selec)
        {
            
        }
        
        private void OnActiveTeam0()
        {
        }

        private void OnActiveTeam1()
        {
        }

        #region Scene Management

        public void StartGame()
        { 
            if (_selectionCounter > 0)
                return;
            
            TeamDatasSaveAndLoad.OnSaveTeamDatas(this);
            OnLoadScene(sceneName);
        }
                
        private static void OnLoadScene(string sName) => SceneManager.LoadScene(sName);

        #endregion
        
        #endregion
    }
}