using System;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Perso1 : Unit
{
    #region fields
    //moves values
    [SerializeField] private int movePoints = 3;
    public override int MovePoints { get { return movePoints; } }
    [SerializeField] private float speed;

    //graph values
    private SelectGlow glow;
    #endregion

    #region inherited methodes
    void Awake()
    {
        glow = GetComponent<SelectGlow>();
    }

    public override void Deselect()
    {
        glow.ToggleGLow(false);
    }
    public override void Select()
    {
        glow.ToggleGLow(true);
    }

    public override void MoveOnPath(List<Vector3> currentPath)
    {
        currentPath.Reverse();
        FollowPath(currentPath);
    }
    #endregion

    #region new methodes
    public void FollowPath(List<Vector3> path)
    {
        Debug.Log(path[path.Count - 1]);
        float pas = speed * Time.fixedDeltaTime;
        foreach (var i in path)
        {
            float z = path[0].z;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(i.x, i.y), pas);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);

            if (Vector2.Distance(transform.position, new Vector2(i.x, i.y)) < 0.001f)
            {
                PositionCharacterOnTile(path[0]);
            }
        }
    }
    void PositionCharacterOnTile(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y + 0.001f, pos.z);
    }
    #endregion
}