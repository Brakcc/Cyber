using System.Collections.Generic;
using UnityEngine;

public interface ISkillsDatas
{
    public int damage { get; set; }
    public List<Vector3Int> affectedTiles { get; set; }
    //public int duration { get; set; }
    public SkillEffectType effectType { get; set; }
}

public enum SkillEffectType
{
    SingleDamage,
    DOT,
    hacked,
    buff
}
