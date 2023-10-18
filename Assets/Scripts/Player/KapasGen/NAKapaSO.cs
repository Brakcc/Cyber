using UnityEngine;

[CreateAssetMenu(fileName = "Normal Attack Kapa", menuName = "Tactical/Normal Attack Kapa")]
public class NAKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName { get => kapaName; }
    public string kapaName;
    public override  int ID { get => id; }
    public int id;
    public override string Description { get => description; }
    public string description;
    public override int Cost { get => cost; }
    public int cost;
    public override EffectType EffectType { get => effectType; }
    public EffectType effectType;
    public override KapaType KapaType { get => kapaType; }
    public KapaType kapaType;
    public override KapaUISO KapaUI { get => kapaUI; }
    public KapaUISO kapaUI;
    public override Vector3Int[] Patern { get => patern; }
    public Vector3Int[] patern;
    #endregion

    #region inherited paterns/accessors
    //North tiles
    public override Vector2Int[] OddNTiles { get => oddNTiles; set { oddNTiles = value; } }
    [HideInInspector] public Vector2Int[] oddNTiles;
    public override Vector2Int[] EvenNTiles { get => evenNTiles; set { evenNTiles = value; } }
    [HideInInspector] public Vector2Int[] evenNTiles;

    //EN tiles
    public override Vector2Int[] OddENTiles { get => oddENTiles; set { oddENTiles = value; } }
    [HideInInspector] public Vector2Int[] oddENTiles;
    public override Vector2Int[] EvenENTiles { get => evenENTiles; set { evenENTiles = value; } }
    public Vector2Int[] evenENTiles;

    //ES tiles
    public override Vector2Int[] OddESTiles { get => oddESTiles; set { oddESTiles = value; } }
    [HideInInspector] public Vector2Int[] oddESTiles;
    public override Vector2Int[] EvenESTiles { get => evenESTiles; set { evenESTiles = value; } }
    [HideInInspector] public Vector2Int[] evenESTiles;

    //S tiles
    public override Vector2Int[] OddSTiles { get => oddSTiles; set { oddSTiles = value; } }
    [HideInInspector] public Vector2Int[] oddSTiles;
    public override Vector2Int[] EvenSTiles { get => evenSTiles; set { evenSTiles = value; } }
    [HideInInspector] public Vector2Int[] evenSTiles;

    //WS tiles
    public override Vector2Int[] OddWSTiles { get => oddWSTiles; set { oddWSTiles = value; } }
    [HideInInspector] public Vector2Int[] oddWSTiles;
    public override Vector2Int[] EvenWSTiles { get => evenWSTiles; set { evenWSTiles = value; } }
    [HideInInspector] public Vector2Int[] evenWSTiles;

    //WN tiles
    public override Vector2Int[] OddWNTiles { get => oddWNTiles; set { oddWNTiles = value; } }
    [HideInInspector] public Vector2Int[] oddWNTiles;
    public override Vector2Int[] EvenWNTiles { get => evenWNTiles; set { evenWNTiles = value; } }
    [HideInInspector] public Vector2Int[] evenWNTiles;
    #endregion

    #region fields
    public int damage;
    public int duration;
    public Animation animation;
    #endregion

    #region inherited methodes
    public override void Execute()
    {
        DoKapa();
        Debug.Log(Description); //PlaceHolder à remplir avec les anims et considération de dégâts
        EndKapa();
    }
    #endregion

    #region cache
    void DoKapa()
    {
        //PlaceHolder à rempir avec les anims et considérations de dégâts
        Debug.Log("Done");
    }
    void EndKapa()
    {
        DeselectTiles();
        UnitManager.unitManager.SelectedUnit.IsPersoLocked = false;
        UnitManager.unitManager.SelectedUnit.CanPlay = false;
        UnitManager.unitManager.SelectedUnit = null;
    }
    #endregion
}
