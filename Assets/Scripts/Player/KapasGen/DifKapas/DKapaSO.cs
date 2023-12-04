using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeAct Kapa", menuName = "Tactical/Kapas/DeAct")]
public class DKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName { get => kapaName; }
    [SerializeField] private string kapaName;
    public override int ID { get => id; }
    [SerializeField] private int id;
    public override string Description { get => description; }
    [SerializeField] private string description;
    public override int Cost { get => 0; }
    public override int MaxPlayerPierce => 0;
    public override EffectType EffectType { get => EffectType.None; }
    public override KapaType KapaType { get => kapaType; }
    [SerializeField] private KapaType kapaType;
    public override KapaFunctionType KapaFunctionType { get => KapaFunctionType.Default; }
    public override KapaUISO KapaUI { get => kapaUI; }
    [SerializeField] private KapaUISO kapaUI;
    public override Vector3Int[] Patern { get => null; }
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
    public sealed override bool OnCheckKapaPoints(Unit unit)
    {
        return (HexGridStore.hGS.GetTile(unit.CurrentHexPos).IsComputer() && !HexGridStore.hGS.computerList[(int)HexGridStore.hGS.GetTile(unit.CurrentHexPos).ComputerTarget].GotHacked && unit.TeamNumber == 1);
    }

    public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, Unit unit)
    {
        GameLoopManager.gLM.HandleComputerValueChange();
        hexGrid.HandlePCHacked(hexGrid.GetTile(unit.CurrentHexPos).ComputerTarget);
    }
    
    public sealed override void InitPaterns(Vector3Int[] p) { }

    public sealed override List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, Unit unit) => null;

    public sealed override List<Vector3Int> OnSelectGraphTiles(Unit unit, HexGridStore hexGrid, Vector3Int[] tilesArray) => null;
    #endregion
}