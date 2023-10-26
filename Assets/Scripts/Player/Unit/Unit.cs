using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IUnit
{
    #region fields/accessors to herit
    public abstract int MovePoints { get; }
    public abstract int Speed { get; }
    public abstract int HealthPoint { get; set; }
    public abstract bool IsDead { get; set; }
    public abstract bool CanPlay { get; set; }
    public abstract bool IsPersoLocked { get; set; }
    public abstract Vector3Int CurrentHexPos { get; set; }
    public abstract List<AKapaSO> KapasList { get; set; }
    #endregion

    #region methodes to herit
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
            while (Vector2.Distance(transform.position, i) >= 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, i, pas);
                transform.position = new Vector3(transform.position.x, transform.position.y, z);
                yield return null;
            }
            
            PositionCharacterOnTile(i);
        }
    }
    protected void PositionCharacterOnTile(Vector3 pos) => transform.position = new Vector3(pos.x, pos.y, pos.z - 0.1f);
    #endregion
}
