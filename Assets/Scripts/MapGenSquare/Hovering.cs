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

    [SerializeField] private Color hoverColor;
    [SerializeField] private Color baseColor;
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
        gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
    }
    public void HideHover()
    {
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
    }
    #endregion
}
