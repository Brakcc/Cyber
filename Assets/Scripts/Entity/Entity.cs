using UnityEngine;

public abstract class Entity : MonoBehaviour, IEntity
{
    #region fields
    public abstract Vector3Int CurrentHexPos { get; set; }
    public abstract bool IsNetworkEmiter { get; set; }
    public abstract int NetworkRange { get; set; }
    #endregion

    #region methodes
    protected virtual void OnInit() => CurrentHexPos = HexCoordonnees.GetClosestHex(transform.position);
    #endregion
}