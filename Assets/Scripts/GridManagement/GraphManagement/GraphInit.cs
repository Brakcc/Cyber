using UnityEngine;

[System.Serializable]
public class GraphInitBoard
{
    #region fields
    [SerializeField] Sprite sprite;
    [SerializeField] int orderInLayer;
    [SerializeField] Color color;
    #endregion

    #region cached methodes
    public GameObject SetRenderer(GameObject parent)
    {
        GameObject child = new("_tex");
        child.transform.SetParent(parent.transform);
        SpriteRenderer rend = child.AddComponent<SpriteRenderer>();

        rend.sprite = sprite;
        rend.sortingOrder = orderInLayer;
        rend.color = color;

        child.transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
        child.transform.localEulerAngles = new(-45, 0);
        child.transform.localPosition = new Vector3(0, 0, -rend.size.y/2);
        return child;
    }
    #endregion
}

[System.Serializable]
public class GraphInitUnit
{
    #region fields
    [SerializeField] int orderInLayer;
    [SerializeField] Color color;
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
        child.transform.localPosition = new Vector3(0, 0, -rend.size.y / 2);
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

        child.transform.localScale = new Vector3(1, Mathf.Sqrt(2), 1);
        child.transform.localEulerAngles = new(-45, 0);
        child.transform.localPosition = new Vector3(0, 0.6f, -child.size.y / 2);
    }
    #endregion
}
