using UnityEngine;

[SelectionBase]
public class DPSUnit : Unit
{
    #region inherited accessors
    //moves fields 
    [SerializeField] private AUnitSO m_Unit;
    public override AUnitSO UnitData { get => m_Unit; set { m_Unit = value; } }

    //Game Loop Logic BALEK LA VISIBILITE et BALEK LE SCRIPTABLE
    public override float Health { get; set; }
    public override bool IsOnTurret { get; set; }
    public override int CompPoints { get; set; }
    public override int UltPoints { get; set; }
    public override bool CanPlay { get; set; }
    public override bool IsDead { get; set; }
    public override bool IsPersoLocked { get; set; }
    public override Vector3Int CurrentHexPos { get => currentHexPos; set { currentHexPos = value; } }
    private Vector3Int currentHexPos;

    [SerializeField] private GraphInitUnit graphs;
    #endregion

    #region other fields
    //graph fields
    private SpriteRenderer rend;
    private Color originColor;
    #endregion

    #region methodes
    void Awake() 
    {
        graphs.SetRenderer(gameObject, m_Unit.Sprite);
        currentHexPos = HexCoordonnees.GetClosestHex(transform.position);
        //Pour que cette ligne fonctionne, il ne faut qu'aucun autre renderer ne soit sur l'objet
        //Donc suprimer celui qui est de base sur le Bob
        rend = GetComponentInChildren<SpriteRenderer>();
        originColor = rend.color;
        OnInit();
    }

    void Start()
    {
        foreach (var kap in m_Unit.KapasList) { kap.InitPaterns(kap.Patern); }
    }
    #endregion

    #region inherited methodes
    public override void Select() => rend.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
    public override void Deselect() => rend.color = originColor;
    public override void OnKapa() => Debug.Log("Omegalul");
    #endregion
}