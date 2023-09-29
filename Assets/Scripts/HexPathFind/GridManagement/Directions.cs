using UnityEngine;
using System.Collections.Generic;

public static class Direction
{
    public static List<Vector3Int> directionOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(0, 1, 0), //N
        new Vector3Int(1, 0, 0), //E1
        new Vector3Int(1, -1, 0), //E2
        new Vector3Int(0, -1, 0), //S
        new Vector3Int(-1, -1, 0), //W1
        new Vector3Int(-1, 0, 0), //W2
    };

    public static List<Vector3Int> directionOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0, 1, 0), //N
        new Vector3Int(1, 1, 0), //E1
        new Vector3Int(1, 0, 0), //E2
        new Vector3Int(0, -1, 0), //S
        new Vector3Int(-1, 0, 0), //W1
        new Vector3Int(-1, 1, 0), //W2
    };

    /// <summary>
    /// détermine la parité d'une tuile hexagonale dans une grid hexagonale en Flat-Top
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static List<Vector3Int> GetDirectionList(int x) => x % 2 == 0 ? directionOffsetEven : directionOffsetOdd;
}