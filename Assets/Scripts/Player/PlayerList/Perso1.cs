using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Perso1 : Unit
{
    #region inherited accessors
    //moves fields 
    [SerializeField] private AUnitSO m_Unit;
    public override AUnitSO UnitData { get => m_Unit; set { m_Unit = value; } }

    //Game Loop Logic BALEK LA VISIBILITE et BALEK LE SCRIPTABLE
    public override bool CanPlay { get; set; }
    public override bool IsDead { get; set; }
    public override bool IsPersoLocked { get; set; }
    public override Vector3Int CurrentHexPos { get => currentHexPos; set { currentHexPos = HexCoordonnees.GetClosestHex(value); } }
    private Vector3Int currentHexPos;
    #endregion

    #region other fields
    //graph fields
    private SpriteRenderer rend;
    private Color originColor;
    #endregion

    #region methodes
    void Awake() 
    {
        rend = GetComponent<SpriteRenderer>();
        InitPlayer();
        currentHexPos = HexCoordonnees.GetClosestHex(transform.position);
    }

    void InitPlayer()
    {
        originColor = rend.color;
        CanPlay = true;
        IsDead = false;
        IsPersoLocked = false;
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