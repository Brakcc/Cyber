using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.InGameUI
{
    public class MouseHoverPause : MonoBehaviour, IUIHelper
    {
        [SerializeField] private Button button;

        private static readonly Vector3 OriginScale = new(1, 1, 1);
        private static readonly Vector3 ScaleChange = new(0.1f, 0.1f, 0.1f);
        [SerializeField] private Outline outline;

        public void OnPointerEnter()
        {
            button.transform.localScale = OriginScale + ScaleChange;
            outline.enabled = true;
        }

        public void OnPointerExit()
        {
            button.transform.localScale = OriginScale;
            outline.enabled = false;
        }

        public void OnEnable()
        {
            button.transform.localScale = OriginScale;
            outline.enabled = false;
        }
        public void OnDisable()
        {
            button.transform.localScale = OriginScale;
            outline.enabled = false;

        }
    }
}