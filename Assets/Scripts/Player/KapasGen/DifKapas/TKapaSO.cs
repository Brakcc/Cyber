using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Kapa", menuName = "Tactical/Kapas/Turret")]
public class TKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName { get => kapaName; }
    [SerializeField] string kapaName;
    public override int ID { get => id; }
    [SerializeField] int id;
    public override string Description { get => description; }
    [SerializeField] string description;
    public override int Cost { get => 0; }
    public override int MaxPlayerPierce => 0;
    public override EffectType EffectType { get => EffectType.None; }
    public override KapaType KapaType { get => kapaType; }
    [SerializeField] KapaType kapaType;
    public override KapaFunctionType KapaFunctionType { get => KapaFunctionType.Default; }
    public override KapaUISO KapaUI { get => kapaUI; }
    [SerializeField] KapaUISO kapaUI;
    public override Vector3Int[] Patern { get => patern; }
    [SerializeField] private Vector3Int[] patern;

    [SerializeField] GameObject turret;
    #endregion

    #region inherited paterns/accessors
    //North tiles
    public override Vector3Int[] OddNTiles { get => oddNTiles; set { oddNTiles = value; } }
    private Vector3Int[] oddNTiles;
    public override Vector3Int[] EvenNTiles { get => evenNTiles; set { evenNTiles = value; } }
    private Vector3Int[] evenNTiles;

    //EN tiles
    public override Vector3Int[] OddENTiles { get => oddENTiles; set { oddENTiles = value; } }
    private Vector3Int[] oddENTiles;
    public override Vector3Int[] EvenENTiles { get => evenENTiles; set { evenENTiles = value; } }
    private Vector3Int[] evenENTiles;

    //ES tiles
    public override Vector3Int[] OddESTiles { get => oddESTiles; set { oddESTiles = value; } }
    private Vector3Int[] oddESTiles;
    public override Vector3Int[] EvenESTiles { get => evenESTiles; set { evenESTiles = value; } }
    private Vector3Int[] evenESTiles;

    //S tiles
    public override Vector3Int[] OddSTiles { get => oddSTiles; set { oddSTiles = value; } }
    private Vector3Int[] oddSTiles;
    public override Vector3Int[] EvenSTiles { get => evenSTiles; set { evenSTiles = value; } }
    private Vector3Int[] evenSTiles;

    //WS tiles
    public override Vector3Int[] OddWSTiles { get => oddWSTiles; set { oddWSTiles = value; } }
    private Vector3Int[] oddWSTiles;
    public override Vector3Int[] EvenWSTiles { get => evenWSTiles; set { evenWSTiles = value; } }
    private Vector3Int[] evenWSTiles;

    //WN tiles
    public override Vector3Int[] OddWNTiles { get => oddWNTiles; set { oddWNTiles = value; } }
    private Vector3Int[] oddWNTiles;
    public override Vector3Int[] EvenWNTiles { get => evenWNTiles; set { evenWNTiles = value; } }
    private Vector3Int[] evenWNTiles;
    #endregion

    #region inherited methodes
    public override bool OnCheckKapaPoints(Unit unit) => GameLoopManager.gLM.TurretNumber[unit.TeamNumber] > 0;

    public override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, Unit unit)
    {
        if (GameLoopManager.gLM.TurretNumber[unit.TeamNumber] <= 0) return;

        Instantiate(turret, hexGrid.GetTile(pattern[0]).transform.position, Quaternion.identity, hexGrid.transform);
        GameLoopManager.gLM.HandleTurretUse(unit.TeamNumber);
        hexGrid.GetTile(pattern[0]).HasEntityOnIt = true;

        OnDeselectTiles(hexGrid, pattern);
    }
    #endregion
}