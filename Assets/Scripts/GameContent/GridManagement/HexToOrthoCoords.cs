using System.Threading.Tasks;
using UnityEngine;

public static class HexToOrthoCoords
{
    #region conversion Impaire
    /// <summary>
    /// Convertit de façon bourrine mais assez opti une coordonnée hexa en 3D algébrique en 
    /// 2D algébrique d'un plan orthonormé, pour une tuile en coord orthoN Impaire/Odd
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    public static async Task<Vector3Int> GetOddOrthoCoord(Vector3Int u)
    {
        await Task.Delay(0);
        return (u.x, u.y, u.z) switch
        {
            //couronne 1
            (0, -1, 0) => new(0, 1, 0),
            (0, 0, 1) => new(1, 0, 0),
            (-1, 0, 0) => new(1, -1, 0),
            (0, 1, 0) => new(0, -1, 0),
            (0, 0, -1) => new(-1, -1, 0),
            (1, 0, 0) => new(-1, 0, 0),
            //couronne 2
            (0, -2, 0) => new(0, 2, 0),
            (0, -1, 1) => new(1, 1, 0),
            (0, 0, 2) => new(2, 1, 0),
            (-1, 0, 1) => new(2, 0, 0),
            (-2, 0, 0) => new(2, -1, 0),
            (-1, 1, 0) => new(1, -2, 0),
            (0, 2, 0) => new(0, -2, 0),
            (0, 1, -1) => new(-1, -2, 0),
            (0, 0, -2) => new(-2, -1, 0),
            (1, 0, -1) => new(-2, 0, 0),
            (2, 0, 0) => new(-2, 1, 0),
            (1, -1, 0) => new(-1, 1, 0),
            //couronne 3
            (0, -3, 0) => new(0, 3, 0),
            (0, -2, 1) => new(1, 2, 0),
            (0, -1, 2) => new(2, 2, 0),
            (0, 0, 3) => new(3, 1, 0),
            (-1, 0, 2) => new(3, 0, 0),
            (-2, 0, 1) => new(3, -1, 0),
            (-3, 0, 0) => new(3, -2, 0),
            (-2, 1, 0) => new(2, -2, 0),
            (-1, 2, 0) => new(1, -3, 0),
            (0, 3, 0) => new(0, -3, 0),
            (0, 2, -1) => new(-1, -3, 0),
            (0, 1, -2) => new(-2, -2, 0),
            (0, 0, -3) => new(-3, -2, 0),
            (1, 0, -2) => new(-3, -1, 0),
            (2, 0, -1) => new(-3, 0, 0),
            (3, 0, 0) => new(-3, 1, 0),
            (2, -1, 0) => new(-2, 2, 0),
            (1, -2, 0) => new(-1, 2, 0),
            _ => new(0, 0, 0)
        };
    }
    #endregion

    #region conversion Paire
    /// <summary>
    /// Convertit de façon bourrine mais assez opti une coordonnée hexa en 3D algébrique en 
    /// 2D algébrique d'un plan orthonormé, pour une tuile en coord orthoN Paire/Even
    /// </summary>
    /// <param name="u"></param>
    /// <returns></returns>
    public static async Task<Vector3Int> GetEvenOrthoCoord(Vector3Int u)
    {
        await Task.Delay(0);
        return (u.x, u.y, u.z) switch
        {
            //couronne 1
            (0, -1, 0) => new(0, 1, 0),
            (0, 0, 1) => new(1, 1, 0),
            (-1, 0, 0) => new(1, 0, 0),
            (0, 1, 0) => new(0, -1, 0),
            (0, 0, -1) => new(-1, 0, 0),
            (1, 0, 0) => new(-1, 1, 0),
            //couronne 2
            (0, -2, 0) => new(0, 2, 0),
            (0, -1, 1) => new(1, 2, 0),
            (0, 0, 2) => new(2, 1, 0),
            (-1, 0, 1) => new(2, 0, 0),
            (-2, 0, 0) => new(2, -1, 0),
            (-1, 1, 0) => new(1, -1, 0),
            (0, 2, 0) => new(0, -2, 0),
            (0, 1, -1) => new(-1, -1, 0),
            (0, 0, -2) => new(-2, -1, 0),
            (1, 0, -1) => new(-2, 0, 0),
            (2, 0, 0) => new(-2, 1, 0),
            (1, -1, 0) => new(-1, 2, 0),
            //couronne 3
            (0, -3, 0) => new(0, 3, 0),
            (0, -2, 1) => new(1, 3, 0),
            (0, -1, 2) => new(2, 2, 0),
            (0, 0, 3) => new(3, 2, 0),
            (-1, 0, 2) => new(3, 1, 0),
            (-2, 0, 1) => new(3, 0, 0),
            (-3, 0, 0) => new(3, -1, 0),
            (-2, 1, 0) => new(2, -2, 0),
            (-1, 2, 0) => new(1, -2, 0),
            (0, 3, 0) => new(0, -3, 0),
            (0, 2, -1) => new(-1, -2, 0),
            (0, 1, -2) => new(-2, -2, 0),
            (0, 0, -3) => new(-3, -1, 0),
            (1, 0, -2) => new(-3, 0, 0),
            (2, 0, -1) => new(-3, 1, 0),
            (3, 0, 0) => new(-3, 2, 0),
            (2, -1, 0) => new(-2, 2, 0),
            (1, -2, 0) => new(-1, 3, 0),
            _ => new(0, 0, 0)
        }; 
    }
    #endregion
}
