using DG.Tweening;
using UnityEngine;

namespace UI.InGameUI
{
    public class UIHideSys : MonoBehaviour
    {
        #region fields
        [SerializeField] private GameObject playerInfo;
        [SerializeField] private float offSet;
        private float originXPos;
        private float counter;
        private bool isHovering;
        #endregion

        #region methodes

        private void Start()
        {
            originXPos = playerInfo.transform.position.x;
        }

        private void Update()
        {
            if (!isHovering) return;
            counter += Time.deltaTime;

            if (counter >= 0.15f) { OnPrint(); }
        }

        public void OnPointerEnter()
        {
            OnPrint();
        }

        public void OnPointerExit()
        {
            OnHide();
            counter = 0;
            isHovering = false;
        }

        public void OnEnable()
        {
            //OnHide();
        }
        public void OnDisable()
        {
            //OnHide();
        }

        private void OnPrint()
        {
            playerInfo.transform.DOMoveX(originXPos + offSet, 0.3f);
        }

        private void OnHide()
        {
            playerInfo.transform.DOMoveX(originXPos, 0.3f);
        }
        #endregion
    }
}