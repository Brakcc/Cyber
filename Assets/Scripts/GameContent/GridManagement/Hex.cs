using Enums.GridEnums;
using GameContent.GridManagement.GridGraphManagement;
using Interfaces.Unit;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("computerTarget")]
        [ShowIfTrue("type", new[]{(int)HexType.Computer})]
        [SerializeField] private RelayTarget relayTarget;
        private IEntity _entityRef;
        
        //la Data importante
        public Vector3Int HexCoords { get; private set; }
        public bool HasEntityOnIt { get; set; }
        public HexType CurrentType { set => type = value; }
        public NetworkType LocalNetwork { get => originNetwork; set => originNetwork = value; }
        public RelayTarget RelayTarget
        {
            get => relayTarget;
            set => relayTarget = value;
        }

        #endregion

        #region methodes

        private void Awake()
        {
            HexCoords = new HexCoordonnees(gameObject).OffsetCoordonnees;
            RelayTarget = RelayTarget.None;
            glow.SetHexaRefs();
            _entityRef = null;
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

        public void SetEntity(IEntity entity) => _entityRef = entity;
        public T GetEntity<T>() where T : IEntity => _entityRef is T entityRef ? entityRef : default;
        public void ClearEntity() => _entityRef = null;

        public bool IsObstacle() => type is HexType.Obstacle or HexType.Hole;
        public bool IsComputer() => type is HexType.Computer;

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