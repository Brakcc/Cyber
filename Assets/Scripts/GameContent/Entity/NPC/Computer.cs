using System.Collections.Generic;
using Enums.GridEnums;
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
        
        public bool GotHacked { get; set; }
        
        [SerializeField] private GraphInitBoard initBoard;

        #endregion

        #region  methodes
        
        public void HandleComputerHack() => initBoard.HandleDeAct(gameObject, false);

        #region Entity overrides

        public sealed override void OnInit()
        {
            base.OnInit();
            IsNetworkEmiter = false;
            IsOnNetwork = false;
            NetworkRange = 0;
        
            GotHacked = false;
        
            initBoard.SetRenderer(gameObject);
        }
        
        public sealed override void OnGenerateNet(int range) { }

        #endregion
        
        #endregion
    }
}