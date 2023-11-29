using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : Entity
{
    #region fields
    public override Vector3Int CurrentHexPos { get; set; }
    public override bool IsNetworkEmiter { get; set; }
    public override int NetworkRange { get; set; }
    public override List<Vector3Int> LocalNetwork { get; set; } = new();
    #endregion

    #region methodes
    void OnEnable()
    {
        OnInit();
    }

    protected override void OnInit()
    {
        base.OnInit();
        IsNetworkEmiter = true;
        HexGridStore.hGS.OnAddEmiter(this);
        LocalNetwork = GetRangeList(CurrentHexPos, HexGridStore.hGS, NetworkRange).ToList();
    }
    #endregion
}