using UnityEngine;
using Utilities.CustomHideAttribute;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff
{
    [System.Serializable]
    public class BuffDebuffList
    {
        public bool movePointsBDb;
        [ShowIfBoolTrue("movePointsBDb")]
        public BuffDebuffKapaDatas mPBuffDebuffData;

        public bool defenseBDb;
        [ShowIfBoolTrue("defenseBDb")]
        public BuffDebuffKapaDatas defBuffDebuffData;
        
        public bool critRateBDb;
        [ShowIfBoolTrue("critRateBDb")]
        public BuffDebuffKapaDatas cRBuffDebuffData;
            
        public bool precisionBDb;
        [ShowIfBoolTrue("precisionBDb")]
        public BuffDebuffKapaDatas precBuffDebuffData;

        public bool balanceMultBDb;
        [ShowIfBoolTrue("balanceMultBDb")]
        [Range(1, 10)] public int balMultBuffDebuffData;
    }
}