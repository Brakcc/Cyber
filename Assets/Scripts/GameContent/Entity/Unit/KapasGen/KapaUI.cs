using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.Entity.Unit.KapasGen
{
    public class KapaUI : MonoBehaviour 
    {
        #region fields
        public KapaUISO currentKapa;

        private Image kapaImage;
        [SerializeField] private TextMeshPro kapaName;
        private TextMeshPro kapaText;
        #endregion

        #region Inits Methodes

        private void Start()
        {
            kapaImage = GetComponent<Image>();
            kapaText = GetComponentInChildren<TextMeshPro>();

            kapaText.gameObject.SetActive(false);
        }

        public void OnNewPlayerSelected(KapaUISO kapa)
        {
            currentKapa = kapa;
            kapaImage = kapa.KapaImage;
            kapaName.text = kapa.KapaName;
            kapaText.text = kapa.KapaDescription;
        }
        public void OnNoPlayerSelected()
        {
            currentKapa = null;
            kapaImage = null;
            kapaName = null;
            kapaText = null;
        }
        #endregion

        #region UI methodes
        public void OnHoverShow() => kapaText?.gameObject.SetActive(true);
        public void OnHoverHide() => kapaText?.gameObject.SetActive(false);
        #endregion
    }
}
