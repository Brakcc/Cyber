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
    Hole,
    Computer
}

public enum Network
{
    Net0,
    Net1,
    Net2,
    Net3,
    Net4,
    Net5,
    Net6,
    Net7,
    Net8,
    Net9,
    Net10,
    Net11,
    Net12,
    Net13,
    Net14,
    Net15,
    Net16,
    Net17,
    Net18,
    Net19,
    None
}