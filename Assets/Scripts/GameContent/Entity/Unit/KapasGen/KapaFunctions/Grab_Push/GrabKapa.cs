using System.Threading.Tasks;
using GameContent.GridManagement;
using Interfaces.Unit;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Grab_Push
{
    public static class GrabKapa
    {
        public static async void OnSecondKapa(HexGridStore hexGrid, IUnit unit, IUnit target)
        {
            var xDiff = unit.CurrentWorldPos.x - target.CurrentWorldPos.x;
            var yDiff = unit.CurrentWorldPos.y - target.CurrentWorldPos.y;

            var targetTile = OnGetTargetTile(hexGrid, xDiff, yDiff, target, unit);

            await Task.Delay(ConstList.DashGrabDelay);
            
            ChangeUnitHexPos(target, targetTile);
            
            target.MoveInFrontOf(targetTile.transform.position);
        }

        private static void ChangeUnitHexPos(IUnit uT, Hex tT)
        {
            HexGridStore.hGs.GetTile(uT.CurrentHexPos).HasEntityOnIt = false;
            HexGridStore.hGs.GetTile(uT.CurrentHexPos).ClearUnit();
            tT.HasEntityOnIt = true;
            tT.SetUnit(uT);
            uT.CurrentHexPos = tT.HexCoords;
        }
        
        private static Hex OnGetTargetTile(HexGridStore hexG, float x, float y, IEntity t, IEntity u) => (x, y) switch
        {
            ( 0, < -ConstList.DashOffSetValue) => hexG.GetNorthTile(u.CurrentHexPos),
            ( < -ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetEastNorthTile(u.CurrentHexPos),
            ( < -ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetEastSouthTile(u.CurrentHexPos),
            ( 0, > ConstList.DashOffSetValue) => hexG.GetSouthTile(u.CurrentHexPos),
            ( > ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetWestSouthTile(u.CurrentHexPos),
            ( > ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetWestNorthTile(u.CurrentHexPos),
            _ => hexG.GetTile(t.CurrentHexPos)
        };
    }
}
