using UnityEngine;
using GameContent.Entity.Unit.UnitWorking;
using GameContent.GridManagement;
using Interfaces.Unit;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.Dash
{
    public static class DashKapa
    {
        public static void OnSecondKapa(UnitManager uM, Vector3Int targetPos)
        {
            uM.moveSys.CalculateRange(uM.SelectedUnit, HexGridStore.hGs);
            
            var tampDashPath = uM.moveSys.GetPath(targetPos);
            
            uM.moveSys.MoveInFront(uM.SelectedUnit, HexGridStore.hGs);
        }
    }
}