using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    #region 
    public HexType type;
    [SerializeField] SelectGlow glow;

    //la Data importante
    public Vector3Int HexCoords { get; set; }
    public bool HasPlayerOnIt { get; set; }
    public Unit PlayerRef { get; set; }
    #endregion

    #region methodes
    void Awake()
    {
        HexCoords = new HexCoordonnees(gameObject).OffsetCoordonnees;
        glow.SetHexaRefs();
    }

    /// <summary>
    /// Donne la valuer de la tile pour le A* BFS
    /// </summary>
    /// <returns></returns>
    public int GetValue() => type switch
    {
        HexType.Default => 1000,
        HexType.Walkable => 1,
        HexType.Obstacle => 1000,
        HexType.Hole => 1000,
        _ => 1000
    };

    public void SetUnit(Unit unit) => PlayerRef = unit;
    public Unit GetUnit() => PlayerRef;
    public void ClearUnit() => PlayerRef = null;

    public bool IsObstacle() => type == HexType.Obstacle || type == HexType.Hole;

    //Init graph a ajouter pour ajouter les textures en sqrt(2) a 45° pour le passage des Units devant ou derrière les props

    #region glow mats
    //General Glow pour la range
    public void EnableGlow() => glow.ToggleRangeGlow(true);
    public void DisableGlow() => glow.ToggleRangeGlow(false);

    //Glow pour le path
    public void EnableGlowPath() => glow.TogglePathGlow(true);
    public void DisableGlowPath() => glow.TogglePathGlow(false);

    //Glow pour les kapas
    public void EnableGlowKapa() => glow.ToggleKapaGlow(true);
    public void DisableGlowKapa() => glow.ToggleKapaGlow(false);

    //Glow pour les boutons de sens de kapas
    public void EnableGlowButton() => glow.ToggleButtonGlow(true);
    public void DisableGlowButton() => glow.ToggleButtonGlow(false);
    #endregion
    #endregion
}