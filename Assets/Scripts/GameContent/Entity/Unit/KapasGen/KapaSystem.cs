using System.Collections.Generic;
using GameContent.GridManagement;
using GameContent.GridManagement.HexPathFind;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen
{
    public class KapaSystem
    {
        #region fields

        private PathResult _kapaRange;
        private PathResult _perfectRange;
        private List<Vector3Int> _activeTiles = new();
        private List<Vector3Int> _perfectTiles = new();
        
        #endregion

        #region methodes

        private static PathResult GetKapaRange(IEntity unit, HexGridStore hexGrid) =>
            PathFind.PathKapaVerif(hexGrid, unit.CurrentHexPos, Constants.MaxReach);
        private static PathResult GetPerfectPath(IEntity unit, HexGridStore hexGrid) =>
            PathFind.PerfectPath(hexGrid, unit.CurrentHexPos, Constants.MaxReach);

        /// <summary>
        /// Verifie le chemin parfait et obstacle pour comparer et determiner si le chemin est
        /// impacte par un obstacle
        /// </summary>
        /// <param name="kapaTile"></param>
        /// <param name="u"></param>
        /// <param name="hgs"></param>
        /// <param name="mPp"></param>
        /// <returns></returns>
        public bool VerifyKapaRange(Vector3Int kapaTile, IUnit u, HexGridStore hgs, int mPp)
        {
            _kapaRange = GetKapaRange(u, hgs);
            _perfectRange = GetPerfectPath(u, hgs);

            _activeTiles = _kapaRange.GetKapaPathTo(kapaTile, hgs, mPp);
            _perfectTiles = _perfectRange.GetPathTo(kapaTile);
            
            return _activeTiles.Count == _perfectTiles.Count;
        }
        
        #endregion
    }
}