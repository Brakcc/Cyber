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
        Vector3 firstTarget = pathPos.Dequeue();
        StartCoroutine(MoveRoutine(firstTarget));
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
    #endregion
}