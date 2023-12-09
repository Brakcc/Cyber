using UnityEngine;

namespace Interfaces.Grid
{
    public interface IHexCoord
    {
        public Vector3Int OffsetCoordonnees { get; set; }
    }
}