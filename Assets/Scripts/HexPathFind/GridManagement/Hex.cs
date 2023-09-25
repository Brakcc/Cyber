using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    #region fields
    [SerializeField] private SelectGlow glow;
    [SerializeField] private HexType type;

    public Vector3Int hexCoords;
    #endregion

    #region methodes
    void Awake()
    {
        hexCoords = new HexCoordonnees(gameObject).offsetCoordonnees;
        glow = GetComponent<SelectGlow>();
    }

    public int GetValue() => type switch
    {
        HexType.Default => int.MaxValue,
        HexType.Walkable => 1,
        HexType.Obstacle => int.MaxValue,
        _ => int.MaxValue
    };

    public bool IsObstacle() => type == HexType.Obstacle;

    public void EnableGlow() => glow.ToggleGLow(true);
    public void DisableGlow() => glow.ToggleGLow(false);

    public void EnableGlowPath() => glow.StartGlowPath();
    public void DisableGlowPath() => glow.ResetGlowPath();
    #endregion
}

public enum HexType
{
    Default,
    Walkable,
    Obstacle,
    Hole
}