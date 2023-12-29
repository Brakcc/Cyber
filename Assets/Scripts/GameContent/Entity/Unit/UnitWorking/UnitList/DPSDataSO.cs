using System.Collections.Generic;
using Enums.UnitEnums.UnitEnums;
using GameContent.Entity.Unit.KapasGen;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking.UnitList
{
    [CreateAssetMenu(fileName = "DPS Datas", menuName = "Tactical/Units/DPS Datas")]
    public class DPSDataSO : AbstractUnitSO
    {
        public override int ID => iD;
        [SerializeField] private int iD;

        public override string Name => pname;
        [SerializeField] private string pname;

        public override UnitType Type => type;
        [SerializeField] private UnitType type;

        public override int NetworkRange => 0;

        public override int MovePoints => movePoints;
        [SerializeField] private int movePoints;

        public override int Attack => attack;
        [SerializeField] private int attack;

        public override int Defense => defense;
        [SerializeField] private int defense;

        public override int CritRate => critRate;
        [SerializeField] private int critRate;

        public override int Speed => speed;
        [SerializeField] private int speed;

        public override float HealthPoint => healthPoint;
        [SerializeField] private float healthPoint;

        public override Sprite Sprite { get => sprite; set => sprite = value; }
        [SerializeField] private Sprite sprite;

        public override List<AbstractKapaSO> KapasList { get => kapasList; set => kapasList = value; }
        [SerializeField] private List<AbstractKapaSO> kapasList;
    }
}
