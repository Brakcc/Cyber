using System.Threading.Tasks;
using Enums.UnitEnums.KapaEnums;
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
            HexGridStore.hGs.GetTile(uT.CurrentHexPos).ClearEntity();
            tT.HasEntityOnIt = true;
            tT.SetEntity(uT);
            uT.CurrentHexPos = tT.HexCoords;
        }
        
        private static Hex OnGetTargetTile(HexGridStore hexG, float x, float y, IEntity t, IEntity u) => (x, y) switch
        {
            ( 0, < -ConstList.DashOffSetValue) => hexG.GetDirectionTile(u.CurrentHexPos, KapaDir.North),
            ( < -ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetDirectionTile(u.CurrentHexPos, KapaDir.NorthEast),
            ( < -ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetDirectionTile(u.CurrentHexPos, KapaDir.SouthEast),
            ( 0, > ConstList.DashOffSetValue) => hexG.GetDirectionTile(u.CurrentHexPos, KapaDir.South),
            ( > ConstList.DashOffSetValue, > ConstList.DashOffSetValue) => hexG.GetDirectionTile(u.CurrentHexPos, KapaDir.SouthWest),
            ( > ConstList.DashOffSetValue, < -ConstList.DashOffSetValue) => hexG.GetDirectionTile(u.CurrentHexPos, KapaDir.NorthWest),
            _ => hexG.GetTile(t.CurrentHexPos)
        };
    }
}
