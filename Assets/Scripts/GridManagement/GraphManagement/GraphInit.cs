using UnityEngine;

[System.Serializable]
public class GraphInitBoard
{
    #region fields
    [SerializeField] int orderInLayer;
    [SerializeField] Vector2 offset;
    #endregion

    #region cached methodes
    public void SetRenderer(GameObject parent)
    {
        SpriteRenderer rend = parent.GetComponentInChildren<SpriteRenderer>();

        rend.sortingOrder = orderInLayer;

        rend.transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
        rend.transform.localEulerAngles = new(-45, 0);
        rend.transform.localPosition = new Vector3(offset.x, offset.y, -rend.size.y / 2 + 0.2f);
    }
    #endregion
}

    [System.Serializable]
public class GraphInitUnit
{
    #region fields
    [SerializeField] int orderInLayer;
    [SerializeField] Color color;
    [SerializeField] Vector2 offset;
    #endregion

    #region cached methodes
    public GameObject SetRenderer(GameObject parent, Sprite sprite)
    {
        GameObject child = new("_tex");
        child.transform.SetParent(parent.transform);
        SpriteRenderer rend = child.AddComponent<SpriteRenderer>();

        rend.sprite = sprite;
        rend.sortingOrder = orderInLayer;
        rend.color = color;

        child.transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
        child.transform.localEulerAngles = new(-45, 0);
        child.transform.localPosition = new Vector3(offset.x, offset.y, -rend.size.y / 2);
        return child;
    }
    #endregion
}

[System.Serializable]
public class GraphInitEntity
{
    #region fields
    [SerializeField] int orderInLayer;
    #endregion

    #region cached methodes
    public void SetRenderer(GameObject parent)
    {
        SpriteRenderer child = parent.GetComponentInChildren<SpriteRenderer>();

        //rend.sprite = sprite;
        //rend.sortingOrder = orderInLayer;
        //rend.color = color;

        child.sortingOrder = orderInLayer;
        child.transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
        child.transform.localEulerAngles = new(-45, 0);
        child.transform.localPosition = new Vector3(0, 0.6f, -child.size.y / 2);
    }
    #endregion
}
