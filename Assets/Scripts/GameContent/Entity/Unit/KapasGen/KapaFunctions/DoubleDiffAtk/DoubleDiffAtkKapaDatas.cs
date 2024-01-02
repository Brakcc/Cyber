using Utilities.CustomHideAttribute;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DoubleDiffAtk
{
    [System.Serializable]
    public class DoubleDiffAtkKapaDatas
    {
        public BuffDebuffList doubleABuffDebuff;
        
        public bool hasDashAfterKapa;

        public bool hasGrabAfterKapa;

        [Tooltip("Not Applicable with Dash or Grab")]
        public bool hasDiffPatterns;
    }
}