using System.Collections.Generic;
using Enums.GridEnums;
using GameContent.GridManagement;
using GameContent.GridManagement.GridGraphManagement.GraphInits;
using UnityEngine;

namespace GameContent.Entity.NPC
{
    public class Relay : Entity
    {
        #region fields

        #region Entity herited

        public override Vector3Int CurrentHexPos { get; set; }
        public override bool IsNetworkEmiter { get; set; }
        public override bool IsOnNetwork { get; protected set; }
        public override int NetworkRange { get; set; }
        public override List<Vector3Int> GlobalNetwork { get => null; protected set { } }

        #endregion

        #region Hack

        [SerializeField] private RelayTarget reTarget;
        private RelayTarget ReTarget => reTarget;
        
        #region Hack checkers
        public bool GotHacked { get; private set; }
        private bool _isTransmitting;

        private bool IsTransmitting
        {
            get => _isTransmitting;
            set =>
                //_isTransmitting = PrecRefList.Any(i => i.GotHacked && i.IsTransmitting);
                _isTransmitting = value;
        }

        #endregion

        #region RelayRefs

        [SerializeField] private RelayRefs relayRefs;
        
        [System.Serializable]
        public class RelayRefs
        {
            public Relay[] precRefList;
            public Relay[] nextRefList;
            public Hex[] networkRef;
        }
        
        #endregion
        
        #region Graphs

        [SerializeField] private RelayGraphs relayGraphs;
        
        [System.Serializable]
        public class RelayGraphs
        {
            public GraphInitBoard initBoard;
            public LineGraphInit lineGraphInit;
        }

        private List<GameObject> _lineList;

        #endregion
        
        #endregion
        
        #endregion

        #region  methodes

        #region Entity overrides

        public sealed override void OnInit()
        {
            base.OnInit();
            IsNetworkEmiter = false;
            IsOnNetwork = false;
            NetworkRange = 0;
        
            GotHacked = false;
            IsTransmitting = false;
        
            relayGraphs.initBoard.SetRenderer(gameObject);
            
            OnNeighbourgsInit(CurrentHexPos, HexGridStore.hGs);
            OnGenerateLinks();
        }
        
        public sealed override void OnGenerateNet(int range) { }

        #endregion
        
        #region Links
        
        private void OnGenerateLinks()
        {
            _lineList = new List<GameObject>();
            
            if (relayRefs.nextRefList == null)
                return;
            
            foreach (var relay in relayRefs.nextRefList)
            {
                var newGO = OnGenerateLine(transform.position, relay.transform.position, relayGraphs.lineGraphInit);
                newGO.transform.SetParent(transform);
                newGO.GetComponent<LineRenderer>().enabled = false;
                
                _lineList.Add(newGO);
            }
        }

        private static GameObject OnGenerateLine(Vector3 startPos, Vector3 endPos, LineGraphInit lineInit)
        {
            var GO = new GameObject();
            var line = GO.AddComponent<LineRenderer>();
            
            line.SetPositions(new []{ startPos, endPos });
            line.colorGradient = lineInit.colorGrad;
            line.sortingOrder = lineInit.orderInLayer;
            line.widthCurve = lineInit.widthCurve;
            line.alignment = lineInit.align;
            line.material = lineInit.mat;

            return GO;
        }
        
        #endregion
        
        #region Hack methodes
        
        private void OnNeighbourgsInit(Vector3Int pos, HexGridStore hexGrid)
        {
            foreach (var t in hexGrid.GetNeighbourgs(pos))
            {
                var tile = hexGrid.GetTile(t);
                if (tile.IsObstacle())
                    continue;
                
                tile.CurrentType = HexType.Computer;
                tile.RelayTarget = ReTarget;
            }
        }
        
        private void HandleComputerHack() => relayGraphs.initBoard.HandleDeAct(gameObject, false);
        
        public void OnNetworkHack()
        {
            HandleComputerHack();
            GotHacked = true;

            if (relayRefs.precRefList.Length == 0)
            {
                IsTransmitting = true;
            }
            
            foreach (var i in relayRefs.networkRef)
            {
                i.LocalNetwork = NetworkType.Net1;
                i.DisableGlowBaseNet();
                i.EnableGlowHackedNet();
            }
            
            OnCheckIfPrecTransmit(this, relayRefs.precRefList, _lineList);
            OnCheckIfNextGotHacked(this, relayRefs.nextRefList, _lineList);
        }
        
        #region Hack chain
        
        private static void OnCheckIfPrecTransmit(Relay rRef, IReadOnlyList<Relay> precRelayRefs, IReadOnlyList<GameObject> lines)
        {
            if (precRelayRefs == null)
                return;

            for (var i = 0; i < precRelayRefs.Count; i++)
            {
                if (!precRelayRefs[i].IsTransmitting)
                    continue;
                rRef.IsTransmitting = true;
                OnCheckIfPrecTransmit(precRelayRefs[i], precRelayRefs[i].relayRefs.nextRefList,
                    precRelayRefs[i]._lineList);
                Debug.Log("2");
                
                if (lines.Count == 0)
                    continue;
                OnLink(i, lines);
            }
        }

        private static void OnCheckIfNextGotHacked(Relay rRef, IReadOnlyList<Relay> nextRelyRefs, IReadOnlyList<GameObject> lines)
        {
            if (nextRelyRefs == null)
                return;
            if (!rRef.IsTransmitting)
                return;

            for (var i = 0; i < nextRelyRefs.Count; i++)
            {
                if (!nextRelyRefs[i].GotHacked)
                    continue;

                nextRelyRefs[i].IsTransmitting = true;
                OnCheckIfNextGotHacked(nextRelyRefs[i], nextRelyRefs[i].relayRefs.nextRefList,
                    nextRelyRefs[i]._lineList);
                Debug.Log("1");
                
                if (lines.Count == 0)
                    continue;
                OnLink(i, lines);
                
            }
        }

        private static void OnLink(int iD, IReadOnlyList<GameObject> lines)
            => lines[iD].GetComponent<LineRenderer>().enabled = true;
        
        #endregion

        #endregion
        
        #endregion
    }
}