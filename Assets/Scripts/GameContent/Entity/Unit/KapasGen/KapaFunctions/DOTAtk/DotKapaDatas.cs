using UnityEngine;
using Utilities.CustomHideAttribute;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DOTAtk
{
    [System.Serializable]
    public class DotKapaDatas
    {
        public bool hasBalanceMultBDb;
        [ShowIfBoolTrue("hasBalanceMultBDb")]
        [Range(0, 10)] public int balMultBuffDebuffData = 1;
        
        public int turnNumber;
    }
}