using UnityEngine;

public interface IEntity
{
    Vector3Int CurrentHexPos { get; }
    bool IsNetworkEmiter { get; }
    int NetworkRange { get; }

    void OnGenerateNet();
}
