using System.Collections.Generic;
using System.Linq;
using Enums.GridEnums;
using GameContent.GridManagement;
using GameContent.GridManagement.HexPathFind;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        #region interface fields
        
        public abstract Vector3Int CurrentHexPos { get; set; }
        public abstract bool IsNetworkEmiter { get; set; }
        public abstract bool IsOnNetwork { get; protected set; }
        public abstract int NetworkRange { get; set; }
        public abstract List<Vector3Int> GlobalNetwork { get; protected set; }
        
        #endregion

        #region methodes
        
        public virtual void OnInit() => CurrentHexPos = HexCoordonnees.GetClosestHex(transform.position);

        /// <summary>
        /// Renvoie la liste (IEnumerable) complete de tiles dans la range d'un NPC
        /// </summary>
        /// <param name="hexPos">Position de depart pour calculer des tiles de reseau</param>
        /// <param name="hexGrid">Ref au HexGridStore.hGS</param>
        /// <param name="range">Range max du reseau de l'Entity</param>
        /// <returns>IEnumerable des tiles d'un NPC</returns>
        protected static IEnumerable<Vector3Int> GetRangeList(Vector3Int hexPos, HexGridStore hexGrid, int range) => PathFind.PathKapaVerif(hexGrid, hexPos, range).GetRangePositions();

        /// <summary>
        /// Verif d'intersection entre diff reseaux
        /// </summary>
        /// <param name="pos">position d'origine de la range locale</param>
        /// <param name="hexGrid">Ref au HexGridStoree.hGS</param>
        /// <param name="range">range du reseau local</param>
        /// <param name="net">list des reseaux de base impactes par le merge, s'il il y a intersection</param>
        /// <returns>bool de validation d'intersection</returns>
        protected static void IsIntersecting(Vector3Int pos, HexGridStore hexGrid, int range, out List<NetworkType> net)
        {
            net = IsInterOnNet(pos, hexGrid, range);
        }

        /// <summary>
        /// S'il il y a intersection entre reseau Local et Base, lance un merge de reseau
        /// </summary>
        /// <param name="pos">pos de depart de la range de reseau local</param>
        /// <param name="hexGrid">Ref de HexGridStore.hGS</param>
        /// <param name="range">range max du reseau local</param>
        /// <returns>liste de network impactee par le merge</returns>
        private static List<NetworkType> IsInterOnNet(Vector3Int pos, HexGridStore hexGrid, int range)
        {
            List<NetworkType> toMerge = new();
            foreach (var i in GetRangeList(pos, hexGrid, range))
            {
                var t = hexGrid.GetTile(i);
                if (t.LocalNetwork != NetworkType.None && !toMerge.Contains(t.LocalNetwork))
                {
                    toMerge.Add(t.LocalNetwork);
                }
            }
            return toMerge;
        }

        /// <summary>
        /// Merge les reseaux local et bases // creer un reseau de base si pas inter
        /// </summary>
        /// <param name="pos">pos de depart de la range de reseau local</param>
        /// <param name="hexGrid">Ref de HexGridStore.hGS</param>
        /// <param name="range">range max du reseau local</param>
        /// <param name="toMerge">liste de network devant etre merge</param>
        /// <returns>la nouvelle list de reseau local du hacker</returns>
        protected virtual List<Vector3Int> OnIntersect(Vector3Int pos, HexGridStore hexGrid, int range, List<NetworkType> toMerge)
        {
            var newRange = GetRangeList(pos, hexGrid, range).ToList();

            if (toMerge.Count == 0)
            {
                return newRange;
            }

            foreach (var i in toMerge)
            {
                foreach (var j in hexGrid.NetworkList[(int)i])
                {
                    if (!newRange.Contains(j)) { newRange.Add(j); }
                }
            }

            return newRange;
        }

        /// <summary>
        /// Remplace l'ancien global network par le nouveau // Fonction generale de generation de reseau /!\
        /// </summary>
        public virtual void OnGenerateNet(int range)
        {
            IsIntersecting(CurrentHexPos, HexGridStore.hGs, range, out var net);
            GlobalNetwork = OnIntersect(CurrentHexPos, HexGridStore.hGs, range, net);
        }

        public void OnSelectNetworkTiles()
        {
            IsOnNetwork = true;

            foreach (var l in GlobalNetwork)
            {
                HexGridStore.hGs.GetTile(l).EnableGlowDynaNet();
            }
        }
        public void OnDeselectNetworkTiles()
        {
            if (!IsOnNetwork) return;

            foreach (var l in GlobalNetwork)
            {
                HexGridStore.hGs.GetTile(l).DisableGlowDynaNet();
            }
            IsOnNetwork = false;
        }
        
        #endregion
    }
}