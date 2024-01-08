using System.Collections.Generic;
using GameContent.Entity.Unit.UnitWorking;
using GameContent.GridManagement;
using UI.InGameUI;
using UnityEngine;

namespace Interfaces.Unit
{
    public interface IUnit : IEntity
    {
        #region accessors
        
        AbstractUnitSO UnitData { get; }
        PlayerStatsUI StatUI { get; }

        #region current stats
        
        float CurrentHealth { get; set; }
        int CurrentMp { get; set;  }
        int CurrentCritRate { get; set; }
        int CurrentDef { get; set;  }
        int CurrentAtk { get; }
        //additional precision stat
        int CurrentPrecision { get; set; }
        Vector3 CurrentWorldPos { get; }

        //Compteurs de tours sur les buffs/debuffs
        List<int> BDbCounters { get; set; }
        List<BuffDatas> BuffLists { get; set; }
        int DeathCounter { get; set; }
        int DotCounter { get; set; }
        
        //Pos
        Vector3Int SpawnPos { get; set; }
        
        #endregion

        Vector3Int OriginPos { get; }
        int TeamNumber { get; set; }
        bool IsOnTurret { get; }
        int UltPoints { get; set; }
        bool IsDead { get; }
        bool CanPlay { get; set; }
        bool IsPersoLocked { get; set; }
        bool CanKapa { get; }
        bool IsOnComputer { get; }
        Color OriginColor { get; }
        
        #endregion

        #region methodes
        
        void Select();
        void MoveOnPath(List<Vector3> currentPath);
        void MoveInFrontOf(Vector3 currentPath);
        void OnCheckEffectCounter(IUnit unit);
        void OnKapa();
        void OnColorFeedback(Color originC, int last);
        void Deselect();
        void OnDie();
        void OnCheckRez(IUnit unit, out bool rezed);
        void OnSelectSelfTile(IEntity uRef, HexGridStore hexGrid);
        void OnDeselectSelfTile(IEntity uRef, HexGridStore hexGrid);

        #endregion
    }
}
