using UnityEngine;

public interface IHexCoord
{
    public Vector3Int OffsetCoordonnees { get; set; }
}

public enum HexType
{
    Default,
    Walkable,
    Obstacle,
    Hole
}