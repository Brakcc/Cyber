﻿using System.Collections.Generic;
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
        int CurrentMp { get; }
        int CurrentCritRate { get; }
        int CurrentDef { get; }
        int CurrentAtk { get; }
        //additional precision stat
        int CurrentPrecision { get; }
        Vector3 CurrentWorldPos { get; }
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
        void OnKapa();
        void Deselect();
        void OnDie();
        void OnRez();
        #endregion
    }
}
