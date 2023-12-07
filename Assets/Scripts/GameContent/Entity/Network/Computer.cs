﻿using System.Collections.Generic;
using UnityEngine;

public class Computer : Entity
{
    public override Vector3Int CurrentHexPos { get; set; }
    public override bool IsNetworkEmiter { get; set; }
    public override bool IsOnNetwork { get; set; }
    public override int NetworkRange { get; set; }
    public override List<Vector3Int> GlobalNetwork { get => null; set { } }

    [SerializeField] ComputerTarget compTarget;
    public ComputerTarget ComputerTarget { get => compTarget; }
    public bool GotHacked { get; set; }

    [SerializeField] GraphInitBoard initBoard;

    void Awake()
    {
        OnInit();
    }

    protected sealed override void OnInit()
    {
        base.OnInit();
        IsNetworkEmiter = false;
        IsOnNetwork = false;
        NetworkRange = 0;

        GotHacked = false;

        initBoard.SetRenderer(gameObject);
    }

    public sealed override void OnGenerateNet() { }

    public void HandleComputerHack()
    {
        initBoard.HandleDeAct(gameObject, false);
    }
}