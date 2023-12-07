using System.Collections.Generic;
using UnityEngine;
using CustomAttributes;

[CreateAssetMenu(fileName = "Competence Kapa", menuName = "Tactical/Kapas/Competence")]
public class CKapaSO : AKapaSO
{
    #region inherited accessors
    public override string KapaName => _kapaName;
    [SerializeField] string _kapaName;
    public override  int ID => _id;
    [SerializeField] int _id;
    public override string Description => _description;
    [SerializeField] string _description;
    public override int Cost => _cost;
    [SerializeField] int _cost;
    public override int MaxPlayerPierce => _maxPlayerPierce;
    [SerializeField] int _maxPlayerPierce;
    public override float BalanceCoeff => _balanceCoeff;
    [SerializeField] float _balanceCoeff;
    public override EffectType EffectType => _effectType;
    [SerializeField] EffectType _effectType;
    public override KapaType KapaType => _kapaType;
    [SerializeField] KapaType _kapaType;
    public override KapaFunctionType KapaFunctionType => _kapaFunctionType;
    [SerializeField] KapaFunctionType _kapaFunctionType;

    [ShowIfTrue("_kapaFunctionType", (int)KapaFunctionType.Grab)]
    [SerializeField] KapaGrab grab;

    [ShowIfTrue("_kapaFunctionType", (int)KapaFunctionType.Dash)]
    [SerializeField] KapaDash dash;

    public override KapaUISO KapaUI => _kapaUI;
    [SerializeField] KapaUISO _kapaUI;

    public override GameObject DamageFeedBack => _damageFeedBack;
    [SerializeField] GameObject _damageFeedBack;

    public override Vector3Int[] Pattern => _pattern;
    [SerializeField] Vector3Int[] _pattern;

    [SerializeField] CKapaSupFields _cKapaSupFields;

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
    public sealed override bool OnCheckKapaPoints(IUnit unit)
    {
        if (GameLoopManager.gLM.CompPoints[unit.TeamNumber] >= _cKapaSupFields.neededCompPoints) return true;
        
        RefuseKapa(); return false;
    }
    public sealed override List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IUnit unit)
    {
        if (EffectType != EffectType.Hacked)
        {
            return base.OnGenerateButton(hexGrid, unit);
        }
        unit.OnSelectNetworkTiles();
        
        return null;
    }

    public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
    {
        if (EffectType == EffectType.Hacked)
        {
            base.OnExecute(hexGrid, unit.GlobalNetwork, unit);
            unit.OnDeselectNetworkTiles();
        }
        else
        {
            base.OnExecute(hexGrid, pattern, unit);
        }

        DoKapa(unit);
        //Debug.Log(Description); //PlaceHolder � remplir avec les anims et consid�ration de d�g�ts
        EndKapa();
    }
    #endregion

    #region cache
    void DoKapa(IUnit unit)
    {
        GameLoopManager.gLM.HandleCompPointValueChange(unit.TeamNumber, -_cKapaSupFields.neededCompPoints);
        unit.UltPoints += _cKapaSupFields.ultPointsAdded;
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