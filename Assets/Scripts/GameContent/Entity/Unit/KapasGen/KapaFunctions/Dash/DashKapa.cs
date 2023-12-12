using System.Threading.Tasks;
using GameContent.GridManagement;
using Interfaces.Unit;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Dash
{
    public static class DashKapa
    {
        public static async void OnSecondKapa(HexGridStore hexGrid, IUnit unit, IUnit target)
        {
            var xDiff = target.CurrentWorldPos.x - unit.CurrentWorldPos.x;
            var yDiff = target.CurrentWorldPos.y - unit.CurrentWorldPos.y;

            var targetTile = OnGetTargetTile(hexGrid, xDiff, yDiff, unit, target);
            
            await Task.Delay(ConstList.DashGrabDelay);
            
            ChangeUnitHexPos(unit, targetTile);
            
            unit.MoveInFrontOf(targetTile.transform.position);
        }

        private static void ChangeUnitHexPos(IUnit u, Hex tT)
        {
            HexGridStore.hGs.GetTile(u.CurrentHexPos).HasEntityOnIt = false;
            HexGridStore.hGs.GetTile(u.CurrentHexPos).ClearUnit();
            tT.HasEntityOnIt = true;
            tT.SetUnit(u);
            u.CurrentHexPos = tT.HexCoords;
        }
        
        private static Hex OnGetTargetTile(HexGridStore hexG, float x, float y, IEntity u, IEntity t) => (x, y) switch
        {
            ( 0, < -ConstList.DashOffSetValue) => hexG.GetNorthTile(t.CurrentHexPos),
            ( < -ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetEastNorthTile(t.CurrentHexPos),
            ( < -ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetEastSouthTile(t.CurrentHexPos),
            ( 0, > ConstList.DashOffSetValue) => hexG.GetSouthTile(t.CurrentHexPos),
            ( > ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetWestSouthTile(t.CurrentHexPos),
            ( > ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetWestNorthTile(t.CurrentHexPos),
            _ => hexG.GetTile(u.CurrentHexPos)
        };
    }
}