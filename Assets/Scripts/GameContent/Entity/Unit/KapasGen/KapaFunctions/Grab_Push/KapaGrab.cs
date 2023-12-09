using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Grab_Push
{
    [System.Serializable]
    public class KapaGrab
    {
        [SerializeField] private int range;
        [SerializeField] private int debufMP;

        public void Grab() { Debug.Log(range); }
    }
}
