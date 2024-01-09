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
            if (unitTarget.CurrentMp <= 0)
                return;

            var temp = unitTarget.CurrentMp + val;
            var tempVal = temp < 0 ? unitTarget.CurrentMp : val;
            
            unitTarget.CurrentMp += tempVal;
            unitTarget.BuffLists.Add(new BuffDatas(tempVal, turnNb, BuffType.Mp));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetMP(unitTarget,
                unitTarget.CurrentMp > unitTarget.UnitData.MovePoints
                    ? UIColorType.Buff
                    : UIColorType.Debuff);
        }
        
        public static void OnBuffDebuffCritRate(IUnit unitTarget, int val, int turnNb)
        {
            if (unitTarget.CurrentCritRate <= 0)
                return;
            
            var temp = unitTarget.CurrentCritRate + val;
            var tempVal = temp < 0 ? unitTarget.CurrentCritRate : val;
            
            unitTarget.CurrentCritRate += tempVal;
            unitTarget.BuffLists.Add(new BuffDatas(tempVal, turnNb, BuffType.CritRate));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetCritRate(unitTarget,
                unitTarget.CurrentCritRate > unitTarget.UnitData.CritRate
                    ? UIColorType.Buff
                    : UIColorType.Debuff);
        }
        
        public static void OnBuffDebuffPrecision(IUnit unitTarget, int val, int turnNb)
        {
            if (unitTarget.CurrentPrecision <= 0)
                return;
            
            var temp = unitTarget.CurrentPrecision + val;
            var tempVal = temp < 0 ? unitTarget.CurrentPrecision : val;
            
            unitTarget.CurrentPrecision += tempVal;
            unitTarget.BuffLists.Add(new BuffDatas(tempVal, turnNb, BuffType.Prec));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetPrec(unitTarget,
                unitTarget.CurrentPrecision > Constants.BasePrec
                    ? UIColorType.Buff
                    : UIColorType.Debuff);
        }

        public static void OnBuffDebuffDef(IUnit unitTarget, int val, int turnNb)
        {
            if (unitTarget.CurrentDef <= 0)
                return;
            
            var temp = unitTarget.CurrentDef + val;
            var tempVal = temp < 0 ? unitTarget.CurrentDef : val;
            
            unitTarget.CurrentDef += tempVal;
            unitTarget.BuffLists.Add(new BuffDatas(tempVal, turnNb, BuffType.Def));
            unitTarget.BDbCounters.Add(turnNb);
            unitTarget.StatUI.SetDef(unitTarget,
                unitTarget.CurrentDef > unitTarget.UnitData.Defense
                    ? UIColorType.Buff
                    : UIColorType.Debuff);
        }
    }
}