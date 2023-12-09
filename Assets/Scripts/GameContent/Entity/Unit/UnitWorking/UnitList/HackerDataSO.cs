using System.Collections.Generic;
using Enums.UnitEnums.UnitEnums;
using GameContent.Entity.Unit.KapasGen;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking.UnitList
{
    [CreateAssetMenu(fileName = "Hacker Datas", menuName = "Tactical/Units/Hacker Datas")]
    public class HackerDataSO : AbstractUnitSO
    {
        public override int ID => iD;
        [SerializeField] int iD;

        public override string Name => pname;
        [SerializeField] string pname;

        public override UnitType Type => type;
        [SerializeField] UnitType type;

        public override int NetworkRange => networkRange;
        [SerializeField] int networkRange;

        public override int MovePoints => movePoints;
        [SerializeField] int movePoints;

        public override int Attack => attack;
        [SerializeField] int attack;

        public override int Defense => defense;
        [SerializeField] int defense;

        public override int CritRate => critRate;
        [SerializeField] int critRate;

        public override int Speed => speed;
        [SerializeField] int speed;

        public override float HealthPoint { get => healthPoint; }
        [SerializeField] float healthPoint;

        public override Sprite Sprite { get => sprite; set { sprite = value; } }
        [SerializeField] Sprite sprite;

        public override List<AbstractKapaSO> KapasList { get => kapasList; set { kapasList = value; } }
        [SerializeField] List<AbstractKapaSO> kapasList;
    }
}
