using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DPS Datas", menuName = "Tactical/Units/DPS Datas")]
public class DPSDataSO : AUnitSO
{
    public override int MovePoints => movePoints;
    [SerializeField] private int movePoints;

    public override int Speed => speed;
    [SerializeField] private int speed;

    public override int HealthPoint { get => healthPoint; set { healthPoint = value; } }
    [SerializeField] private int healthPoint;

    public override List<AKapaSO> KapasList { get => kapasList; set { kapasList = value; } }
    [SerializeField] private List<AKapaSO> kapasList;
}
