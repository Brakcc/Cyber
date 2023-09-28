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
    //[SerializeField] private float moveDuration = 1;
    //[SerializeField] private float rotaDur = 1;
    [SerializeField] private float speed;

    //graph values
    private SelectGlow glow;
    //private Queue<Vector3> pathPos = new Queue<Vector3>();
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
        //pathPos = new Queue<Vector3>(currentPath);
        //Vector3 firstTarget = pathPos.Dequeue();
        //StartCoroutine(MoveRoutine(firstTarget));
        FollowPath(currentPath);
    }
    #endregion

    #region new methodes
    /*IEnumerator MoveRoutine(Vector3 endPos)
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

        if (pathPos.Count > 0) { StartCoroutine(RotaRoutine(pathPos.Dequeue(), rotaDur)); }

        if (pathPos.Count <= 0) { moveFinished?.Invoke(this); }
    }

    IEnumerator RotaRoutine(Vector3 endPos, float rotaDur)
    {
        Quaternion startRota = transform.rotation;
        endPos.y = transform.position.y;
        Vector3 dir = endPos - transform.position;
        Quaternion endRota = Quaternion.LookRotation(dir, Vector3.up);

        if (!Mathf.Approximately(Mathf.Abs(Quaternion.Dot(startRota, endRota)), 1))
        {
            float elapsedTime = 0;
            while (elapsedTime < rotaDur)
            {
                elapsedTime += Time.fixedDeltaTime;
                float lerp = elapsedTime/rotaDur;
                transform.rotation = Quaternion.Lerp(startRota, endRota, lerp);
                yield return null;
            }
            transform.rotation = endRota;
        }
        StartCoroutine(MoveRoutine(endPos));
    }*/


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