using System.Threading.Tasks;
using UnityEngine;

namespace GameContent.GridManagement
{
    public static class HexToOrthoCoords
    {
        #region conversion Impaire
        /// <summary>
        /// Convertit de façon bourrine mais obligatoire une coordonnée hexa en 3D algébrique en 
        /// 2D algébrique d'un plan orthonormé, pour une tuile en coord orthoN Impaire/Odd
        /// Appelé /!\ UNE SEULE FOIS /!\ en multithreading en lancement de partie 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static async Task<Vector3Int> GetOddOrthoCoord(Vector3Int u)
        {
            await Task.Yield();
            return (u.x, u.y, u.z) switch
            {
                //couronne 1
                (0, -1, 0) => new Vector3Int(0, 1, 0),
                (0, 0, 1) => new Vector3Int(1, 0, 0),
                (-1, 0, 0) => new Vector3Int(1, -1, 0),
                (0, 1, 0) => new Vector3Int(0, -1, 0),
                (0, 0, -1) => new Vector3Int(-1, -1, 0),
                (1, 0, 0) => new Vector3Int(-1, 0, 0),
                //couronne 2
                (0, -2, 0) => new Vector3Int(0, 2, 0),
                (0, -1, 1) => new Vector3Int(1, 1, 0),
                (0, 0, 2) => new Vector3Int(2, 1, 0),
                (-1, 0, 1) => new Vector3Int(2, 0, 0),
                (-2, 0, 0) => new Vector3Int(2, -1, 0),
                (-1, 1, 0) => new Vector3Int(1, -2, 0),
                (0, 2, 0) => new Vector3Int(0, -2, 0),
                (0, 1, -1) => new Vector3Int(-1, -2, 0),
                (0, 0, -2) => new Vector3Int(-2, -1, 0),
                (1, 0, -1) => new Vector3Int(-2, 0, 0),
                (2, 0, 0) => new Vector3Int(-2, 1, 0),
                (1, -1, 0) => new Vector3Int(-1, 1, 0),
                //couronne 3
                (0, -3, 0) => new Vector3Int(0, 3, 0),
                (0, -2, 1) => new Vector3Int(1, 2, 0),
                (0, -1, 2) => new Vector3Int(2, 2, 0),
                (0, 0, 3) => new Vector3Int(3, 1, 0),
                (-1, 0, 2) => new Vector3Int(3, 0, 0),
                (-2, 0, 1) => new Vector3Int(3, -1, 0),
                (-3, 0, 0) => new Vector3Int(3, -2, 0),
                (-2, 1, 0) => new Vector3Int(2, -2, 0),
                (-1, 2, 0) => new Vector3Int(1, -3, 0),
                (0, 3, 0) => new Vector3Int(0, -3, 0),
                (0, 2, -1) => new Vector3Int(-1, -3, 0),
                (0, 1, -2) => new Vector3Int(-2, -2, 0),
                (0, 0, -3) => new Vector3Int(-3, -2, 0),
                (1, 0, -2) => new Vector3Int(-3, -1, 0),
                (2, 0, -1) => new Vector3Int(-3, 0, 0),
                (3, 0, 0) => new Vector3Int(-3, 1, 0),
                (2, -1, 0) => new Vector3Int(-2, 2, 0),
                (1, -2, 0) => new Vector3Int(-1, 2, 0),
                _ => new Vector3Int(0, 0, 0)
            };
        }
        #endregion

        #region conversion Paire
        /// <summary>
        /// Convertit de façon bourrine mais obligatoire une coordonnée hexa en 3D algébrique en 
        /// 2D algébrique d'un plan orthonormé, pour une tuile en coord orthoN Paire/Even
        /// Appelé /!\ UNE SEULE FOIS /!\ en multithreading en lancement de partie 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static async Task<Vector3Int> GetEvenOrthoCoord(Vector3Int u)
        {
            await Task.Yield();
            return (u.x, u.y, u.z) switch
            {
                //couronne 1
                (0, -1, 0) => new Vector3Int(0, 1, 0),
                (0, 0, 1) => new Vector3Int(1, 1, 0),
                (-1, 0, 0) => new Vector3Int(1, 0, 0),
                (0, 1, 0) => new Vector3Int(0, -1, 0),
                (0, 0, -1) => new Vector3Int(-1, 0, 0),
                (1, 0, 0) => new Vector3Int(-1, 1, 0),
                //couronne 2
                (0, -2, 0) => new Vector3Int(0, 2, 0),
                (0, -1, 1) => new Vector3Int(1, 2, 0),
                (0, 0, 2) => new Vector3Int(2, 1, 0),
                (-1, 0, 1) => new Vector3Int(2, 0, 0),
                (-2, 0, 0) => new Vector3Int(2, -1, 0),
                (-1, 1, 0) => new Vector3Int(1, -1, 0),
                (0, 2, 0) => new Vector3Int(0, -2, 0),
                (0, 1, -1) => new Vector3Int(-1, -1, 0),
                (0, 0, -2) => new Vector3Int(-2, -1, 0),
                (1, 0, -1) => new Vector3Int(-2, 0, 0),
                (2, 0, 0) => new Vector3Int(-2, 1, 0),
                (1, -1, 0) => new Vector3Int(-1, 2, 0),
                //couronne 3
                (0, -3, 0) => new Vector3Int(0, 3, 0),
                (0, -2, 1) => new Vector3Int(1, 3, 0),
                (0, -1, 2) => new Vector3Int(2, 2, 0),
                (0, 0, 3) => new Vector3Int(3, 2, 0),
                (-1, 0, 2) => new Vector3Int(3, 1, 0),
                (-2, 0, 1) => new Vector3Int(3, 0, 0),
                (-3, 0, 0) => new Vector3Int(3, -1, 0),
                (-2, 1, 0) => new Vector3Int(2, -2, 0),
                (-1, 2, 0) => new Vector3Int(1, -2, 0),
                (0, 3, 0) => new Vector3Int(0, -3, 0),
                (0, 2, -1) => new Vector3Int(-1, -2, 0),
                (0, 1, -2) => new Vector3Int(-2, -2, 0),
                (0, 0, -3) => new Vector3Int(-3, -1, 0),
                (1, 0, -2) => new Vector3Int(-3, 0, 0),
                (2, 0, -1) => new Vector3Int(-3, 1, 0),
                (3, 0, 0) => new Vector3Int(-3, 2, 0),
                (2, -1, 0) => new Vector3Int(-2, 2, 0),
                (1, -2, 0) => new Vector3Int(-1, 3, 0),
                _ => new Vector3Int(0, 0, 0)
            }; 
        }
        #endregion
    }
}
