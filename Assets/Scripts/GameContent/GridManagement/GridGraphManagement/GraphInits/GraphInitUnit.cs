using UnityEngine;

namespace GameContent.GridManagement.GridGraphManagement.GraphInits
{
    [System.Serializable]
    public class GraphInitUnit
    {
        #region fields

        [SerializeField] private int orderInLayer;
        [SerializeField] private Color color;
        [SerializeField] private Vector2 offset;

        #endregion

        #region cached methodes

        public GameObject SetRenderer(GameObject parent, Sprite sprite)
        {
            GameObject child = new("_tex");
            child.transform.SetParent(parent.transform);
            var rend = child.AddComponent<SpriteRenderer>();

            rend.sprite = sprite;
            rend.sortingOrder = orderInLayer;
            rend.color = color;

            child.transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
            child.transform.localEulerAngles = new Vector3(-45, 0);
            child.transform.localPosition = new Vector3(offset.x, offset.y, -rend.size.y / 2);
            return child;
        }

        #endregion
    }
}