using System.Collections.Generic;
using System.Linq;
using Enums.GridEnums;
using GameContent.GridManagement;
using GameContent.GridManagement.GridGraphManagement;
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
        public override List<Vector3Int> GlobalNetwork { get => null;
            protected set { } }

        #endregion

        #region Hack

        [SerializeField] private RelayTarget reTarget;
        public RelayTarget ReTarget => reTarget;
        
        public bool GotHacked { get; private set; }
        private bool _isTransmitting;
        public bool IsTransmitting
        {
            get => _isTransmitting;
            set
            {
                IsTransmitting = PrecRefList.Any(i => i.GotHacked && i.IsTransmitting);
                _isTransmitting = value;
                foreach (var i in PrecRefList)
                {
                    if (i.IsTransmitting && i.GotHacked)
                    {
                        OnLink(i);
                    }
                }
            }
        }
        
        public IEnumerable<Relay> PrecRefList => precRefList;
        [SerializeField] private Relay[] precRefList;
        
        [SerializeField] private Hex[] networkRef;
                
        [SerializeField] private GraphInitBoard initBoard;

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
        
            initBoard.SetRenderer(gameObject);
            
            OnNeighbourgsInit(CurrentHexPos, HexGridStore.hGs);
        }
        
        public sealed override void OnGenerateNet(int range) { }
        
        #endregion
        
        #region Hack methodes
        
        private void HandleComputerHack() => initBoard.HandleDeAct(gameObject, false);
        
        private void OnNeighbourgsInit(Vector3Int pos, HexGridStore hexGrid)
        {
            foreach (var t in hexGrid.GetNeighbourgs(pos))
            {
                var tile = hexGrid.GetTile(t);
                tile.CurrentType = HexType.Computer;
                tile.RelayTarget = ReTarget;
            }
        }
        
        public void OnNetworkHack()
        {
            HandleComputerHack();
            GotHacked = true;

            foreach (var i in networkRef)
            {
                i.LocalNetwork = NetworkType.Net1;
                //diff les glows pour la team atk et def
            }
            
            OnCheckIfPrecOn();
        }
        
        public void OnCheckIfPrecOn()
        {
            
        }

        public void OnLink(Relay relayTarget)
        {
            Debug.Log("allez");
            //LineMeshRender
        }

        #endregion
        
        #endregion
    }
}