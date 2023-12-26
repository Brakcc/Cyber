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
                //branche N
                (0, -4, 0) => new Vector3Int(0, 4, 0),
                (0, -5, 0) => new Vector3Int(0, 5, 0),
                (0, -6, 0) => new Vector3Int(0, 6, 0),
                (0, -7, 0) => new Vector3Int(0, 7, 0),
                (0, -8, 0) => new Vector3Int(0, 8, 0),
                (0, -9, 0) => new Vector3Int(0, 9, 0),
                (0, -10, 0) => new Vector3Int(0, 10, 0),
                (0, -11, 0) => new Vector3Int(0, 11, 0),
                (0, -12, 0) => new Vector3Int(0, 12, 0),
                (0, -13, 0) => new Vector3Int(0, 13, 0),
                (0, -14, 0) => new Vector3Int(0, 14, 0),
                (0, -15, 0) => new Vector3Int(0, 15, 0),
                //branche EN
                (0, 0, 4) => new Vector3Int(4, 2, 0),
                (0, 0, 5) => new Vector3Int(5, 2, 0),
                (0, 0, 6) => new Vector3Int(6, 3, 0),
                (0, 0, 7) => new Vector3Int(7, 3, 0),
                (0, 0, 8) => new Vector3Int(8, 4, 0),
                (0, 0, 9) => new Vector3Int(9, 4, 0),
                (0, 0, 10) => new Vector3Int(10, 5, 0),
                (0, 0, 11) => new Vector3Int(11, 5, 0),
                (0, 0, 12) => new Vector3Int(12, 6, 0),
                (0, 0, 13) => new Vector3Int(13, 6, 0),
                (0, 0, 14) => new Vector3Int(14, 7, 0),
                (0, 0, 15) => new Vector3Int(15, 7, 0),
                //branche ES
                (-4, 0, 0) => new Vector3Int(4, -2, 0),
                (-5, 0, 0) => new Vector3Int(5, -3, 0),
                (-6, 0, 0) => new Vector3Int(6, -3, 0),
                (-7, 0, 0) => new Vector3Int(7, -4, 0),
                (-8, 0, 0) => new Vector3Int(8, -4, 0),
                (-9, 0, 0) => new Vector3Int(9, -5, 0),
                (-10, 0, 0) => new Vector3Int(10, -5, 0),
                (-11, 0, 0) => new Vector3Int(11, -6, 0),
                (-12, 0, 0) => new Vector3Int(12, -6, 0),
                (-13, 0, 0) => new Vector3Int(13, -7, 0),
                (-14, 0, 0) => new Vector3Int(14, -7, 0),
                (-15, 0, 0) => new Vector3Int(15, -8, 0),
                //branche S
                (0, 4,0) => new Vector3Int(0, -4, 0),
                (0, 5,0) => new Vector3Int(0, -5, 0),
                (0, 6,0) => new Vector3Int(0, -6, 0),
                (0, 7,0) => new Vector3Int(0, -7, 0),
                (0, 8,0) => new Vector3Int(0, -8, 0),
                (0, 9,0) => new Vector3Int(0, -9, 0),
                (0, 10,0) => new Vector3Int(0, -10, 0),
                (0, 11,0) => new Vector3Int(0, -11, 0),
                (0, 12,0) => new Vector3Int(0, -12, 0),
                (0, 13,0) => new Vector3Int(0, -13, 0),
                (0, 14,0) => new Vector3Int(0, -14, 0),
                (0, 15,0) => new Vector3Int(0, -15, 0),
                //branche WS
                (0, 0,-4) => new Vector3Int(-4, -2, 0),
                (0, 0,-5) => new Vector3Int(-5, -3, 0),
                (0, 0,-6) => new Vector3Int(-6, -3, 0),
                (0, 0,-7) => new Vector3Int(-7, -4, 0),
                (0, 0,-8) => new Vector3Int(-8, -4, 0),
                (0, 0,-9) => new Vector3Int(-9, -5, 0),
                (0, 0,-10) => new Vector3Int(-10, -5, 0),
                (0, 0,-11) => new Vector3Int(-11, -6, 0),
                (0, 0,-12) => new Vector3Int(-12, -6, 0),
                (0, 0,-13) => new Vector3Int(-13, -7, 0),
                (0, 0,-14) => new Vector3Int(-14, -7, 0),
                (0, 0,-15) => new Vector3Int(-15, -8, 0),
                //branche WN
                (4, 0, 0) => new Vector3Int(-4, 2, 0),
                (5, 0, 0) => new Vector3Int(-5, 2, 0),
                (6, 0, 0) => new Vector3Int(-6, 3, 0),
                (7, 0, 0) => new Vector3Int(-7, 3, 0),
                (8, 0, 0) => new Vector3Int(-8, 4, 0),
                (9, 0, 0) => new Vector3Int(-9, 4, 0),
                (10, 0, 0) => new Vector3Int(-10, 5, 0),
                (11, 0, 0) => new Vector3Int(-11, 5, 0),
                (12, 0, 0) => new Vector3Int(-12, 6, 0),
                (13, 0, 0) => new Vector3Int(-13, 6, 0),
                (14, 0, 0) => new Vector3Int(-14, 7, 0),
                (15, 0, 0) => new Vector3Int(-15, 7, 0),
                //default
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
                //branche N
                (0, -4, 0) => new Vector3Int(0, 4, 0),
                (0, -5, 0) => new Vector3Int(0, 5, 0),
                (0, -6, 0) => new Vector3Int(0, 6, 0),
                (0, -7, 0) => new Vector3Int(0, 7, 0),
                (0, -8, 0) => new Vector3Int(0, 8, 0),
                (0, -9, 0) => new Vector3Int(0, 9, 0),
                (0, -10, 0) => new Vector3Int(0, 10, 0),
                (0, -11, 0) => new Vector3Int(0, 11, 0),
                (0, -12, 0) => new Vector3Int(0, 12, 0),
                (0, -13, 0) => new Vector3Int(0, 13, 0),
                (0, -14, 0) => new Vector3Int(0, 14, 0),
                (0, -15, 0) => new Vector3Int(0, 15, 0),
                //branche EN
                (0, 0, 4) => new Vector3Int(4, 2, 0),
                (0, 0, 5) => new Vector3Int(5, 3, 0),
                (0, 0, 6) => new Vector3Int(6, 3, 0),
                (0, 0, 7) => new Vector3Int(7, 4, 0),
                (0, 0, 8) => new Vector3Int(8, 4, 0),
                (0, 0, 9) => new Vector3Int(9, 5, 0),
                (0, 0, 10) => new Vector3Int(10, 5, 0),
                (0, 0, 11) => new Vector3Int(11, 6, 0),
                (0, 0, 12) => new Vector3Int(12, 6, 0),
                (0, 0, 13) => new Vector3Int(13, 7, 0),
                (0, 0, 14) => new Vector3Int(14, 7, 0),
                (0, 0, 15) => new Vector3Int(15, 8, 0),
                //branche ES
                (-4, 0, 0) => new Vector3Int(4, -2, 0),
                (-5, 0, 0) => new Vector3Int(5, -2, 0),
                (-6, 0, 0) => new Vector3Int(6, -3, 0),
                (-7, 0, 0) => new Vector3Int(7, -3, 0),
                (-8, 0, 0) => new Vector3Int(8, -4, 0),
                (-9, 0, 0) => new Vector3Int(9, -4, 0),
                (-10, 0, 0) => new Vector3Int(10, -5, 0),
                (-11, 0, 0) => new Vector3Int(11, -5, 0),
                (-12, 0, 0) => new Vector3Int(12, -6, 0),
                (-13, 0, 0) => new Vector3Int(13, -6, 0),
                (-14, 0, 0) => new Vector3Int(14, -7, 0),
                (-15, 0, 0) => new Vector3Int(15, -7, 0),
                //branche S
                (0, 4,0) => new Vector3Int(0, -4, 0),
                (0, 5,0) => new Vector3Int(0, -5, 0),
                (0, 6,0) => new Vector3Int(0, -6, 0),
                (0, 7,0) => new Vector3Int(0, -7, 0),
                (0, 8,0) => new Vector3Int(0, -8, 0),
                (0, 9,0) => new Vector3Int(0, -9, 0),
                (0, 10,0) => new Vector3Int(0, -10, 0),
                (0, 11,0) => new Vector3Int(0, -11, 0),
                (0, 12,0) => new Vector3Int(0, -12, 0),
                (0, 13,0) => new Vector3Int(0, -13, 0),
                (0, 14,0) => new Vector3Int(0, -14, 0),
                (0, 15,0) => new Vector3Int(0, -15, 0),
                //branche WS
                (0, 0,-4) => new Vector3Int(-4, -2, 0),
                (0, 0,-5) => new Vector3Int(-5, -2, 0),
                (0, 0,-6) => new Vector3Int(-6, -3, 0),
                (0, 0,-7) => new Vector3Int(-7, -3, 0),
                (0, 0,-8) => new Vector3Int(-8, -4, 0),
                (0, 0,-9) => new Vector3Int(-9, -4, 0),
                (0, 0,-10) => new Vector3Int(-10, -5, 0),
                (0, 0,-11) => new Vector3Int(-11, -5, 0),
                (0, 0,-12) => new Vector3Int(-12, -6, 0),
                (0, 0,-13) => new Vector3Int(-13, -6, 0),
                (0, 0,-14) => new Vector3Int(-14, -7, 0),
                (0, 0,-15) => new Vector3Int(-15, -7, 0),
                //branche WN
                (4, 0, 0) => new Vector3Int(-4, 2, 0),
                (5, 0, 0) => new Vector3Int(-5, 3, 0),
                (6, 0, 0) => new Vector3Int(-6, 3, 0),
                (7, 0, 0) => new Vector3Int(-7, 4, 0),
                (8, 0, 0) => new Vector3Int(-8, 4, 0),
                (9, 0, 0) => new Vector3Int(-9, 5, 0),
                (10, 0, 0) => new Vector3Int(-10, 5, 0),
                (11, 0, 0) => new Vector3Int(-11, 6, 0),
                (12, 0, 0) => new Vector3Int(-12, 6, 0),
                (13, 0, 0) => new Vector3Int(-13, 7, 0),
                (14, 0, 0) => new Vector3Int(-14, 7, 0),
                (15, 0, 0) => new Vector3Int(-15, 8, 0),
                //default
                _ => new Vector3Int(0, 0, 0)
            }; 
        }
        #endregion
    }
}
