using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DPS Datas", menuName = "Tactical/Units/DPS Datas")]
public class DPSDataSO : AUnitSO
{
    public override int ID => iD;
    [SerializeField] int iD;

    public override string Name => pname;
    [SerializeField] string pname;

    public override UnitType Type => type;
    [SerializeField] UnitType type;

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

    public override List<AKapaSO> KapasList { get => kapasList; set { kapasList = value; } }
    [SerializeField] List<AKapaSO> kapasList;
}
