using System.Collections;
using System.Collections.Generic;
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
        public abstract int CurrentCRRez { get; set; }
        //additional precision stat
        public abstract int CurrentPrecision { get; set; }
        public Vector3 CurrentWorldPos => transform.position;
        
        //BDb Counters
        public abstract int MpBDbCounter { get; set; }
        public abstract int CrBDbCounter { get; set; }
        public abstract int PrecBDbCounter { get; set; }
        public abstract int DefBDbCounter { get; set; }
        public abstract int CrRezBDbCounter { get; set; }
        public abstract int TempKapaMult { get; set; }

        #endregion
        
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
        public override List<Vector3Int> GlobalNetwork { get; set; } = new();
        
        #endregion
        
        #endregion

        #region methodes to herit
        public override void OnInit()
        {
            base.OnInit(); 
            
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
            //currentPath.RemoveAt(currentPath.Count - 1);
            StartCoroutine(DashGrabPath(currentPath, UnitData.Speed * ConstList.SpeedDashMult));
        }
        
        public abstract void OnKapa();
        public virtual void Deselect()
        {
            if (UnitData.Type != UnitType.Hacker) return;
            if (GlobalNetwork == null) return;
        }
        public virtual void OnDie()
        {
            CanPlay = false;
            IsDead = true;
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
        public virtual void OnRez()
        {
            CanPlay = false;
            IsDead = false;
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        #endregion
    
        #region cache
        IEnumerator FollowPath(List<Vector3> path, float speed)
        {
            CanKapa = false;
            float pas = speed * Time.fixedDeltaTime / 10;
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
            OnGenerateNet();

            if (MpBDbCounter > 0)
            {
                MpBDbCounter--;
                if (MpBDbCounter == 0)
                {
                    CurrentMp = UnitData.MovePoints;
                    StatUI.SetMP(this);
                }
            }
            if (CrBDbCounter > 0)
            {
                CrBDbCounter--;
                if (CrBDbCounter == 0)
                {
                    CurrentCritRate = UnitData.CritRate;
                    StatUI.SetCritRate(this);
                }
            }
            if (PrecBDbCounter > 0)
            {
                PrecBDbCounter--;
                if (PrecBDbCounter == 0)
                {
                    CurrentPrecision = 100;
                    StatUI.SetPrec(this);
                }
            }
        }

        IEnumerator DashGrabPath(Vector3 path, float speed)
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
            
            OnGenerateNet();
        }

        void PositionCharacterOnTile(Vector3 pos) => transform.position = new Vector3(pos.x, pos.y, pos.z - 0.1f);
        #endregion
    }
}