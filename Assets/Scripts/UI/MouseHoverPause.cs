using UnityEngine;
using UnityEngine.UI;

public class MouseHoverPause : MonoBehaviour, IUIHelper
{
    [SerializeField] Button button;

    [SerializeField] Vector3 originsCale = new(1, 1, 1);

    [SerializeField] Vector3 scalecahnge = new(0.1f, 0.1f, 0.1f);

    [SerializeField] GameObject PlayerInfo;
    float counter = 0;
    bool isHovering;

    void Update()
    {
        if (!isHovering) return;
        counter += Time.deltaTime;

        if (counter >= 1) { OnPrint(); }
        else { OnHide(); }
    }

    public void OnPointerEnter()
    {
        isHovering = true;
        button.transform.localScale = originsCale + scalecahnge;
    }

    public void OnPointerExit()
    {
        isHovering = false;
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

    void OnPrint()
    {
        PlayerInfo.SetActive(true);
    }
    void OnHide()
    {
        PlayerInfo.SetActive(false);
    }
}