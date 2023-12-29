using UnityEngine;

namespace GameContent.GridManagement.GridGraphManagement.GraphInits
{
    [System.Serializable]
    public class GraphInitEntity
    {
        #region fields
        
        [SerializeField] private int orderInLayer;
        
        #endregion

        #region cached methodes
        
        public void SetRenderer(GameObject parent)
        {
            var child = parent.GetComponentInChildren<SpriteRenderer>();

            //rend.sprite = sprite;
            //rend.sortingOrder = orderInLayer;
            //rend.color = color;

            child.sortingOrder = orderInLayer;
            Transform transform;
            (transform = child.transform).localScale = new Vector3(1, Mathf.Sqrt(2), 1);
            transform.localEulerAngles = new Vector3(-45, 0);
            transform.localPosition = new Vector3(0, 0.6f, -child.size.y / 2);
        }
        
        #endregion
    }
}