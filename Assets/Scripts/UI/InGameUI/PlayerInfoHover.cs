using UnityEngine;

namespace UI.InGameUI
{
    public class PlayerInfoHover : MonoBehaviour
    {
        #region fields
        [SerializeField] GameObject PlayerInfo;
        float counter = 0;
        bool isHovering;
        #endregion

        #region methodes
        void Update()
        {
            if (!isHovering) return;
            counter += Time.deltaTime;

            if (counter >= 0.5f) { OnPrint(); }
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

        void OnPrint()
        {
            PlayerInfo.SetActive(true);
        }
        void OnHide()
        {
            PlayerInfo.SetActive(false);
        }
        #endregion
    }
}