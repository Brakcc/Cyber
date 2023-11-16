using UnityEngine;
using CustomAttributes;

[CreateAssetMenu(fileName = "Competence Kapa", menuName = "Tactical/Kapas/Competence")]
public class CKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName { get => kapaName; }
    [SerializeField] private string kapaName;
    public override  int ID { get => id; }
    [SerializeField] private int id;
    public override string Description { get => description; }
    [SerializeField] private string description;
    public override int Cost { get => cost; }
    [SerializeField] private int cost;
    public override int MaxPlayerPierce => maxPlayerPierce;
    [SerializeField] int maxPlayerPierce;
    public override EffectType EffectType { get => effectType; }
    [SerializeField] private EffectType effectType;
    public override KapaType KapaType { get => kapaType; }
    [SerializeField] private KapaType kapaType;
    public override KapaFunctionType KapaFunctionType { get => kapaFunctionType; }
    [SerializeField] private KapaFunctionType kapaFunctionType;

    [ShowIfTrue("kapaFunctionType", (int)KapaFunctionType.Grab)]
    [SerializeField] KapaGrab grab;

    [ShowIfTrue("kapaFunctionType", (int)KapaFunctionType.Dash)]
    [SerializeField] KapaDash dash;

    public override KapaUISO KapaUI { get => kapaUI; }
    [SerializeField] private KapaUISO kapaUI;
    public override Vector3Int[] Patern { get => patern; }
    [SerializeField] private Vector3Int[] patern;

    [SerializeField] CKapaSupFields cKapaSupFields;

    [SerializeField] private CameraManager cam;
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

    #region fields
    [System.Serializable]
    public class CKapaSupFields
    {
        public int neededCompPoints;
        public int ultPointsAdded;
        public int damage;
        public int duration;
        public Animation animation;
    }
    #endregion

    #region inherited methodes
    public override bool OnCheckKapaPoints(Unit unit)
    {
        if (unit.CompPoints < cKapaSupFields.neededCompPoints) { RefuseKapa(); return false; }
        return true;
    }

    public override void OnExecute(Unit unit)
    {
        DoKapa(unit);
        Debug.Log(Description); //PlaceHolder à remplir avec les anims et considération de dégâts
        EndKapa();
    }
    #endregion

    #region cache
    void DoKapa(Unit unit)
    {
        //AJOUTER LA LOGIQUE DE TEAM POUR BAISSER LES POINTS DE COMP COMMUNS
        unit.CompPoints -= cKapaSupFields.neededCompPoints;
        unit.UltPoints += cKapaSupFields.ultPointsAdded;
        //PlaceHolder à rempir avec les anims et considérations de dégâts
    }
    void RefuseKapa() { Debug.Log("nope"); }
    void EndKapa()
    {
        //Debug.Log("End Kapa");
    }
    #endregion
}