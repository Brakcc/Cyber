using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class ClicInput : MonoBehaviour
    {
        public UnityEvent<Vector3> mouseClic;
        [SerializeField] InputActionReference clic;

        void Update()
        {
            DetectClic();
        }

        public void DetectClic()
        {
            if (clic.action.WasPerformedThisFrame())
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                mouseClic?.Invoke(mousePos);
            }
        }
    }
}


