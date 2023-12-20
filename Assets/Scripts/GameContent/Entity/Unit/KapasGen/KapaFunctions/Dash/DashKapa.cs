using System.Threading.Tasks;
using Enums.UnitEnums.KapaEnums;
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
            HexGridStore.hGs.GetTile(u.CurrentHexPos).ClearEntity();
            tT.HasEntityOnIt = true;
            tT.SetEntity(u);
            u.CurrentHexPos = tT.HexCoords;
        }
        
        private static Hex OnGetTargetTile(HexGridStore hexG, float x, float y, IEntity u, IEntity t) => (x, y) switch
        {
            ( 0, < -ConstList.DashOffSetValue) => hexG.GetDirectionTile(t.CurrentHexPos, KapaDir.North),
            ( < -ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetDirectionTile(t.CurrentHexPos, KapaDir.NorthEast),
            ( < -ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetDirectionTile(t.CurrentHexPos, KapaDir.SouthEast),
            ( 0, > ConstList.DashOffSetValue) => hexG.GetDirectionTile(t.CurrentHexPos, KapaDir.South),
            ( > ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetDirectionTile(t.CurrentHexPos, KapaDir.SouthWest),
            ( > ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetDirectionTile(t.CurrentHexPos, KapaDir.NorthWest),
            _ => hexG.GetTile(u.CurrentHexPos)
        };
    }
}