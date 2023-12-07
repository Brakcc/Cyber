using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[CreateAssetMenu(fileName = "Normal Attack Kapa", menuName = "Tactical/Kapas/Normal Attack")]
public class NAKapaSO : AKapaSO
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
    public override Vector3Int[] Patern => patern;
    [SerializeField] Vector3Int[] patern;

    [SerializeField] NAKapaSupFields nAKapaSupFields;

    [SerializeField] CameraManager cam;
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
    public class NAKapaSupFields
    {
        public int compPointsAdded;
        public int ultPointsAdded;
        public int damage;
        public int duration;
        public Animation animation;
    }
    #endregion

    #region inherited methodes
    public sealed override bool OnCheckKapaPoints(Unit unit) => true;

    public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, Unit unit)
    {
        base.OnExecute(hexGrid, pattern, unit, out bool isHitting);
        DoKapa(unit, isHitting);
        //PlaceHolder à remplir avec les anims et considération de dégâts
        EndKapa();
    }
    #endregion

    #region cache
    void DoKapa(Unit unit, bool hit)
    {
        if (hit) { GameLoopManager.gLM.HandleCompPointValueChange(unit.TeamNumber, nAKapaSupFields.compPointsAdded); }
        unit.UltPoints += nAKapaSupFields.ultPointsAdded;
        CameraFunctions.OnShake(FindObjectOfType<CinemachineVirtualCamera>(), cam.shake);
        //PlaceHolder à rempir avec les anims et considérations de dégâts

        unit.StatUI.SetUP(unit);
    }
    void EndKapa()
    {
        //Debug.Log("End Kapa");
    }
    #endregion
}
