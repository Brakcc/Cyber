using Enums.FeedBackEnums;
using Interfaces.Unit;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff
{
    public static class BuffDebuffKapa
    {
        public static void OnBuffDebuffMP(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentMp += val;
            unitTarget.MpBDbCounter = turnNb;
            unitTarget.StatUI.SetMP(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }
        
        public static void OnBuffDebuffCritRate(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentCritRate += val;
            unitTarget.CrBDbCounter = turnNb;
            unitTarget.StatUI.SetCritRate(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }
        
        public static void OnBuffDebuffPrecision(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentPrecision += val;
            unitTarget.PrecBDbCounter = turnNb;
            unitTarget.StatUI.SetPrec(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }

        public static void OnBuffDebuffDef(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentDef += val;
            unitTarget.DefBDbCounter = turnNb;
            unitTarget.StatUI.SetDef(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }
    }
}