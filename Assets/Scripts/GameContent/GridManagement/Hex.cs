using Enums.GridEnums;
using GameContent.GridManagement.GridGraphManagement;
using Interfaces.Unit;
using UnityEngine;
using Utilities.CustomHideAttribute;

namespace GameContent.GridManagement
{
    [SelectionBase]
    public class Hex : MonoBehaviour
    {
        #region fields
        
        [SerializeField] private HexType type;
        [SerializeField] private SelectGlow glow;

        [SerializeField] private NetworkType originNetwork;
        [ShowIfTrue("type", new[]{(int)HexType.Computer})]
        [SerializeField] private ComputerTarget computerTarget;

        //la Data importante
        public Vector3Int HexCoords { get; private set; }
        public bool HasEntityOnIt { get; set; }
        private IUnit _unitRef;
        public HexType CurrentType { set => type = value; }
        public NetworkType LocalNetwork { get => originNetwork; set => originNetwork = value; }
        public ComputerTarget ComputerTarget => computerTarget;
        
        #endregion

        #region methodes
        
        void Awake()
        {
            HexCoords = new HexCoordonnees(gameObject).OffsetCoordonnees;
            glow.SetHexaRefs();
            _unitRef = null;
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
            HexType.Computer => 1,
            _ => 1000
        };

        public void SetUnit(IUnit unit) => _unitRef = unit;
        public IUnit GetUnit() => _unitRef;
        public void ClearUnit() => _unitRef = null;

        public bool IsObstacle() => type is HexType.Obstacle or HexType.Hole;
        public bool IsComputer() => type is HexType.Computer;

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
}