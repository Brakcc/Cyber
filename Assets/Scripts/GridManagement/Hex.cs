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
        HexType.Hole => int.MaxValue,
        _ => int.MaxValue
    };

    public bool IsObstacle() => type == HexType.Obstacle;

    //General Glow pour la range
    public void EnableGlow() => glow.ToggleGlow(true);
    public void DisableGlow() => glow.ToggleGlow(false);

    //Glow pour le path
    public void EnableGlowPath() => glow.StartGlowPath();
    public void DisableGlowPath() => glow.ResetGlowPath();

    //Glow pour les compétences
    public void EnableGlowKapa() => glow.ToggleGlowKapa(true);
    public void DisableGlowKapa() => glow.ToggleGlowKapa(false);
    #endregion
}

public enum HexType
{
    Default,
    Walkable,
    Obstacle,
    Hole
}