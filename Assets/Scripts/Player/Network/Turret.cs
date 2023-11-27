using UnityEngine;

public class Turret : Entity
{
    #region fields
    public override Vector3Int CurrentHexPos { get; set; }
    public override bool IsNetworkEmiter { get; set; }
    public override int NetworkRange { get; set; }
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
    }
    #endregion
}