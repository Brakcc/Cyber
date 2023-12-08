using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Kapa", menuName = "Tactical/Kapas/Turret")]
public class TurretKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName => _kapaName;
    [SerializeField] string _kapaName;
    public override int ID => _id;
    [SerializeField] int _id;
    public override string Description => _description;
    [SerializeField] string _description;
    public override int Cost => 0;
    public override int MaxPlayerPierce => 0;
    public override float BalanceCoeff => 0;
    public override EffectType EffectType => EffectType.None;
    public override KapaType KapaType => _kapaType;
    [SerializeField] KapaType _kapaType;
    public override KapaFunctionType KapaFunctionType => KapaFunctionType.Default;
    public override KapaUISO KapaUI => _kapaUI;
    [SerializeField] KapaUISO _kapaUI;
    public override GameObject DamageFeedBack => null;
    public override Vector3Int[] Patterns => _pattern;
    [SerializeField] Vector3Int[] _pattern;

    [SerializeField] GameObject _turret;
    #endregion

    #region inherited paterns/accessors
    //North tiles
    public override Vector3Int[] OddNTiles { get => _oddNTiles; protected set => _oddNTiles = value; }
    private Vector3Int[] _oddNTiles;
    public override Vector3Int[] EvenNTiles { get => _evenNTiles; protected set => _evenNTiles = value; }
    private Vector3Int[] _evenNTiles;

    //EN tiles
    public override Vector3Int[] OddENTiles { get => _oddENTiles; protected set => _oddENTiles = value; }
    private Vector3Int[] _oddENTiles;
    public override Vector3Int[] EvenENTiles { get => _evenENTiles; protected set => _evenENTiles = value; }
    private Vector3Int[] _evenENTiles;

    //ES tiles
    public override Vector3Int[] OddESTiles { get => _oddESTiles; protected set => _oddESTiles = value; }
    private Vector3Int[] _oddESTiles;
    public override Vector3Int[] EvenESTiles { get => _evenESTiles; protected set => _evenESTiles = value; }
    private Vector3Int[] _evenESTiles;

    //S tiles
    public override Vector3Int[] OddSTiles { get => _oddSTiles; protected set => _oddSTiles = value; }
    private Vector3Int[] _oddSTiles;
    public override Vector3Int[] EvenSTiles { get => _evenSTiles; protected set => _evenSTiles = value; }
    private Vector3Int[] _evenSTiles;

    //WS tiles
    public override Vector3Int[] OddWSTiles { get => _oddWSTiles; protected set => _oddWSTiles = value; }
    private Vector3Int[] _oddWSTiles;
    public override Vector3Int[] EvenWSTiles { get => _evenWSTiles; protected set => _evenWSTiles = value; }
    private Vector3Int[] _evenWSTiles;

    //WN tiles
    public override Vector3Int[] OddWNTiles { get => _oddWNTiles; protected set => _oddWNTiles = value; }
    private Vector3Int[] _oddWNTiles;
    public override Vector3Int[] EvenWNTiles { get => _evenWNTiles; protected set => _evenWNTiles = value; }
    private Vector3Int[] _evenWNTiles;
    #endregion

    #region inherited methodes
    public sealed override bool OnCheckKapaPoints(IUnit unit) => GameLoopManager.gLM.TurretNumber[unit.TeamNumber] > 0;

    public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
    {
        if (GameLoopManager.gLM.TurretNumber[unit.TeamNumber] <= 0) return;

        Instantiate(_turret, hexGrid.GetTile(pattern[0]).transform.position, Quaternion.identity, hexGrid.transform);
        GameLoopManager.gLM.HandleTurretUse(unit.TeamNumber);
        hexGrid.GetTile(pattern[0]).HasEntityOnIt = true;

        OnDeselectTiles(hexGrid, pattern);
    }
    #endregion
}