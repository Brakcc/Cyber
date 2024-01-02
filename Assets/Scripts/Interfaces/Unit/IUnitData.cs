using System.Collections.Generic;
using Enums.UnitEnums.UnitEnums;
using GameContent.Entity.Unit.KapasGen;
using UnityEngine;

namespace Interfaces.Unit
{
    public interface IUnitData
    {
        int ID { get; }
        string Name { get; }
        UnitType Type { get; }
        int MovePoints { get; }
        int Attack { get; }
        int Defense { get; }
        int CritRate { get; }
        int Speed { get; }
        float HealthPoint { get; }
        Sprite Sprite {  get; }
        List<AbstractKapaSO> KapasList { get; }
    }
}

