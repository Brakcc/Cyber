using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Perso1 : Unit
{
    #region fields
    //moves fields
    [SerializeField] private int movePoints = 3;
    public override int MovePoints { get { return movePoints; } }
    [SerializeField] private int speed;
    public override int Speed { get { return speed; } }

    //graph fields
    private SelectGlow glow;
    #endregion

    #region methodes
    void Awake() => glow = GetComponent<SelectGlow>();
    #endregion

    #region inherited methodes
    public override void Deselect() => glow.ToggleGLow(false);
    public override void Select() => glow.ToggleGLow(true);

    public override void MoveOnPath(List<Vector3> currentPath) => StartCoroutine(FollowPath(currentPath, Speed));
    #endregion
}