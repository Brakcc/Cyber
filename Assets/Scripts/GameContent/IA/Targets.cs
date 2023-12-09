using UnityEngine;

namespace GameContent.IA
{
    public class Targets : MonoBehaviour
    {
        public Vector3 Position;

        private void Start()
        {
            transform.position = new Vector3(Position.x,Position.y, Position.z);
            IAManager.instance.Choose(Position);
        }
    }
}
