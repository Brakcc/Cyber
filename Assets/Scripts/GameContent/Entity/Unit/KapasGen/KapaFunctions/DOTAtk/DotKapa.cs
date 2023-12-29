using Interfaces.Unit;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.DOTAtk
{
    public static class DotKapa
    {
        public static void OnStartDot(IUnit unitRef, int turnNb)
        {
            unitRef.DotCounter = turnNb;
        }
    }
}