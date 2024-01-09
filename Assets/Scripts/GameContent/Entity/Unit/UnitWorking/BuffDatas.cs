using Enums.UnitEnums.UnitEnums;
using Utilities;

namespace GameContent.Entity.Unit.UnitWorking
{
    public readonly struct BuffDatas
    {
        public readonly int buffValue;
        public readonly int turnNb;
        public readonly BuffType buffType;

        public BuffDatas(int buffValue, int turnNb, BuffType buffType)
        {
            this.buffValue = buffValue;
            this.turnNb = turnNb;
            this.buffType = buffType;
        }

        public string GetBuffTypeName() => buffType switch
        {
            BuffType.Mp => "PM",
            BuffType.Def => "DEF",
            BuffType.Prec => "PREC",
            BuffType.CritRate => "TC",
            _ => throw new CustomExceptions.CustomException()
        };
    }
}