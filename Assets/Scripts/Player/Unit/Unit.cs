using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : Entity, IUnit
{
    #region fields/accessors to herit
    public abstract AUnitSO UnitData { get; set; }
    public abstract PlayerStatsUI StatUI { get; set; }
    #region current stats
    public abstract float CurrentHealth { get; set; }
    public abstract int CurrentMP { get; set; }
    public abstract int CurrentAtk { get; set; }
    public abstract int CurrentDef { get; set; }
    public abstract int CurrentCritRate { get; set; }
    #endregion
    public abstract int TeamNumber { get; set; }
    public abstract bool IsOnTurret { get; set; }
    public abstract int UltPoints { get; set; }
    public abstract bool IsDead { get; set; }
    public abstract bool CanPlay { get; set; }
    public abstract bool IsPersoLocked { get; set; }
    public abstract bool CanKapa { get; set; }

    #region Entity heritage
    public override Vector3Int CurrentHexPos { get => currentHexPos; set { currentHexPos = value; } }
    protected Vector3Int currentHexPos;
    public override bool IsNetworkEmiter { get; set; }
    public override int NetworkRange { get; set; }
    #endregion

    protected NetworkSystem netSys;
    #endregion

    #region methodes to herit
    protected override sealed void OnInit()
    {
        base.OnInit();

        CanPlay = false;
        IsDead = false;
        IsPersoLocked = false;
        CanKapa = true;
        IsOnTurret = false;
        if (UnitData.Type == UnitType.Hacker) { IsNetworkEmiter = true; NetworkRange = UnitData.NetworkRange; }
        else { IsNetworkEmiter = false; NetworkRange = 0; }

        //stats that can vary over the game
        CurrentHealth = UnitData.HealthPoint;
        CurrentAtk = UnitData.Attack;
        CurrentDef = UnitData.Defense;
        CurrentCritRate = UnitData.CritRate;
        CurrentMP = UnitData.MovePoints;

        netSys = new();
        //if (UnitData.Type == UnitType.Hacker) { ChangeNetwotrk(); }
    }

    public abstract void Select();
    public virtual void MoveOnPath(List<Vector3> currentPath) => StartCoroutine(FollowPath(currentPath,UnitData.Speed));
    public abstract void OnKapa();
    public abstract void Deselect();
    public virtual void OnDie()
    {
        CanPlay = false;
        IsDead = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
    public virtual void OnRez()
    {
        CanPlay = false;
        IsDead = false;
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }
    #endregion

    #region cache
    protected IEnumerator FollowPath(List<Vector3> path, float speed)
    {
        CanKapa = false;
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
        CanKapa = true;
        if (UnitData.Type == UnitType.Hacker) { ChangeNetwotrk(); }
    }
    protected void PositionCharacterOnTile(Vector3 pos) => transform.position = new Vector3(pos.x, pos.y, pos.z - 0.1f);

    protected void ChangeNetwotrk()
    {
        foreach (var i in HexGridStore.hGS.hexTiles.Values) { i.ClearLMixedNetwork(); }
        netSys.OnActivateNetwork(this, HexGridStore.hGS);
    }
    #endregion
}
