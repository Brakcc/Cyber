using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class MapSelection : MonoBehaviour
    {
        #region fields

        [SerializeField] private Image mapImage;
        [SerializeField] private TMP_Text mapText;
        
        [SerializeField] private MapData[] mapDatas;
        private int _currentMapSelectedID;
        
        #endregion

        #region methodes

        private void Start()
        {
            _currentMapSelectedID = 0;
            mapImage.sprite = mapDatas?[0].mapImage;
            mapText.text = mapDatas?[0].mapName;
        }

        public void StartGame()
        { 
            OnLoadScene(mapDatas?[_currentMapSelectedID].sceneName);
        }
                
        private static void OnLoadScene(string sName) => SceneManager.LoadScene(sName);
        
        #region selection switch

        public void OnSwitchMapPreview(int i)
        {
            _currentMapSelectedID += i;
            
            if (_currentMapSelectedID < 0)
                _currentMapSelectedID = mapDatas.Length - 1;
            
            if (_currentMapSelectedID > mapDatas.Length - 1)
                _currentMapSelectedID = 0;
            
            mapImage.sprite = mapDatas?[_currentMapSelectedID].mapImage;
            mapText.text = mapDatas?[_currentMapSelectedID].mapName;
        }
        
        #endregion
        
        #endregion
    }
}