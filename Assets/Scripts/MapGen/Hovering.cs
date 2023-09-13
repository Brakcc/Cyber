using UnityEngine;

public class Hovering : MonoBehaviour
{
    #region fields
    public int G;
    public int H;
    public int F { get { return G + H; } }

    [HideInInspector] public bool isBlocked;
    [HideInInspector] public Hovering previous;

    public Vector3Int gridLoaction;
    #endregion

    #region methodes
    private void Start()
    {
        HideHover();    
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideHover();
        }
    }

    public void ShowHover()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.12945f, 0, 0.25f);
    }
    public void HideHover()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.12945f, 0, 0);
    }
    #endregion
}
