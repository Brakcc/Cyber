using UnityEngine;
using UnityEngine.UI;

public class MouseHoverPause : MonoBehaviour, IUIHelper
{
    public Button button;
    public bool isSized = false;

    public Vector3 originsCale = new(1, 1, 1);

    public Vector3 scalecahnge = new(0.1f, 0.1f, 0.1f);

    public void OnPointerEnter()
    {
        button.transform.localScale = originsCale + scalecahnge;
        isSized = true;
    }

    public void OnPointerExit()
    {
        button.transform.localScale = originsCale;
        isSized = false;
    }

    public void OnEnable()
    {
        button.transform.localScale = originsCale;
    }
    public void OnDisable()
    {
        isSized = false;
        button.transform.localScale = originsCale;
    }
}