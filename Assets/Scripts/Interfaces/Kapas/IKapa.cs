using System.Collections.Generic;
using Enums.UnitEnums.KapaEnums;
using GameContent.Entity.Unit.KapasGen;
using GameContent.GridManagement;
using Interfaces.Unit;
using UnityEngine;

namespace Interfaces.Kapas
{
    public interface IKapa
    {
        #region fields
        string KapaName { get; }
        int ID { get; }
        KapaType KapaType { get; }
        string Description { get; }
        int MaxPlayerPierce { get; }
        int BalanceMult { get; }
        EffectType EffectType { get; }
        KapaFunctionType KapaFunctionType { get; }
        KapaUISO KapaUI { get; }
        GameObject DamageFeedBack { get; }
        Vector3Int[] Patterns { get; }
        
        #region paterns to herit
        
        //North tiles
        Vector3Int[] OddNTiles { get; }
        Vector3Int[] EvenNTiles { get; }
        //EN tiles
        Vector3Int[] OddENTiles { get; }
        Vector3Int[] EvenENTiles { get; }
        //ES tiles
        Vector3Int[] OddESTiles { get;  }
        Vector3Int[] EvenESTiles { get; }
        //S tiles
        Vector3Int[] OddSTiles { get; }
        Vector3Int[] EvenSTiles { get; }
        //WS tiles
        Vector3Int[] OddWSTiles { get; }
        Vector3Int[] EvenWSTiles { get; }
        //WN tiles
        Vector3Int[] OddWNTiles { get; }
        Vector3Int[] EvenWNTiles { get; }
        
        #endregion
        
        #endregion

        #region methodes
        
        void InitPatterns(Vector3Int[] patterns);
        bool OnCheckKapaPoints(IUnit unit);
        List<Vector3Int> OnSelectGraphTiles(IUnit unit, HexGridStore hexGrid, Vector3Int[] tilesArray);
        void OnDeselectTiles(HexGridStore hexGrid, List<Vector3Int> pattern);
        List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IUnit unit);
        
        #endregion
    }
}

