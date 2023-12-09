using DG.Tweening;
using UnityEngine;

namespace UI.InGameUI
{
    public class UIHideSys : MonoBehaviour
    {
        #region fields
        [SerializeField] GameObject playerInfo;
        [SerializeField] float offSet;
        float originXPos;
        float counter;
        bool isHovering;
        #endregion

        #region methodes
        void Start()
        {
            originXPos = playerInfo.transform.position.x;
        }
        void Update()
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

        void OnPrint()
        {
            playerInfo.transform.DOMoveX(originXPos + offSet, 0.3f);
        }
        void OnHide()
        {
            playerInfo.transform.DOMoveX(originXPos, 0.3f);
        }
        #endregion
    }
}