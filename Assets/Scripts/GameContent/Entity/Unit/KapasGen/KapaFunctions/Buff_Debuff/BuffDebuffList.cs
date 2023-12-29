using UnityEngine;
using Utilities.CustomHideAttribute;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff
{
    [System.Serializable]
    public class BuffDebuffList
    {
        public bool hasMovePointsBDb;
        [ShowIfBoolTrue("hasMovePointsBDb")]
        public BuffDebuffKapaDatas mPBuffDebuffData;

        public bool hasDefenseBDb;
        [ShowIfBoolTrue("hasDefenseBDb")]
        public BuffDebuffKapaDatas defBuffDebuffData;
        
        public bool hasCritRateBDb;
        [ShowIfBoolTrue("hasCritRateBDb")]
        public BuffDebuffKapaDatas cRBuffDebuffData;
            
        public bool hasPrecisionBDb;
        [ShowIfBoolTrue("hasPrecisionBDb")]
        public BuffDebuffKapaDatas precBuffDebuffData;

        public bool hasBalanceMultBDb;
        [ShowIfBoolTrue("hasBalanceMultBDb")]
        [Range(0, 10)] public int balMultBuffDebuffData = 1;
    }
}