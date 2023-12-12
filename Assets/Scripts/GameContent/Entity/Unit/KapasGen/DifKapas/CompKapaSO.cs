using System.Collections.Generic;
using CameraManagement;
using Enums.UnitEnums.KapaEnums;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Dash;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Grab_Push;
using GameContent.GameManagement;
using GameContent.GridManagement;
using Interfaces.Unit;
using UnityEngine;
using Utilities.CustomHideAttribute;

namespace GameContent.Entity.Unit.KapasGen.DifKapas
{
    [CreateAssetMenu(fileName = "Competence Kapa", menuName = "Tactical/Kapas/Competence")]
    public class CompKapaSO : AbstractKapaSO
    {
        #region inherited accessors
        
        public override KapaUISO KapaUI => _kapaUI;
        [SerializeField] KapaUISO _kapaUI;

        public override GameObject DamageFeedBack => _damageFeedBack;
        [SerializeField] GameObject _damageFeedBack;

        public override Vector3Int[] Patterns => _pattern;
        [SerializeField] Vector3Int[] _pattern;

        [SerializeField] CKapaSupFields _cKapaSupFields;

        [SerializeField] CameraManager cam;
        #endregion

        #region inherited paterns/accessors
        //North tiles
        public override Vector3Int[] OddNTiles { get => _oddNTiles; protected set => _oddNTiles = value; }
        private Vector3Int[] _oddNTiles;
        public override Vector3Int[] EvenNTiles { get => _evenNTiles; protected set => _evenNTiles = value; }
        private Vector3Int[] _evenNTiles;

        //EN tiles
        public override Vector3Int[] OddENTiles { get => _oddENTiles; protected set => _oddENTiles = value; }
        private Vector3Int[] _oddENTiles;
        public override Vector3Int[] EvenENTiles { get => _evenENTiles; protected set => _evenENTiles = value; }
        private Vector3Int[] _evenENTiles;

        //ES tiles
        public override Vector3Int[] OddESTiles { get => _oddESTiles; protected set => _oddESTiles = value; }
        private Vector3Int[] _oddESTiles;
        public override Vector3Int[] EvenESTiles { get => _evenESTiles; protected set => _evenESTiles = value; }
        private Vector3Int[] _evenESTiles;

        //S tiles
        public override Vector3Int[] OddSTiles { get => _oddSTiles; protected set => _oddSTiles = value; }
        private Vector3Int[] _oddSTiles;
        public override Vector3Int[] EvenSTiles { get => _evenSTiles; protected set => _evenSTiles = value; }
        private Vector3Int[] _evenSTiles;

        //WS tiles
        public override Vector3Int[] OddWSTiles { get => _oddWSTiles; protected set => _oddWSTiles = value; }
        private Vector3Int[] _oddWSTiles;
        public override Vector3Int[] EvenWSTiles { get => _evenWSTiles; protected set => _evenWSTiles = value; }
        private Vector3Int[] _evenWSTiles;

        //WN tiles
        public override Vector3Int[] OddWNTiles { get => _oddWNTiles; protected set => _oddWNTiles = value; }
        private Vector3Int[] _oddWNTiles;
        public override Vector3Int[] EvenWNTiles { get => _evenWNTiles; protected set => _evenWNTiles = value; }
        private Vector3Int[] _evenWNTiles;
        #endregion

        #region fields
        [System.Serializable]
        public class CKapaSupFields
        {
            public int neededCompPoints;
            public int ultPointsAdded;
            public Animation animation;
        }
        #endregion

        #region inherited methodes
        public sealed override bool OnCheckKapaPoints(IUnit unit)
        {
            if (GameLoopManager.gLm.teamInventory.CompPoints[unit.TeamNumber] >= _cKapaSupFields.neededCompPoints) return true;
        
            RefuseKapa(); return false;
        }
        public sealed override List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IUnit unit)
        {
            if (EffectType != EffectType.Hacked)
            {
                return base.OnGenerateButton(hexGrid, unit);
            }
            unit.OnSelectNetworkTiles();
        
            return null;
        }

        public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
        {
            if (EffectType == EffectType.Hacked)
            {
                base.OnExecute(hexGrid, unit.GlobalNetwork, unit);
                unit.OnDeselectNetworkTiles();
            }
            else
            {
                base.OnExecute(hexGrid, pattern, unit);
            }

            DoKapa(unit);
            //Debug.Log(Description); //PlaceHolder à remplir avec les anims et considération de dégâts
            EndKapa();
        }
        #endregion

        #region cache
        void DoKapa(IUnit unit)
        {
            GameLoopManager.gLm.HandleCompPointValueChange(unit.TeamNumber, -_cKapaSupFields.neededCompPoints);
            unit.UltPoints += _cKapaSupFields.ultPointsAdded;
            //PlaceHolder à rempir avec les anims et considérations de dégâts

            unit.StatUI.SetUP(unit);
        }
        void RefuseKapa() { Debug.Log("nope"); }
        void EndKapa()
        {
            //Debug.Log("End Kapa");
        }
        #endregion
    }
}