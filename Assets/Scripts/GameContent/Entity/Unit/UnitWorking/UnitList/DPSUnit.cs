using System.Collections.Generic;
using GameContent.GridManagement.GridGraphManagement.GraphInits;
using UI.InGameUI;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking.UnitList
{
    [SelectionBase]
    public class DPSUnit : Unit
    {
        #region inherited accessors
        
        //moves fields 
        [SerializeField] private AbstractUnitSO m_Unit;
        public override AbstractUnitSO UnitData { get => m_Unit; set => m_Unit = value; }

        [SerializeField] private PlayerStatsUI m_StatsUI;
        public override PlayerStatsUI StatUI => m_StatsUI;

        //Game Loop Logic BALEK LA VISIBILITE et BALEK LE SCRIPTABLE
        
        #region current stats
        
        public override float CurrentHealth { get; set; }
        public override int CurrentMp { get; set; }
        public override int CurrentAtk { get; set; }
        public override int CurrentDef { get; set; }
        public override int CurrentCritRate { get; set; }
        //additional precision stat
        public override int CurrentPrecision { get; set; }
        
        //BDb Counters
        public override List<int> BDbCounters { get; set; }
        public override int DeathCounter { get; set; }
        public override int DotCounter { get; set; }
        
        #endregion

        public override Vector3Int OriginPos { get; protected set; }
        public override int TeamNumber { get; set; }
        public override bool IsOnTurret { get; protected set; }
        public override int UltPoints { get; set; }
        public override bool CanPlay { get; set; }
        public override bool IsDead { get; protected set; }
        public override bool IsPersoLocked { get; set; }
        public override bool CanKapa { get; protected set; }
        public override bool IsOnComputer { get; protected set; }

        [SerializeField] private GraphInitUnit graphs;
        
        #endregion

        #region other fields
        
        //graph fields
        private SpriteRenderer _rend;
        
        #endregion

        #region methodes
        
        public override void OnInit()
        {
            graphs.SetRenderer(gameObject, m_Unit.Sprite);
            _rend = GetComponentInChildren<SpriteRenderer>();
            OriginColor = _rend.color;
            
            base.OnInit();
        }

        private void Start()
        {
            foreach (var kap in m_Unit.KapasList)
            {
                kap.InitPatterns(kap.Patterns);
            }
        }
        #endregion

        #region inherited methodes
        public override void Select()
        {
            base.Select();
            _rend.color = Color.Lerp(Color.red, Color.yellow, 0.5f);
        }
        public override void Deselect()
        {
            base.Deselect();
            _rend.color = OriginColor;
        }
        public override void OnKapa() => Debug.Log("Omegalul");

        protected sealed override void OnRez()
        {
            base.OnRez();
            GetComponentInChildren<SpriteRenderer>().color = OriginColor;
        }

        #endregion
    }
}