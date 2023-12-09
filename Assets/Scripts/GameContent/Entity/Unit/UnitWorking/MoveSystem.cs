using System.Collections.Generic;
using System.Linq;
using GameContent.GridManagement;
using GameContent.GridManagement.HexPathFind;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking
{
    public class MoveSystem
    {
        #region fields
        PathResult _moveRange;
        List<Vector3Int> _currentPath = new();
        #endregion

        #region methodes
        public void HideRange(HexGridStore hexGrid)
        {
            foreach (Vector3Int hexPos in _moveRange.GetRangePositions()) { hexGrid.GetTile(hexPos).DisableGlowPath(); hexGrid.GetTile(hexPos).DisableGlow(); }
            _moveRange = new PathResult();
        }

        public void ShowRange(IUnit selected, HexGridStore hexGrid)
        {
            CalculateRange(selected, hexGrid);
            var unitPos = selected.CurrentHexPos;

            foreach (Vector3Int hexPos in _moveRange.GetRangePositions()) 
            {
                if (unitPos == hexPos) continue;
                hexGrid.GetTile(hexPos).EnableGlow(); 
            }
        }

        public void CalculateRange(IUnit selects, HexGridStore hexGrid) => _moveRange = PathFind.PathGetRange(hexGrid, selects.CurrentHexPos, selects.UnitData.MovePoints);

        public void ShowPath(Vector3Int selects,  HexGridStore hexGrid)
        {
            if (_moveRange.GetRangePositions().Contains(selects))
            {
                foreach (Vector3Int hexPos in _currentPath) { hexGrid.GetTile(hexPos).DisableGlowPath(); }
                _currentPath = _moveRange.GetPathTo(selects);
                foreach (Vector3Int hexPos in _currentPath) { hexGrid.GetTile(hexPos).EnableGlowPath(); }
            }
        }

        public void MoveUnit(IUnit selects, HexGridStore hexGrid) => selects.MoveOnPath(_currentPath.Select(pos => hexGrid.GetTile(pos).transform.position).ToList());

        public bool IsHexInRange(Vector3Int hexPos) => _moveRange.IsHexPosInRange(hexPos);
    
        #endregion
    }
}
