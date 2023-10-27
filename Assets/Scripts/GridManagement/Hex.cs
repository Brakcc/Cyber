using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    #region fields
    private SelectGlow glow;
    [SerializeField] private HexType type;

    //la Data importante
    [HideInInspector] public Vector3Int hexCoords;
    [HideInInspector] public bool hasPlayerOnIt;
    #endregion

    #region methodes
    void Awake()
    {
        hexCoords = new HexCoordonnees(gameObject).OffsetCoordonnees;
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

    #region glow mats
    public bool IsObstacle() => type == HexType.Obstacle || hasPlayerOnIt;

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