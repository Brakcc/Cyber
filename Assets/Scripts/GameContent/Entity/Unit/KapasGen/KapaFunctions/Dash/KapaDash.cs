using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Dash
{
    [System.Serializable]
    public class KapaDash
    {
        [SerializeField] private int range;
        [SerializeField] private int speed;

        public void Grab() { Debug.Log(range); }
    }
}
