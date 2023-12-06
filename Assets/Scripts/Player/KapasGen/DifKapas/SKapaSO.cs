using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skip Kapa", menuName = "Tactical/Kapas/Skip")]
public class SKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName => kapaName; 
    [SerializeField] string kapaName;
    public override int ID => id;
    [SerializeField] int id;
    public override string Description => description;
    [SerializeField] string description;
    public override int Cost => 0;
    public override int MaxPlayerPierce => 0;
    public override EffectType EffectType => EffectType.None;
    public override KapaType KapaType => kapaType;
    [SerializeField] KapaType kapaType;
    public override KapaFunctionType KapaFunctionType => KapaFunctionType.Default;
    public override KapaUISO KapaUI => kapaUI;
    [SerializeField] KapaUISO kapaUI;
    public override GameObject DamageFeedBack => null;
    public override Vector3Int[] Patern => null;
    #endregion

    #region inherited paterns/accessors
    //North tiles
    public override Vector3Int[] OddNTiles { get => null; set { } }
    public override Vector3Int[] EvenNTiles { get => null; set { } }

    //EN tiles
    public override Vector3Int[] OddENTiles { get => null; set { } }
    public override Vector3Int[] EvenENTiles { get => null; set { } }

    //ES tiles
    public override Vector3Int[] OddESTiles { get => null; set { } }
    public override Vector3Int[] EvenESTiles { get => null; set { } }

    //S tiles
    public override Vector3Int[] OddSTiles { get => null; set { } }
    public override Vector3Int[] EvenSTiles { get => null; set { } }

    //WS tiles
    public override Vector3Int[] OddWSTiles { get => null; set { } }
    public override Vector3Int[] EvenWSTiles { get => null; set { } }

    //WN tiles
    public override Vector3Int[] OddWNTiles { get => null; set { } }
    public override Vector3Int[] EvenWNTiles { get => null; set { } }
    #endregion

    #region inherited methodes (rendues null)
    public sealed override bool OnCheckKapaPoints(Unit unit) => true;

    public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, Unit unit) { }

    public sealed override void InitPaterns(Vector3Int[] p) { }

    public sealed override List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, Unit unit) => null;

    public sealed override List<Vector3Int> OnSelectGraphTiles(Unit unit, HexGridStore hexGrid, Vector3Int[] tilesArray) => null;
    #endregion
}
