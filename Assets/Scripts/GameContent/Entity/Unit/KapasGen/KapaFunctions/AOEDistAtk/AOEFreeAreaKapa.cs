using System.Collections.Generic;
using System.Linq;
using GameContent.GridManagement;
using GameContent.GridManagement.HexPathFind;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.KapaFunctions.AOEDistAtk
{
    public static class AOEFreeAreaKapa
    {
        public static List<Vector3Int> GetThrowRange(Vector3Int pos, HexGridStore hexGrid, int range)
            => PathFind.PathKapaVerif(hexGrid, pos, range).GetRangePositions().ToList();

        public static IEnumerable<Vector3Int> GetAtkArea(Vector3Int pos, HexGridStore hexGrid)
            => hexGrid.GetNeighbourgs(pos);
    }
}