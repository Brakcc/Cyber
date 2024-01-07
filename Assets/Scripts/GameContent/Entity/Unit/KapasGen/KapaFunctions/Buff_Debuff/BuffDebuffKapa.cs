using Enums.FeedBackEnums;
using Enums.UnitEnums.UnitEnums;
using GameContent.Entity.Unit.UnitWorking;
using Interfaces.Unit;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff
{
    public static class BuffDebuffKapa
    {
        public static void OnBuffDebuffMP(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentMp += val;
            unitTarget.BuffLists.Add(new BuffDatas(val, turnNb, BuffType.Mp));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetMP(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }
        
        public static void OnBuffDebuffCritRate(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentCritRate += val;
            unitTarget.BuffLists.Add(new BuffDatas(val, turnNb, BuffType.CritRate));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetCritRate(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }
        
        public static void OnBuffDebuffPrecision(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentPrecision += val;
            unitTarget.BuffLists.Add(new BuffDatas(val, turnNb, BuffType.Prec));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetPrec(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }

        public static void OnBuffDebuffDef(IUnit unitTarget, int val, int turnNb)
        {
            unitTarget.CurrentDef += val;
            unitTarget.BuffLists.Add(new BuffDatas(val, turnNb, BuffType.Def));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetDef(unitTarget, val > 0 ? UIColorType.Buff : UIColorType.Debuff);
        }
    }
}