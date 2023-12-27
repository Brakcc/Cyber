using System.Collections;
using System.Collections.Generic;
using Enums.UnitEnums.KapaEnums;
using Enums.UnitEnums.UnitEnums;
using GameContent.GridManagement;
using Interfaces.Unit;
using UI.InGameUI;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking
{
    public abstract class Unit : Entity, IUnit
    {
        #region fields/accessors to herit
        
        public abstract AbstractUnitSO UnitData { get; set; }
        public abstract PlayerStatsUI StatUI { get; }
        
        #region current stats
        
        public abstract float CurrentHealth { get; set; }
        public abstract int CurrentMp { get; set; }
        public abstract int CurrentAtk { get; set; }
        public abstract int CurrentDef { get; set; }
        public abstract int CurrentCritRate { get; set; }
        //additional precision stat
        public abstract int CurrentPrecision { get; set; }
        public Vector3 CurrentWorldPos => transform.position;
        
        //BDb Counters
        public abstract int MpBDbCounter { get; set; }
        public abstract int CrBDbCounter { get; set; }
        public abstract int PrecBDbCounter { get; set; }
        public abstract int DefBDbCounter { get; set; }
        public abstract int DeathCounter { get; set; }
        public abstract int DotCounter { get; set; }

        #endregion
        
        public abstract Vector3Int OriginPos { get; protected set; }
        public abstract int TeamNumber { get; set; }
        public abstract bool IsOnTurret { get; protected set; }
        public abstract int UltPoints { get; set; }
        public abstract bool IsDead { get; protected set; }
        public abstract bool CanPlay { get; set; }
        public abstract bool IsPersoLocked { get; set; }
        public abstract bool CanKapa { get; protected set; }
        public abstract bool IsOnComputer {  get; protected set; }
        
        #region Entity heritage
        
        public override Vector3Int CurrentHexPos { get => _currentHexPos; set => _currentHexPos = value; }
        Vector3Int _currentHexPos;
        public override bool IsNetworkEmiter { get; set; }
        public override bool IsOnNetwork { get; protected set; }
        public override int NetworkRange { get; set; }
        public override List<Vector3Int> GlobalNetwork { get; protected set; } = new();
        
        #endregion
        
        #endregion

        #region methodes to herit
        
        public override void OnInit()
        {
            base.OnInit();

            OriginPos = CurrentHexPos;
            CanPlay = false;
            IsDead = false;
            IsPersoLocked = false;
            CanKapa = true;
            IsOnTurret = false;
            IsOnComputer = false;
            if (UnitData.Type == UnitType.Hacker)
            {
                IsNetworkEmiter = true;
                IsOnNetwork = false;
                NetworkRange = UnitData.NetworkRange;
                GlobalNetwork = null;
            }
            else { IsNetworkEmiter = false; NetworkRange = 0; }

            //stats that can vary during the game
            CurrentHealth = UnitData.HealthPoint;
            CurrentAtk = UnitData.Attack;
            CurrentDef = UnitData.Defense;
            CurrentCritRate = UnitData.CritRate;
            CurrentMp = UnitData.MovePoints;
            CurrentPrecision = 100;

            MpBDbCounter = 0;
            CrBDbCounter = 0;
            PrecBDbCounter = 0;
        }
        
        public virtual void Select()
        {
            
        }
        
        public virtual void MoveOnPath(List<Vector3> currentPath) => StartCoroutine(FollowPath(currentPath,UnitData.Speed));

        public void MoveInFrontOf(Vector3 currentPath)
        {
            StartCoroutine(DashGrabPath(currentPath, UnitData.Speed * Constants.SpeedDashMult));
        }
        
        public abstract void OnKapa();
        public virtual void Deselect()
        {
            
        }
        public virtual void OnDie()
        {
            CanPlay = false;
            IsDead = true;
            DeathCounter = 3;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        protected virtual void OnRez()
        {
            CanPlay = false;
            IsDead = false;
            
            ChangeUnitHexPos(this, HexGridStore.hGs);
            PositionCharacterOnTile(HexGridStore.hGs.GetTile(OriginPos).transform.position);
        }
        
        #endregion
    
        #region cache

        private IEnumerator FollowPath(List<Vector3> path, float speed)
        {
            CanKapa = false;
            var  pas = speed * Time.fixedDeltaTime / 10;
            foreach (var i in path)
            {
                float z = path[0].z;
                while (Vector2.Distance(transform.position, i) >= 0.001f)
                {
                    var position = transform.position;
                
                    position = Vector2.MoveTowards(position, i, pas);
                    position = new Vector3(position.x, position.y, z);
                    transform.position = position;
                    yield return null;
                }
            
                PositionCharacterOnTile(i);
            }
            CanKapa = true;

            if (UnitData.Type != UnitType.Hacker) yield break;
            if (GlobalNetwork != null)
            {
                foreach (var i in GlobalNetwork) { HexGridStore.hGs.GetTile(i).DisableGlowDynaNet(); }
            }
            OnGenerateNet(NetworkRange, TeamNumber);
        }

        private IEnumerator DashGrabPath(Vector3 path, float speed)
        {
            CanKapa = false;
            var pas = speed * Time.fixedDeltaTime / 10;
            
            const float z = -0.1f;
            while (Vector2.Distance(transform.position, path) >= 0.001f)
            {
                var position = transform.position;

                position = Vector2.MoveTowards(position, path, pas);
                position = new Vector3(position.x, position.y, z);
                transform.position = position;
                yield return null;
            }

            PositionCharacterOnTile(path);
            
            OnGenerateNet(NetworkRange, TeamNumber);
        }

        public void OnCheckEffectCounter(IUnit unit)
        {
            if (unit.MpBDbCounter > 0)
            {
                unit.MpBDbCounter--;
                if (unit.MpBDbCounter == 0)
                {
                    unit.CurrentMp = unit.UnitData.MovePoints;
                    unit.StatUI.SetMP(unit);
                }
            }
            if (unit.CrBDbCounter > 0)
            {
                unit.CrBDbCounter--;
                if (unit.CrBDbCounter == 0)
                {
                    unit.CurrentCritRate = unit.UnitData.CritRate;
                    unit.StatUI.SetCritRate(unit);
                }
            }
            if (unit.PrecBDbCounter > 0)
            {
                unit.PrecBDbCounter--;
                if (unit.PrecBDbCounter == 0)
                {
                    unit.CurrentPrecision = 100;
                    unit.StatUI.SetPrec(unit);
                }
            }
            if (unit.DefBDbCounter > 0)
            {
                unit.DefBDbCounter--;
                if (unit.DefBDbCounter == 0)
                {
                    unit.CurrentDef = unit.UnitData.Defense;
                    unit.StatUI.SetDef(unit);
                }
            }

            if (unit.DotCounter > 0)
            {
                unit.DotCounter--;
                if (!IsIntersecting(CurrentHexPos, HexGridStore.hGs, UnitData.NetworkRange, out _))
                {
                    DotCounter = 0;
                }
                //Avec ca on part totallement du principe, et de facon tres rigide, que la Kapa de type Dot est 
                //obligatoirement une competence. Donc, on appelle la Kapa 1 dans la liste de Kapa 
                if (unit.DotCounter > 0)
                {
                    unit.UnitData.KapasList[(int)KapaType.Competence].OnExecute(HexGridStore.hGs, GlobalNetwork, unit, true);
                }
            }
        }

        public void OnCheckRez(IUnit unit, out bool rezed)
        {
            rezed = false;
            if (!unit.IsDead) return;
            
            unit.DeathCounter--;
            if (unit.DeathCounter != 0) return;
            
            rezed = true;
            OnRez();
        }

        private void PositionCharacterOnTile(Vector3 pos) =>
            transform.position = new Vector3(pos.x, pos.y, pos.z - Constants.OffsetZPos);
        
        private static void ChangeUnitHexPos(IUnit uT, HexGridStore hexGrid)
        {
            var tT = hexGrid.GetTile(uT.OriginPos);
            
            hexGrid.GetTile(uT.CurrentHexPos).HasEntityOnIt = false;
            hexGrid.GetTile(uT.CurrentHexPos).ClearEntity();
            tT.HasEntityOnIt = true;
            tT.SetEntity(uT);
            uT.CurrentHexPos = tT.HexCoords;
        }

        public void OnSelectSelfTile(IEntity uRef, HexGridStore hexGrid)
        {
            var tile = hexGrid.GetTile(uRef.CurrentHexPos);
            tile.EnableGlowPath();
        }
        public void OnDeselectSelfTile(IEntity uRef, HexGridStore hexGrid)
        {
            var tile = hexGrid.GetTile(uRef.CurrentHexPos);
            tile.DisableGlowPath();
        }
        
        #endregion
    }
}