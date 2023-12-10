using System.Collections.Generic;
using Enums.UnitEnums.KapaEnums;
using GameContent.GameManagement;
using GameContent.GridManagement;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.DifKapas
{
    [CreateAssetMenu(fileName = "DeAct Kapa", menuName = "Tactical/Kapas/DeAct")]
    public class DeActKapaSO : AbstractKapaSO
    {
        #region inherited accessors
        public override string KapaName => _kapaName;
        [SerializeField] string _kapaName;
        public override int ID => _id;
        [SerializeField] int _id;
        public override KapaType KapaType => _kapaType;
        [SerializeField] KapaType _kapaType;
        public override string Description => _description;
        [SerializeField] string _description;
        public override int Cost => 0;
        public override int MaxPlayerPierce => 0;
        public override float BalanceCoeff => 0;
        public override EffectType EffectType => EffectType.None;
        public override KapaFunctionType KapaFunctionType => KapaFunctionType.Default;
        public override KapaUISO KapaUI => _kapaUI;
        [SerializeField] KapaUISO _kapaUI;
        public override GameObject DamageFeedBack => null;
        public override Vector3Int[] Patterns => null;
        #endregion

        #region inherited paterns/accessors
        //North tiles
        public override Vector3Int[] OddNTiles { get => null; protected set { } }
        public override Vector3Int[] EvenNTiles { get => null; protected set { } }

        //EN tiles
        public override Vector3Int[] OddENTiles { get => null; protected set { } }
        public override Vector3Int[] EvenENTiles { get => null; protected set { } }

        //ES tiles
        public override Vector3Int[] OddESTiles { get => null; protected set { } }
        public override Vector3Int[] EvenESTiles { get => null; protected set { } }

        //S tiles
        public override Vector3Int[] OddSTiles { get => null; protected set { } }
        public override Vector3Int[] EvenSTiles { get => null; protected set { } }

        //WS tiles
        public override Vector3Int[] OddWSTiles { get => null; protected set { } }
        public override Vector3Int[] EvenWSTiles { get => null; protected set { } }

        //WN tiles
        public override Vector3Int[] OddWNTiles { get => null; protected set { } }
        public override Vector3Int[] EvenWNTiles { get => null; protected set { } }
        #endregion

        #region inherited methodes (rendues null)
        public sealed override bool OnCheckKapaPoints(IUnit unit)
        {
            return HexGridStore.hGs.GetTile(unit.CurrentHexPos).IsComputer() &&
                   !HexGridStore.hGs.computerList[(int)HexGridStore.hGs.GetTile(unit.CurrentHexPos).ComputerTarget]
                       .GotHacked && unit.TeamNumber == 1;
        }

        public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
        {
            GameLoopManager.gLm.HandleComputerValueChange();
            hexGrid.HandlePCHacked(hexGrid.GetTile(unit.CurrentHexPos).ComputerTarget);
        }
    
        public sealed override void InitPatterns(Vector3Int[] p) { }

        public sealed override List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IEntity entity) => null;
        public sealed override List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IUnit unit) => null;

        public sealed override List<Vector3Int> OnSelectGraphTiles(IUnit unit, HexGridStore hexGrid, Vector3Int[] tilesArray) => null;
        #endregion
    }
}