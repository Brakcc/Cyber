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

    #region inherited paterns
    //North tiles
    public override Vector2Int[] OddNTiles { get; set; }
    public override Vector2Int[] EvenNTiles { get; set; }
    //EN tiles
    public override Vector2Int[] OddENTiles { get; set; }
    public override Vector2Int[] EvenENTiles { get; set; }
    //ES tiles
    public override Vector2Int[] OddESTiles { get; set; }
    public override Vector2Int[] EvenESTiles { get; set; }
    //S tiles
    public override Vector2Int[] OddSTiles { get; set; }
    public override Vector2Int[] EvenSTiles { get; set; }
    //WS tiles
    public override Vector2Int[] OddWSTiles { get; set; }
    public override Vector2Int[] EvenWSTiles { get; set; }
    //WN tiles
    public override Vector2Int[] OddWNTiles { get; set; }
    public override Vector2Int[] EvenWNTiles { get; set; }
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
