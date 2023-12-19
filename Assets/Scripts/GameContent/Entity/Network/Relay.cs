using System.Collections.Generic;
using Enums.GridEnums;
using GameContent.GridManagement.GridGraphManagement;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Network
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
        
        [SerializeField] RelayTarget reTarget;
        public RelayTarget RelayTarget => reTarget;
        
        public bool GotHacked { get; set; }
        
        public IEntity[] EntityRef => entityRef;
        [SerializeField] private IEntity[] entityRef;
        
        [SerializeField] GraphInitBoard initBoard;

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