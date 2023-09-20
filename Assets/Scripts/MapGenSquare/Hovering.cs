using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    #region fields
    public float G;
    public float H;
    public void SetG(float g) => G = g;
    public void SetH(float h) => H = h;
    public float F { get { return G + H; } }

    [HideInInspector] public bool isBlocked;
    [HideInInspector] public Hovering previous;
    [HideInInspector] public List<Hovering> Neighbors {  get; private set; }

    public Vector3Int gridLoaction;

    [SerializeField] private Color hoverColor;
    [SerializeField] private Color baseColor;
    #endregion

    #region methodes
    private void Start()
    {
        HideHover();
    }

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HideHover();
        }
    }*/

    public void ShowHover()
    {
        gameObject.GetComponent<SpriteRenderer>().color = hoverColor;
    }
    public void HideHover()
    {
        gameObject.GetComponent<SpriteRenderer>().color = baseColor;
    }

    public void  SetConnection(Hovering hovering) => previous = hovering;
    #endregion
}
