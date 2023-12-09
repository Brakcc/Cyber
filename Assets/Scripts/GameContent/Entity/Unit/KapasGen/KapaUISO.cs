using Interfaces.Kapas;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.Entity.Unit.KapasGen
{
    public class KapaUISO : ScriptableObject, IKapaUISO
    {
        [SerializeField] Image kapaImage;
        public Image KapaImage => kapaImage;

        [SerializeField] string kapaName;
        public string KapaName => kapaName;

        [SerializeField] int kapaCost;
        public int KapaCost => kapaCost;

        [SerializeField] string kapaDescription;
        public string KapaDescription => kapaDescription;
    }
}
