using UnityEngine;
using UnityEngine.Events;

public class ClicInput : MonoBehaviour
{
    public UnityEvent<Vector3> mouseClic;

    void Update()
    {
        DetectClic();
    }

    public void DetectClic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mouseClic?.Invoke(mousePos);
        }
    }
}


