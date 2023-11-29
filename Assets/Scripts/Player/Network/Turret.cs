using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : Entity
{
    #region fields
    public override Vector3Int CurrentHexPos { get; set; }
    public override bool IsNetworkEmiter { get; set; }
    public override bool IsOnNetwork { get; set; }
    public override int NetworkRange { get; set; }
    public override List<Vector3Int> GlobalNetwork { get; set; } = new();
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
        IsOnNetwork = true;
        NetworkRange = 2;
        HexGridStore.hGS.OnAddEmiter(this);
        OnGenerateNet();
    }

    public override void OnGenerateNet()
    {
        if (IsIntersecting(CurrentHexPos, HexGridStore.hGS, NetworkRange, out List<Network> net))
        {
            GlobalNetwork = OnIntersect(CurrentHexPos, HexGridStore.hGS, NetworkRange, net);
        }

        if (net == null) return;

        foreach (var i in net)
        {
            HexGridStore.hGS.NetworkList[(int)i] = GlobalNetwork;
        }
    }

    protected sealed override List<Vector3Int> OnIntersect(Vector3Int pos, HexGridStore hexGrid, int range, List<Network> toMerge)
    {
        if (toMerge.Count == 0) { return null; }

        List<Vector3Int> newRange = GetRangeList(pos, hexGrid, range).ToList();
        foreach (var i in toMerge)
        {
            foreach (var j in hexGrid.NetworkList[(int)i])
            {
                if (!newRange.Contains(j)) { newRange.Add(j); }
            }
        }
        foreach (var l in newRange) { hexGrid.GetTile(l).EnableGlowBaseNet(); }

        return newRange;
    }
    #endregion
}