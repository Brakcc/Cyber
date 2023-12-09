using System.Collections.Generic;
using UnityEngine;

namespace Interfaces.FeedBacks
{
    public interface IVFXManager
    {
        public void OnEmitParts(List<Vector3Int> tilesList);
    }
}
