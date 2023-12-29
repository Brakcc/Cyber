using UnityEngine;

namespace GameContent.GridManagement.GridGraphManagement.GraphInits
{
    [System.Serializable]
    public class GraphInitBoard
    {
        #region fields
        
        [SerializeField] private int orderInLayer;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Sprite switchSprite;
        [SerializeField] private Material newMat;
        
        #endregion

        #region cached methodes
        
        public void SetRenderer(GameObject parent)
        {
            var rend = parent.GetComponentInChildren<SpriteRenderer>();

            rend.sortingOrder = orderInLayer;

            var transRend = rend.transform;
            
            transRend.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
            transRend.localEulerAngles = new Vector3(-45, 0);
            transRend.localPosition = new Vector3(offset.x, offset.y, -rend.size.y / 2 + 0.2f);
        }

        public void HandleDeAct(GameObject parent, bool actAnim)
        {
            parent.GetComponent<Animator>().enabled = actAnim;
            parent.GetComponentInChildren<SpriteRenderer>().material = newMat;
            parent.GetComponentInChildren<SpriteRenderer>().sprite = switchSprite;
        }
        
        #endregion
    }
}