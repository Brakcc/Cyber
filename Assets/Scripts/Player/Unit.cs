using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    #region fields a heriter
    public abstract int MovePoints { get; }
    public abstract int Speed { get; }
    public abstract int HealthPoint { get; set; }
    public abstract bool IsDead { get; set; }
    public abstract bool CanPlay { get; set; }
    public abstract bool IsPersoLocked { get; set; }
    #endregion

    #region methodes a heriter
    public abstract void Select();
    public virtual void MoveOnPath(List<Vector3> currentPath) => StartCoroutine(FollowPath(currentPath, Speed));
    public abstract void OnKapa();
    public abstract void Deselect();
    #endregion

    #region cache
    public IEnumerator FollowPath(List<Vector3> path, float speed)
    {
        float pas = speed * Time.fixedDeltaTime / 10;
        foreach (var i in path)
        {
            float z = path[0].z;
            while (Vector2.Distance(transform.position, new Vector2(i.x, i.y)) >= 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(i.x, i.y), pas);
                transform.position = new Vector3(transform.position.x, transform.position.y, z);
                yield return null;
            }
            
            PositionCharacterOnTile(i);
        }
    }
    void PositionCharacterOnTile(Vector3 pos) => transform.position = new Vector3(pos.x, pos.y + 0.0001f, pos.z - 0.1f);
    #endregion
}
