using System.Threading.Tasks;
using CameraManagement;
using GameContent.Entity.Unit.UnitWorking;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class CameraMovement : MonoBehaviour
    {
        #region fields
        //Drag Params
        Camera cam;
        Vector3 origingPos;
        Vector3 diff;
        bool isDragging;

        //AutoMove Params
        bool isMoving;
        Unit temp;
        bool canClicSwitch;

        //camMovement avec la GameLoop
        [SerializeField] CameraManager camManager;
        [SerializeField] int switchDelay;
        #endregion

        #region methodes
        void Awake()
        {
            cam = Camera.main;
        }

        void LateUpdate()
        {
            OnKeepFollow();
            OnMouseMove();
        }

        #region MouseMove
        void OnMouseMove()
        {
            if (!isDragging || isMoving) return;

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

        #region SwitchMove
        public async void OnFollowPlayer(Unit unit)
        {
            canClicSwitch = false;
            await Task.Delay(switchDelay);
            isMoving = true;
            temp = unit;
            canClicSwitch = true;
        }

        public void OnFocusPlayer(Unit unit)
        {
            if (!canClicSwitch) return;
            isMoving = true;
            temp = unit;
        }

        void OnKeepFollow()
        {
            if (isMoving && isDragging) isMoving = false;
            if (!isMoving) return;

            if (Vector2.Distance(new Vector2(temp.CurrentHexPos.x , temp.CurrentHexPos.y), transform.position) < 0.1f) isMoving = false;
            CameraFunctions.OnFocus(temp, transform, camManager.focus);
        }
        #endregion
        #endregion
    }
}