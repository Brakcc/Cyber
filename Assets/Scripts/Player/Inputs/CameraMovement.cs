using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    #region fields
    Camera cam;
    Vector3 origingPos;
    Vector3 diff;
    bool isDragging;
    #endregion

    #region methodes
    void Awake()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (!isDragging) return;

        diff = GetMousePosition() - transform.position;
        transform.position = origingPos - diff;
    }

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { origingPos = GetMousePosition(); }
        isDragging = ctx.started || ctx.performed;
    }
    Vector3 GetMousePosition() => cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    #endregion
}