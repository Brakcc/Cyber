using System.Collections.Generic;
using CameraManagement;
using FeedBacks;
using GameContent.GridManagement;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.DifKapas
{
    [CreateAssetMenu(fileName = "Ultimate Kapa", menuName = "Tactical/Kapas/Ultimate")]
    public class UltimateKapaSO : AbstractKapaSO
    {
        #region inherited accessors
        
        public override Vector3Int[] Patterns => _patterns;
        [SerializeField] private Vector3Int[] _patterns;
        
        [SerializeField] private UKapaSupFields _uKapaSupFields;
        
        public override GameObject DamageFeedBack => _damageFeedBack;
        [SerializeField] private GameObject _damageFeedBack;
        
        protected override VFXManager VFx => vFx;
        [SerializeField] private VFXManager vFx;

        [SerializeField] private CameraManager _cam;
        #endregion

        #region inherited paterns/accessors
        
        //North tiles
        public override Vector3Int[] OddNTiles { get => _oddNTiles; protected set => _oddNTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _oddNTiles;
        public override Vector3Int[] EvenNTiles { get => _evenNTiles; protected set => _evenNTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _evenNTiles;

        //EN tiles
        public override Vector3Int[] OddENTiles { get => _oddENTiles; protected set => _oddENTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _oddENTiles;
        public override Vector3Int[] EvenENTiles { get => _evenENTiles; protected set => _evenENTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _evenENTiles;

        //ES tiles
        public override Vector3Int[] OddESTiles { get => _oddESTiles; protected set => _oddESTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _oddESTiles;
        public override Vector3Int[] EvenESTiles { get => _evenESTiles; protected set => _evenESTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _evenESTiles;

        //S tiles
        public override Vector3Int[] OddSTiles { get => _oddSTiles; protected set => _oddSTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _oddSTiles;
        public override Vector3Int[] EvenSTiles { get => _evenSTiles; protected set => _evenSTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _evenSTiles;

        //WS tiles
        public override Vector3Int[] OddWSTiles { get => _oddWSTiles; protected set => _oddWSTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _oddWSTiles;
        public override Vector3Int[] EvenWSTiles { get => _evenWSTiles; protected set => _evenWSTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _evenWSTiles;

        //WN tiles
        public override Vector3Int[] OddWNTiles { get => _oddWNTiles; protected set => _oddWNTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _oddWNTiles;
        public override Vector3Int[] EvenWNTiles { get => _evenWNTiles; protected set => _evenWNTiles = value; }
        [HideInInspector][SerializeField] private Vector3Int[] _evenWNTiles;
        
        #endregion

        #region fields
        [System.Serializable]
        public class UKapaSupFields
        {
            public int neededUltPoints;
            public Animation animation;
        }
        #endregion

        #region inherited methodes
        public sealed override bool OnCheckKapaPoints(IUnit unit)
        {
            if (unit.UltPoints >= _uKapaSupFields.neededUltPoints) return true;
        
            RefuseKapa(); return false;
        }

        public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit, bool fromUnit, out bool isHitting)
        {
            base.OnExecute(hexGrid, pattern, unit, fromUnit, out isHitting);
            DoKapa(unit);
            //Debug.Log(Description); //PlaceHolder � remplir avec les anims et consid�ration de d�g�ts
            EndKapa();
        }
        #endregion

        #region cache

        private void DoKapa(IUnit unit)
        {
            unit.UltPoints -= _uKapaSupFields.neededUltPoints;
            //PlaceHolder � rempir avec les anims et consid�rations de d�g�ts

            unit.StatUI.SetUP(unit);
        }

        private void RefuseKapa() { Debug.Log("nope"); }

        private void EndKapa()
        {
            //Debug.Log("End Kapa");
        }
        #endregion
    }
}
