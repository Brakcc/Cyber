using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ultimate Kapa", menuName = "Tactical/Kapas/Ultimate")]
public class UKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName => kapaName;
    [SerializeField] string kapaName;
    public override  int ID => id;
    [SerializeField] int id;
    public override string Description => description;
    [SerializeField] string description;
    public override int Cost => cost;
    [SerializeField] int cost;
    public override int MaxPlayerPierce => maxPlayerPierce;
    [SerializeField] int maxPlayerPierce;
    public override float BalanceCoeff => balanceCoeff;
    [SerializeField] float balanceCoeff;
    public override EffectType EffectType => effectType;
    [SerializeField] EffectType effectType;
    public override KapaType KapaType => kapaType;
    [SerializeField] KapaType kapaType;
    public override KapaFunctionType KapaFunctionType => kapaFunctionType;
    [SerializeField] KapaFunctionType kapaFunctionType;
    public override KapaUISO KapaUI => kapaUI;
    [SerializeField] KapaUISO kapaUI;
    public override GameObject DamageFeedBack => damageFeedBack;
    [SerializeField] GameObject damageFeedBack;
    public override Vector3Int[] Pattern => pattern;
    [SerializeField] Vector3Int[] pattern;

    [SerializeField] UKapaSupFields uKapaSupFields;

    [SerializeField] CameraManager cam;
    #endregion

    #region inherited paterns/accessors
    //North tiles
    public override Vector3Int[] OddNTiles { get => _oddNTiles; protected set => _oddNTiles = value; }
    private Vector3Int[] _oddNTiles;
    public override Vector3Int[] EvenNTiles { get => _evenNTiles; protected set => _evenNTiles = value; }
    private Vector3Int[] _evenNTiles;

    //EN tiles
    public override Vector3Int[] OddENTiles { get => _oddENTiles; protected set => _oddENTiles = value; }
    private Vector3Int[] _oddENTiles;
    public override Vector3Int[] EvenENTiles { get => _evenENTiles; protected set => _evenENTiles = value; }
    private Vector3Int[] _evenENTiles;

    //ES tiles
    public override Vector3Int[] OddESTiles { get => _oddESTiles; protected set => _oddESTiles = value; }
    private Vector3Int[] _oddESTiles;
    public override Vector3Int[] EvenESTiles { get => _evenESTiles; protected set => _evenESTiles = value; }
    private Vector3Int[] _evenESTiles;

    //S tiles
    public override Vector3Int[] OddSTiles { get => _oddSTiles; protected set => _oddSTiles = value; }
    private Vector3Int[] _oddSTiles;
    public override Vector3Int[] EvenSTiles { get => _evenSTiles; protected set => _evenSTiles = value; }
    private Vector3Int[] _evenSTiles;

    //WS tiles
    public override Vector3Int[] OddWSTiles { get => _oddWSTiles; protected set => _oddWSTiles = value; }
    private Vector3Int[] _oddWSTiles;
    public override Vector3Int[] EvenWSTiles { get => _evenWSTiles; protected set => _evenWSTiles = value; }
    private Vector3Int[] _evenWSTiles;

    //WN tiles
    public override Vector3Int[] OddWNTiles { get => _oddWNTiles; protected set => _oddWNTiles = value; }
    private Vector3Int[] _oddWNTiles;
    public override Vector3Int[] EvenWNTiles { get => _evenWNTiles; protected set => _evenWNTiles = value; }
    private Vector3Int[] _evenWNTiles;
    #endregion

    #region fields
    [System.Serializable]
    public class UKapaSupFields
    {
        public int neededUltPoints;
        public int damage;
        public int duration;
        public Animation animation;
    }
    #endregion

    #region inherited methodes
    public sealed override bool OnCheckKapaPoints(IUnit unit)
    {
        if (unit.UltPoints >= uKapaSupFields.neededUltPoints) return true;
        
        RefuseKapa(); return false;
    }

    public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
    {
        base.OnExecute(hexGrid, pattern, unit);
        DoKapa(unit);
       //Debug.Log(Description); //PlaceHolder � remplir avec les anims et consid�ration de d�g�ts
        EndKapa();
    }
    #endregion

    #region cache
    void DoKapa(IUnit unit)
    {
        unit.UltPoints -= uKapaSupFields.neededUltPoints;
        //PlaceHolder � rempir avec les anims et consid�rations de d�g�ts

        unit.StatUI.SetUP(unit);
    }
    void RefuseKapa() { Debug.Log("nope"); }
    void EndKapa()
    {
        //Debug.Log("End Kapa");
    }
    #endregion
}
