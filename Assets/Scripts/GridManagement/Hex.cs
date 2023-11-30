using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[SelectionBase]
public class Hex : MonoBehaviour
{
    #region 
    [SerializeField] HexType type;
    [SerializeField] SelectGlow glow;

    [SerializeField] Network originNetwork;

    //la Data importante
    public Vector3Int HexCoords { get; set; }
    public bool HasEntityOnIt { get; set; }
    public Unit PlayerRef { get; set; }
    public Network LocalNetwork { get => originNetwork; set { originNetwork = value; } }
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

    #region Network
    Network GetLocalNetwork() => originNetwork;

    //public void AddMixedNetwork(Network net) { MixedNetwork.Add(net); EnableGlowPath(); mixedNetwork = MixedNetwork; }
    //public void AddMixedNetwork(List<Network> networks) { MixedNetwork.AddRange(networks); EnableGlowPath(); mixedNetwork = MixedNetwork; }

    //public void ClearLMixedNetwork() { MixedNetwork.Clear(); DisableGlowPath(); }
    #endregion

    public void SetUnit(Unit unit) => PlayerRef = unit;
    public Unit GetUnit() => PlayerRef;
    public void ClearUnit() => PlayerRef = null;

    public bool IsObstacle() => type == HexType.Obstacle || type == HexType.Hole;
    public bool IsComputer() => type == HexType.Computer;

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

    //Glow pour les base network
    public void EnableGlowBaseNet() => glow.ToggleBaseNetGlow(true);
    public void DisableGlowBaseNet() => glow.ToggleBaseNetGlow(false);

    //Glow pour les dynamic network
    public void EnableGlowDynaNet() => glow.ToggleDynaNetGlow(true);
    public void DisableGlowDynaNet() => glow.ToggleDynaNetGlow(false);
    #endregion
    #endregion
}