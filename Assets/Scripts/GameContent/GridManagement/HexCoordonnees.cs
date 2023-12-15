using Interfaces.Grid;
using UnityEngine;

namespace GameContent.GridManagement
{
    public struct HexCoordonnees : IHexCoord
    {
        #region fields
        public Vector3Int OffsetCoordonnees { get; set; }
        #endregion

        #region constructeur
        /// <summary>
        /// Donne les coordonn�es en int d'une tuile dans la grid 
        /// </summary>
        /// <param name="gameObject"></param>
        public HexCoordonnees (GameObject gameObject)
        {
            OffsetCoordonnees = GetClosestHex(gameObject.transform.position);
        }
        #endregion

        #region methodes
        public static Vector3Int GetClosestHex(Vector3 worldPos)
        {
            var temp = worldPos.x / 0.05f / 19;
            var x = Mathf.RoundToInt(temp);
            var y = worldPos.y >= 0 ? Mathf.CeilToInt(worldPos.y) : Mathf.FloorToInt(worldPos.y);
            worldPos.z = 0;
            var z = (int)worldPos.z;
            return new Vector3Int(x, y, z);
        }
        #endregion
    }
}
