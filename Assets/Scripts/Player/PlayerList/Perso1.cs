using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Perso1 : Unit
{
    #region inherited accessors
    //moves fields
    [SerializeField] private int movePoints = 3;
    public override int MovePoints { get => movePoints; }

    [SerializeField] private int speed = 15;
    public override int Speed { get => speed; }    //health
    [SerializeField] private int healthPoint = 50;
    public override int HealthPoint { get => healthPoint; 
                                      set { healthPoint = value; } }

    //Game Loop Logic
    public override bool CanPlay { get; set; }
    public override bool IsDead { get; set; }
    public override bool IsPersoLocked { get; set; }

    //kapas
    [SerializeField] private List<AKapaSO> kapasList = new List<AKapaSO>();
    public override List<AKapaSO> KapasList { get => kapasList; 
                                           set { kapasList = value; } }
    #endregion

    #region other fields
    //graph fields
    private SelectGlow glow;
    #endregion

    #region methodes
    void Awake() 
    {
        glow = GetComponent<SelectGlow>();
        InitPlayer();
    }

    void InitPlayer()
    {
        CanPlay = true;
        IsDead = false;
        IsPersoLocked = false;
    }
    #endregion

    #region inherited methodes
    public override void Deselect() => glow.ToggleGLow(false);
    public override void Select() => glow.ToggleGLow(true);
    public override void OnKapa() => Debug.Log("Omegalul");
    #endregion
}