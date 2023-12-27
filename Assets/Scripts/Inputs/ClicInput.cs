using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class ClicInput : MonoBehaviour
    {
        public UnityEvent<Vector3> mouseClic;
        [SerializeField] private InputActionReference clic;

        private void Update()
        {
            DetectClic();
        }

        private void DetectClic()
        {
            if (!clic.action.WasPerformedThisFrame())
                return;
            
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mouseClic?.Invoke(mousePos);
        }
    }
}


