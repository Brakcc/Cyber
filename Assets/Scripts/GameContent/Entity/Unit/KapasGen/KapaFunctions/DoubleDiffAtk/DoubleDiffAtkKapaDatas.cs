using Utilities.CustomHideAttribute;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DoubleDiffAtk
{
    [System.Serializable]
    public class DoubleDiffAtkKapaDatas
    {
        public DoubleDiffAtkBDbList doubleDABuffDebuff;
        
        [System.Serializable]
        public class DoubleDiffAtkBDbList
        {
            public bool hasMovePointsBDb2;
            [ShowIfBoolTrue("hasMovePointsBDb2")]
            public BuffDebuffKapaDatas mPBuffDebuffData2;
            
            public bool hasDefenseBDb2;
            [ShowIfBoolTrue("hasDefenseBDb2")]
            public BuffDebuffKapaDatas defBuffDebuffData2;
                
            public bool hasCritRateBDb2;
            [ShowIfBoolTrue("hasCritRateBDb2")]
            public BuffDebuffKapaDatas cRBuffDebuffData2;
                
            public bool hasPrecisionBDb2;
            [ShowIfBoolTrue("hasPrecisionBDb2")]
            public BuffDebuffKapaDatas precBuffDebuffData2;
            
            public bool hasBalanceMultBDb2;
            [ShowIfBoolTrue("hasBalanceMultBDb2")]
            [Range(1, 10)] public int balMultBuffDebuffData2 = 1;
        }
        
        public bool hasDashAfterKapa;

        public bool hasGrabAfterKapa;
    }
}