using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Offensive Skill", menuName = "Tactical/Offensive Skill")]
public class ScriptableOffensiveSkill : ScriptableSkill
{
    public override string SkillName { get => skillName; }
    public string skillName;
    public override  int ID { get => id; }
    public int id;
    public override string Description { get => description; }
    public string description;
    public override int Cost { get => cost; }
    public int cost;
    public override SkillEffectType EffectType { get => effectType; }
    public SkillEffectType effectType;

    public int damage;
    public List<Vector3Int> evenAffectedTiles;
    public List<Vector3Int> oddAffectedTiles;
    public int duration;
    public Animation animation;
}
