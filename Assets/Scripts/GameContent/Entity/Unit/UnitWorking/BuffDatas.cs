using Enums.UnitEnums.UnitEnums;

namespace GameContent.Entity.Unit.UnitWorking
{
    public struct BuffDatas
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
    }
}