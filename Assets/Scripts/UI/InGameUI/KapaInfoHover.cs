using GameContent.Entity.Unit.UnitWorking;
using UnityEngine;

namespace UI.InGameUI
{
    public class KapaInfoHover : MonoBehaviour
    {
        #region fields
        
        [SerializeField] private GameObject PlayerInfo;
        
        #endregion

        #region methodes

        public void OnPointerEnter()
        {
            if (UnitManager.uM.SelectedUnit == null)
                return;
            
            OnPrint();
        }

        public void OnPointerExit()
        {
            OnHide();
        }

        public void OnEnable()
        {
            OnHide();
        }
        public void OnDisable()
        {
            OnHide();
        }

        private void OnPrint()
        {
            PlayerInfo.SetActive(true);
        }

        private void OnHide()
        {
            PlayerInfo.SetActive(false);
        }
        #endregion
    }
}