using System.Collections.Generic;
using Enums.GridEnums;
using GameContent.GameManagement;
using GameContent.GridManagement.GridGraphManagement.GraphInits;
using UnityEngine;

namespace GameContent.Entity.NPC
{
    public class Computer : Entity
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
        
        [SerializeField] private ComputerTarget compTarget;
        public ComputerTarget ComputerTarget => compTarget;

        [SerializeField] private Relay relayRef;
        
        [SerializeField] private GraphInitBoard initBoard;

        #endregion

        #region  methodes

        private void Update()
        {
            if (!relayRef.IsTransmitting)
                return;
            
            GameLoopManager.gLm.OnEndGame();
        }

        public void HandleComputerHack() => initBoard.HandleDeAct(gameObject, false);

        #region Entity overrides

        public sealed override void OnInit()
        {
            base.OnInit();
            IsNetworkEmiter = false;
            IsOnNetwork = false;
            NetworkRange = 0;
        
            initBoard.SetRenderer(gameObject);
        }
        
        public sealed override void OnGenerateNet(int range, int team) { }

        #endregion
        
        #endregion
    }
}