using System.Collections.Generic;
using UnityEngine;

public interface IUnitData
{
    int ID { get; }
    UnitType Type { get; }
    int MovePoints { get; }
    int Attack { get; }
    int Defense { get; }
    int Speed { get; }
    float HealthPoint { get; }
    Sprite Sprite {  get; }
    List<AKapaSO> KapasList { get; }
}

public enum UnitType
{
    Default,
    Tank,
    DPS,
    Hacker
}