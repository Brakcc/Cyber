using UnityEngine;

namespace UI.InGameUI
{
    public class PlayerInfoHover : MonoBehaviour
    {
        #region fields
        [SerializeField] private GameObject PlayerInfo;
        private float counter = 0;
        private bool isHovering;
        #endregion

        #region methodes

        private void Update()
        {
            if (!isHovering) return;
            counter += Time.deltaTime;

            if (counter >= 0.2f) { OnPrint(); }
        }

        public void OnPointerEnter()
        {
            isHovering = true;
        }

        public void OnPointerExit()
        {
            OnHide();
            counter = 0;
            isHovering = false;
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