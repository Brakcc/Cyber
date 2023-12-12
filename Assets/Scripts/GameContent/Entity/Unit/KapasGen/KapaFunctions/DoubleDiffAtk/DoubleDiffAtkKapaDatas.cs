using Utilities.CustomHideAttribute;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DoubleDiffAtk
{
    [System.Serializable]
    public class DoubleDiffAtkKapaDatas
    {
        public bool movePointsBDb2;
        [ShowIfBoolTrue("movePointsBDb2")]
        public BuffDebuffKapaDatas mPBuffDebuffData2;
        
        public bool defenseBDb2;
        [ShowIfBoolTrue("defenseBDb2")]
        public BuffDebuffKapaDatas defBuffDebuffData2;
            
        public bool critRateBDb2;
        [ShowIfBoolTrue("critRateBDb2")]
        public BuffDebuffKapaDatas cRBuffDebuffData2;
            
        public bool precisionBDb2;
        [ShowIfBoolTrue("precisionBDb2")]
        public BuffDebuffKapaDatas precBuffDebuffData2;

        public bool balanceMultBDb2;
        [ShowIfBoolTrue("balanceMultBDb2")]
        [Range(1, 10)] public int balMultBuffDebuffData2;

        public bool dashAfterKapa;

        public bool grabAfterKapa;
    }
}