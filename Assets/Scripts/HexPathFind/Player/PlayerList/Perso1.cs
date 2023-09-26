using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Perso1 : Unit
{
    #region fields
    //moves values
    [SerializeField] private int movePoints = 3;
    public override int MovePoints { get { return movePoints; } }
    [SerializeField] private float moveDuration = 1;
    [SerializeField] private float speed;

    //graph values
    private SelectGlow glow;
    private Queue<Vector3> pathPos = new Queue<Vector3>();

    public event Action<Unit> moveFinished;
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
        pathPos = new Queue<Vector3>(currentPath);
        //Vector3 firstTarget = pathPos.Dequeue();
        //StartCoroutine(MoveRoutine(firstTarget));
        FollowPath(currentPath);
    }
    #endregion

    #region new methodes
    IEnumerator MoveRoutine(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
        endPos.y = startPos.y;
        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.fixedDeltaTime;
            float lerp = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(startPos, endPos, lerp);
            yield return null;
        }
        transform.position = endPos;

        if (pathPos.Count <= 0)
        {
            moveFinished?.Invoke(this);
        }
    }

    public void FollowPath(List<Vector3> path)
    {
        var pas = speed * Time.fixedDeltaTime;

        var z = path[0].z;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(path[0].x, path[0].y), pas);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);

        if (Vector2.Distance(transform.position, new Vector2(path[0].x, path[0].y)) < 0.0001f)
        {
            PositionCharacterOnTile(path[0]);
            path.RemoveAt(0);
        }
    }
    void PositionCharacterOnTile(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y + 0.001f, pos.z);
    }
    #endregion
}