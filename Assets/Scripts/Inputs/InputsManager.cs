using UnityEngine;

namespace Inputs
{
    public class InputsManager
    {
        /*public RaycastHit2D? GetFocusedOnTile(LayerMask l, Camera cam,Vector3 mousePos)
        {
            var mousePos2D = new Vector2(mousePos.x, mousePos.y);

            var hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero, l);

            if (hits.Length > 0)
            {
                return hits.OrderByDescending(i => i.collider.transform.position.z).First();
            }
            else return null;
        }*/

        public static bool FocV2(LayerMask l, Camera cam, Vector3 mousePos, out GameObject result)
        {
            var ray = cam.ScreenPointToRay(mousePos);
            if (Physics2D.Raycast(ray.origin, ray.direction, 100, l))
            {
                result = Physics2D.Raycast(ray.origin, ray.direction, 100, l).collider.gameObject;
                return true;
            }
            result = null;
            return false;
        }
    }
}
