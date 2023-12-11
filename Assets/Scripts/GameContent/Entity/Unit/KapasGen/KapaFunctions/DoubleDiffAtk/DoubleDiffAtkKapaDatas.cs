using Utilities.CustomHideAttribute;
using UnityEngine;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DoubleDiffAtk
{
    [System.Serializable]
    public class DoubleDiffAtkKapaDatas
    {
        //1st atk effect
        [SerializeField] private bool buffDebuffs1;
        
        [ShowIfBoolTrue("buffDebuffs1")]
        [SerializeField] private bool movePointsBDb1;
        [ShowIfBoolTrue("movePointsBDb1")][ShowIfSecu("buffDebuffs1")]
        public BuffDebuffKapaDatas mPBuffDebuffData1;
        
        [ShowIfBoolTrue("buffDebuffs1")]
        [SerializeField] private bool critRateBDb1;
        [ShowIfBoolTrue("critRateBDb1")][ShowIfSecu("buffDebuffs1")]
        public BuffDebuffKapaDatas cRBuffDebuffData1;
        
        [ShowIfBoolTrue("buffDebuffs1")]
        [SerializeField] private bool precisionBDb1;
        [ShowIfBoolTrue("precisionBDb1")][ShowIfSecu("buffDebuffs1")]
        public BuffDebuffKapaDatas precBuffDebuffData1;
        
        //2nd atk effect
        [SerializeField] private bool buffDebuffs2;
        
        [ShowIfBoolTrue("buffDebuffs2")]
        [SerializeField] private bool movePointsBDb2;
        [ShowIfBoolTrue("movePointsBDb2")][ShowIfSecu("buffDebuffs2")]
        [SerializeField] BuffDebuffKapaDatas mPBuffDebuffData2;
        
        [ShowIfBoolTrue("buffDebuffs2")]
        [SerializeField] private bool critRateBDb2;
        [ShowIfBoolTrue("critRateBDb2")][ShowIfSecu("buffDebuffs2")]
        [SerializeField] BuffDebuffKapaDatas cRBuffDebuffData2;
        
        [ShowIfBoolTrue("buffDebuffs2")]
        [SerializeField] private bool precisionBDb2;
        [ShowIfBoolTrue("precisionBDb2")][ShowIfSecu("buffDebuffs2")]
        [SerializeField] BuffDebuffKapaDatas precBuffDebuffData2;
    }
}