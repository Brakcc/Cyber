using UnityEngine.UI;

namespace Interfaces.Kapas
{
    public interface IKapaUISO
    {
        public Image KapaImage { get; }
        public string KapaName { get; }
        public int KapaCost { get; }
        public string KapaDescription { get; }
    }
}
