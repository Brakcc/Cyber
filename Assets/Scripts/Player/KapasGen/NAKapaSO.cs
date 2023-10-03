using System.Collections.Generic;
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
    public override DamageEffectType EffectType { get => effectType; }
    public DamageEffectType effectType;
    public override KapaType KapaType { get => kapaType; }
    public KapaType kapaType;
    public override KapaUISO KapaUI { get => kapaUI; }
    public KapaUISO kapaUI;
    #endregion

    #region fields
    public int damage;
    public List<Vector3Int> evenAffectedTiles;
    public List<Vector3Int> oddAffectedTiles;
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
