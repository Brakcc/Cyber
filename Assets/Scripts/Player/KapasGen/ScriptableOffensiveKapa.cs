using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Offensive Kapa", menuName = "Tactical/Offensive Kapa")]
public class ScriptableOffensiveKapa : ScriptableDataKapa
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
    #endregion

    #region fields
    public int damage;
    public List<Vector3Int> evenAffectedTiles;
    public List<Vector3Int> oddAffectedTiles;
    public int duration;
    public Animation animation;
    #endregion
}
