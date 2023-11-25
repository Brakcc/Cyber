using UnityEngine;
using UnityEngine.UI;

public class MouseHoverPause : MonoBehaviour, IUIHelper
{
    [SerializeField] Button button;

    [SerializeField] Vector3 originsCale = new(1, 1, 1);

    [SerializeField] Vector3 scalecahnge = new(0.1f, 0.1f, 0.1f);

    public void OnPointerEnter()
    {
        button.transform.localScale = originsCale + scalecahnge;
    }

    public void OnPointerExit()
    {
        button.transform.localScale = originsCale;
    }

    public void OnEnable()
    {

        button.transform.localScale = originsCale;
    }
    public void OnDisable()
    {
        button.transform.localScale = originsCale;
    }
}