using UnityEngine;

[SelectionBase]
public class DPSUnit : Unit
{
    #region inherited accessors
    //moves fields 
    [SerializeField] AbstractUnitSO m_Unit;
    public override AbstractUnitSO UnitData => m_Unit;

    [SerializeField] PlayerStatsUI m_StatsUI;
    public override PlayerStatsUI StatUI => m_StatsUI;

    //Game Loop Logic BALEK LA VISIBILITE et BALEK LE SCRIPTABLE
    #region current stats
    public override float CurrentHealth { get; set; }
    public override int CurrentMP { get; set; }
    public override int CurrentAtk { get; protected set; }
    public override int CurrentDef { get; protected set; }
    public override int CurrentCritRate { get; set; }
    //additional precision stat
    public override int CurrentPrecision { get; set; }
    #endregion
    public override int TeamNumber { get; set; }
    public override bool IsOnTurret { get; protected set; }
    public override int UltPoints { get; set; }
    public override bool CanPlay { get; set; }
    public override bool IsDead { get; protected set; }
    public override bool IsPersoLocked { get; set; }
    public override bool CanKapa { get; protected set; }
    public override bool IsOnComputer { get; protected set; }

    [SerializeField] GraphInitUnit graphs;
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
        //Pour que cette ligne fonctionne, il ne faut qu'aucun autre renderer ne soit sur l'objet
        //Donc suprimer celui qui est de base sur le Bob
        rend = GetComponentInChildren<SpriteRenderer>();
        originColor = rend.color;
        OnInit();
    }

    void Start()
    {
        foreach (var kap in m_Unit.KapasList)
        {
            kap.InitPatterns(kap.Patterns);
        }
    }
    #endregion

    #region inherited methodes
    public override void Select()
    {
        base.Select();
        rend.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
    }
    public override void Deselect()
    {
        base.Deselect();
        rend.color = originColor;
    }
    public override void OnKapa() => Debug.Log("Omegalul");
    #endregion
}