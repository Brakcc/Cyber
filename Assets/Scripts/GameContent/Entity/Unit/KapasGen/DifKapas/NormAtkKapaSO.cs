using System.Collections.Generic;
using CameraManagement;
using Cinemachine;
using GameContent.GameManagement;
using GameContent.GridManagement;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.KapasGen.DifKapas
{
    [CreateAssetMenu(fileName = "Normal Attack Kapa", menuName = "Tactical/Kapas/Normal Attack")]
    public class NormAtkKapaSO : AbstractKapaSO
    {
        #region inherited accessors
        
        public override KapaUISO KapaUI => _kapaUI;
        [SerializeField] KapaUISO _kapaUI;
        public override GameObject DamageFeedBack => _damageFeedBack;
        [SerializeField] GameObject _damageFeedBack;
        public override Vector3Int[] Patterns => _pattern;
        [SerializeField] Vector3Int[] _pattern;

        [SerializeField] NAKapaSupFields _nAKapaSupFields;

        [SerializeField] CameraManager _cam;
        
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
        public class NAKapaSupFields
        {
            public int compPointsAdded;
            public int ultPointsAdded;
            public Animation animation;
        }
        #endregion

        #region inherited methodes
        public sealed override bool OnCheckKapaPoints(IUnit unit) => true;

        public sealed override void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
        {
            base.OnExecute(hexGrid, pattern, unit, out bool isHitting);
            DoKapa(unit, isHitting);
            //PlaceHolder � remplir avec les anims et consid�ration de d�g�ts
            EndKapa();
        }
        #endregion

        #region cache
        void DoKapa(IUnit unit, bool hit)
        {
            if (hit)
            {
                GameLoopManager.gLm.HandleCompPointValueChange(unit.TeamNumber, _nAKapaSupFields.compPointsAdded);
            }

            CameraFunctions.OnShake(FindObjectOfType<CinemachineVirtualCamera>(), _cam.shake);
        }
        void EndKapa()
        {
            //Debug.Log("End Kapa");
        }
        #endregion
    }
}
