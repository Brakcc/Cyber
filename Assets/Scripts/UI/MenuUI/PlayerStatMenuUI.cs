using Interfaces.Unit;
using TMPro;
using UnityEngine;

namespace UI.MenuUI
{
    public class PlayerStatMenuUI : MonoBehaviour
    {
        #region fields
        
        [SerializeField] private TMP_Text mpText;
        [SerializeField] private TMP_Text atkText;
        [SerializeField] private TMP_Text defText;
        [SerializeField] private TMP_Text critText;
        [SerializeField] private TMP_Text hPText;
        [SerializeField] private TMP_Text kapa1Text;
        [SerializeField] private TMP_Text kapa2Text;

        #endregion

        #region methodes

        public void OnInitUI(IUnitData uData)
        {
            
        }
        
        #region setter Methodes 
        
        
        
        #endregion

        #endregion
    }
}