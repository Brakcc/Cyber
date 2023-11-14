using System.Collections.Generic;
using UnityEngine;

public interface IUnitData
{
    int ID { get; }
    int MovePoints { get; }
    int Speed { get; }
    int HealthPoint { get; }
    Sprite Sprite {  get; }
    List<AKapaSO> KapasList { get; }
}
