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
    public override Vector3Int[] OddNTiles { get => oddNTiles; set { oddNTiles = value; } }
    public Vector3Int[] oddNTiles;
    public override Vector3Int[] EvenNTiles { get => evenNTiles; set { evenNTiles = value; } }
    [HideInInspector] public Vector3Int[] evenNTiles;

    //EN tiles
    public override Vector3Int[] OddENTiles { get => oddENTiles; set { oddENTiles = value; } }
    [HideInInspector] public Vector3Int[] oddENTiles;
    public override Vector3Int[] EvenENTiles { get => evenENTiles; set { evenENTiles = value; } }
    [HideInInspector] public Vector3Int[] evenENTiles;

    //ES tiles
    public override Vector3Int[] OddESTiles { get => oddESTiles; set { oddESTiles = value; } }
    [HideInInspector] public Vector3Int[] oddESTiles;
    public override Vector3Int[] EvenESTiles { get => evenESTiles; set { evenESTiles = value; } }
    [HideInInspector] public Vector3Int[] evenESTiles;

    //S tiles
    public override Vector3Int[] OddSTiles { get => oddSTiles; set { oddSTiles = value; } }
    [HideInInspector] public Vector3Int[] oddSTiles;
    public override Vector3Int[] EvenSTiles { get => evenSTiles; set { evenSTiles = value; } }
    [HideInInspector] public Vector3Int[] evenSTiles;

    //WS tiles
    public override Vector3Int[] OddWSTiles { get => oddWSTiles; set { oddWSTiles = value; } }
    [HideInInspector] public Vector3Int[] oddWSTiles;
    public override Vector3Int[] EvenWSTiles { get => evenWSTiles; set { evenWSTiles = value; } }
    [HideInInspector] public Vector3Int[] evenWSTiles;

    //WN tiles
    public override Vector3Int[] OddWNTiles { get => oddWNTiles; set { oddWNTiles = value; } }
    [HideInInspector] public Vector3Int[] oddWNTiles;
    public override Vector3Int[] EvenWNTiles { get => evenWNTiles; set { evenWNTiles = value; } }
    [HideInInspector] public Vector3Int[] evenWNTiles;
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
        Debug.Log("pas utile de fou");
    }
    #endregion
}
