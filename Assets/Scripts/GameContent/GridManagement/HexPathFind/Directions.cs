using UnityEngine;
using System.Collections.Generic;

public static class Direction
{
    public static List<Vector3Int> directionOffsetOdd = new()
    {
        new Vector3Int(0, 1, 0), //N
        new Vector3Int(1, 0, 0), //E-N
        new Vector3Int(1, -1, 0), //E-S
        new Vector3Int(0, -1, 0), //S
        new Vector3Int(-1, -1, 0), //W-S
        new Vector3Int(-1, 0, 0), //W-N
    };

    public static List<Vector3Int> directionOffsetEven = new()
    {
        new Vector3Int(0, 1, 0), //N
        new Vector3Int(1, 1, 0), //E-N
        new Vector3Int(1, 0, 0), //E-S
        new Vector3Int(0, -1, 0), //S
        new Vector3Int(-1, 0, 0), //W-S
        new Vector3Int(-1, 1, 0), //W-N
    };

    /// <summary>
    /// détermine la parité d'une tuile hexagonale dans une grid hexagonale en Flat-Top
    /// </summary>
    /// <param name="x">direction de non-alignement des tuiles</param>
    /// <returns></returns>
    public static List<Vector3Int> GetDirectionList(int x) => x % 2 == 0 ? directionOffsetEven : directionOffsetOdd;

    /// <summary>
    /// revoit un bool sur la parité d'une tuile, celle sur laquelle une Unit est posée
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static bool IsPariryEven(int x) => x % 2 == 0;
}