using System.Collections.Generic;
using System.Linq;
using Enums.GridEnums;
using GameContent.GridManagement;
using GameContent.GridManagement.GridGraphManagement;
using UnityEngine;

namespace GameContent.Entity.NPC
{
    public class Turret : Entity
    {
        #region fields
        public override Vector3Int CurrentHexPos { get; set; }
        public override bool IsNetworkEmiter { get; set; }
        public override bool IsOnNetwork { get; protected set; }
        public override int NetworkRange { get; set; }
        public override List<Vector3Int> GlobalNetwork { get; protected set; } = new();

        [SerializeField] GraphInitEntity graphInit;
        #endregion

        #region methodes
        void OnEnable()
        {
            OnInit();
        }

        /// <summary>
        /// init a l'enable
        /// </summary>
        public sealed override void OnInit()
        {
            base.OnInit();
            IsNetworkEmiter = true;
            IsOnNetwork = true;
            NetworkRange = 2;
            HexGridStore.hGs.OnAddEmiter(this);
            OnGenerateNet(NetworkRange);
            graphInit.SetRenderer(gameObject);

            foreach (var e in HexGridStore.hGs.emiters)
            {
                e.OnGenerateNet(NetworkRange);
            }
        }

        /// <summary>
        /// override de la OnGenNet de Entity avec list gen diff
        /// </summary>
        public sealed override void OnGenerateNet(int range)
        {
            IsIntersecting(CurrentHexPos, HexGridStore.hGs, range, out List<NetworkType> net);
            GlobalNetwork = OnIntersect(CurrentHexPos, HexGridStore.hGs, range, net);
        

            if (net.Count != 0)
            {
                foreach (var i in net)
                {
                    HexGridStore.hGs.NetworkList[(int)i].AddRange(GlobalNetwork);
                }
                foreach (var g in GlobalNetwork)
                {
                    HexGridStore.hGs.GetTile(g).LocalNetwork = net[^1];
                }
            }
            else
            {
                foreach (var g in GlobalNetwork)
                {
                    HexGridStore.hGs.GetTile(g).LocalNetwork = (NetworkType)HexGridStore.hGs.NetworkList.Length - HexGridStore.hGs.EmptySockets;
                }
                HexGridStore.hGs.EmptySockets--;
            }
        }

        /// <summary>
        /// Intersect les listes et gen les outlines specifiquement pour les tourelles
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="hexGrid"></param>
        /// <param name="range"></param>
        /// <param name="toMerge"></param>
        /// <returns></returns>
        protected sealed override List<Vector3Int> OnIntersect(Vector3Int pos, HexGridStore hexGrid, int range, List<NetworkType> toMerge)
        {
            List<Vector3Int> newRange = GetRangeList(pos, hexGrid, range).ToList();

            if (toMerge.Count == 0)
            {

                foreach (var l in newRange)
                {
                    hexGrid.GetTile(l).EnableGlowBaseNet();
                }

                return newRange;
            }

            else
            {
                foreach (var i in toMerge)
                {
                    foreach (var j in hexGrid.NetworkList[(int)i])
                    {
                        if (!newRange.Contains(j)) { newRange.Add(j); }
                    }
                }

                foreach (var l in newRange)
                {
                    hexGrid.GetTile(l).EnableGlowBaseNet();
                }

                return newRange;
            }
        }
        #endregion
    }
}