using Utilities.CustomHideAttribute;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DoubleDiffAtk
{
    [System.Serializable]
    public class DoubleDiffAtkKapaDatas
    {
        public BuffDebuffList doubleABuffDebuff;
        
        public bool hasBalanceMultBDb2;
        [ShowIfBoolTrue("hasBalanceMultBDb2")]
        [Range(1, 10)] public int balMultBuffDebuffData2 = 1;
        
        public bool hasDashAfterKapa;

        public bool hasGrabAfterKapa;

        [Tooltip("Not Applicable with Dash or Grab")]
        public bool hasDiffPatterns;
    }
}