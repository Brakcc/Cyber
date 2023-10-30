using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skip Kapa", menuName = "Tactical/Kapas/Skip")]
public class SKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName { get => kapaName; }
    [SerializeField] private string kapaName;
    public override int ID { get => id; }
    [SerializeField] private int id;
    public override string Description { get => description; }
    [SerializeField] private string description;
    public override int Cost { get => cost; }
    [SerializeField] private int cost;
    public override EffectType EffectType { get => effectType; }
    [SerializeField] private EffectType effectType;
    public override KapaType KapaType { get => kapaType; }
    [SerializeField] private KapaType kapaType;
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
    public override void Execute() { }

    public override void InitPaterns(Vector3Int[] p) { }

    public override List<Vector3Int> GenerateButton(HexGridStore hexGrid, Unit unit) => null;

    public override List<Vector3Int> SelectGraphTiles(Unit unit, HexGridStore hexGrid, Vector3Int[] tilesArray) => null;
    #endregion
}
