using System.Collections.Generic;
using UnityEngine;

public interface IUnitData
{
    int ID { get; }
    string Name { get; }
    UnitType Type { get; }
    int NetworkRange { get; }
    int MovePoints { get; }
    int Attack { get; }
    int Defense { get; }
    int CritRate { get; }
    int Speed { get; }
    float HealthPoint { get; }
    Sprite Sprite {  get; }
    List<AKapaSO> KapasList { get; }
}

