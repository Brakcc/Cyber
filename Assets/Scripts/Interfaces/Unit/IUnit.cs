using System.Collections.Generic;
using GameContent.Entity.Unit.UnitWorking;
using UI.InGameUI;
using UnityEngine;

namespace Interfaces.Unit
{
    public interface IUnit : IEntity
    {
        #region accessors
        
        AbstractUnitSO UnitData { get; set; }
        PlayerStatsUI StatUI { get; }

        #region current stats
        
        float CurrentHealth { get; set; }
        int CurrentMp { get; set;  }
        int CurrentCritRate { get; set; }
        int CurrentDef { get; set;  }
        int CurrentAtk { get; set; }
        //additional precision stat
        int CurrentPrecision { get; set; }
        Vector3 CurrentWorldPos { get; }

        //Compteurs de tours sur les buffs/debuffs
        int MpBDbCounter { get; set; }
        int CrBDbCounter { get; set; }
        int PrecBDbCounter { get; set; }
        int DefBDbCounter { get; set; }
        
        #endregion

        int TeamNumber { get; set; }
        bool IsOnTurret { get; }
        int UltPoints { get; set; }
        bool IsDead { get; }
        bool CanPlay { get; set; }
        bool IsPersoLocked { get; set; }
        bool CanKapa { get; }
        bool IsOnComputer { get; }
        
        #endregion

        #region methodes
        void Select();
        void MoveOnPath(List<Vector3> currentPath);
        void MoveInFrontOf(Vector3 currentPath);
        void OnCheckBuffDebuffCounter();
        void OnKapa();
        void Deselect();
        void OnDie();
        void OnRez();
        #endregion
    }
}
