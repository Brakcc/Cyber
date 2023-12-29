using Interfaces.Kapas;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.Entity.Unit.KapasGen
{
    public class KapaUISO : ScriptableObject, IKapaUISO
    {
        [SerializeField] private Image kapaImage;
        public Image KapaImage => kapaImage;

        [SerializeField] private string kapaName;
        public string KapaName => kapaName;

        [SerializeField] private int kapaCost;
        public int KapaCost => kapaCost;

        [SerializeField] private string kapaDescription;
        public string KapaDescription => kapaDescription;
    }
}
