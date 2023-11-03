using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    #region 
    [SerializeField] private HexType type;
    [SerializeField] private SelectGlow glow;

    //la Data importante
    public Vector3Int HexCoords { get; set; }
    public Vector3Int hexCoords;
    public bool HasPlayerOnIt { get; set; }
    #endregion

    #region methodes
    void Awake()
    {
        HexCoords = new HexCoordonnees(gameObject).OffsetCoordonnees;
        glow.SetGlow(this);
        hexCoords = HexCoords;
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

    public bool IsObstacle() => type == HexType.Obstacle || type == HexType.Hole;

    //Init graph a ajouter pour ajouter les textures en sqrt(2) a 45° pour le passage des Units devant ou derrière les props

    #region glow mats
    //General Glow pour la range
    public void EnableGlow() => glow.ToggleGlow(true);
    public void DisableGlow() => glow.ToggleGlow(false);

    //Glow pour le path
    public void EnableGlowPath() => glow.StartGlowPath();
    public void DisableGlowPath() => glow.ResetGlowPath();

    //Glow pour les kapas
    public void EnableGlowKapa() => glow.ToggleGlowKapa(true);
    public void DisableGlowKapa() => glow.ToggleGlowKapa(false);
    public void GlowOnButton() => glow.GlowKapaOnButton();

    //Glow pour les boutons de sens de kapas
    public void EnableGlowButton() => glow.ToggleSelectGlowKapa(true);
    public void DisableGlowButton() => glow.ToggleSelectGlowKapa(false);
    public void GetColorGlowButton() => glow.StartGlowButton();
    public void ResetColorGlowButton() => glow.ResetGlowButton();
    #endregion
    #endregion
}