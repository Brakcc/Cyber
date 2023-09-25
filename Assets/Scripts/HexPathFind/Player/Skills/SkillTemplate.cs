using UnityEngine;

public abstract class SkillTemplate : MonoBehaviour, ISkills
{
    public int i = 1;

    public abstract void SelectTiles();

    public abstract void Execute();

    public abstract void DeselectTiles();
}
