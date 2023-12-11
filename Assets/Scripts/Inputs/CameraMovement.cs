﻿using System.Threading.Tasks;
using CameraManagement;
using Cinemachine;
using GameContent.Entity.Unit.UnitWorking;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class CameraMovement : MonoBehaviour
    {
        #region fields

        #region DragParams

        //Drag Params
        Camera cam;
        Vector3 origingPos;
        Vector3 diff;
        bool isDragging;
        
        //AutoMove Params
        bool isMoving;
        Unit temp;
        bool canClicSwitch;

        #endregion

        //camMovement avec la GameLoop
        [SerializeField] CameraManager camManager;
        [SerializeField] int switchDelay;

        #region ZoomParams

        [SerializeField] CamZoomMove camZoomMove;
                
        [System.Serializable]
        public class CamZoomMove 
        {
            public CinemachineVirtualCamera vCam;
            [HideInInspector] public float zoomValue;
            public float zoomCoef;
            public float maxOrthoSize; 
            public float minOrthoSize; 
            public float offSet;
        }

        #endregion
        
        
        #endregion

        #region methodes
        
        void Awake() => cam = Camera.main;

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
        
        #region Zoom

        public void ZoomMove(InputAction.CallbackContext ctx)
        {
            var s = ctx.ReadValue<Vector2>().y;
                    
            camZoomMove.zoomValue -= s * camZoomMove.zoomCoef;
            camZoomMove.zoomValue = Mathf.Clamp(camZoomMove.zoomValue, camZoomMove.minOrthoSize, camZoomMove.maxOrthoSize);
                    
            camZoomMove.vCam.m_Lens.OrthographicSize = Mathf.Lerp(camZoomMove.vCam.m_Lens.OrthographicSize,
                camZoomMove.zoomValue, Time.deltaTime * camZoomMove.offSet);
        }

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