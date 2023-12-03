using System.Collections.Generic;
using UnityEngine;

public class Computer : Entity
{
    public override Vector3Int CurrentHexPos { get; set; }
    public override bool IsNetworkEmiter { get; set; }
    public override bool IsOnNetwork { get; set; }
    public override int NetworkRange { get; set; }
    public override List<Vector3Int> GlobalNetwork { get => null; set { } }

    public bool GotHacked { get; set; }

    [SerializeField] GraphInitBoard initBoard;

    void OnEnable()
    {
        OnInit();
    }

    protected override sealed void OnInit()
    {
        base.OnInit();
        IsNetworkEmiter = false;
        IsOnNetwork = false;
        NetworkRange = 0;

        GotHacked = false;

        initBoard.SetRenderer(gameObject);
    }

    public override sealed void OnGenerateNet() { }
}