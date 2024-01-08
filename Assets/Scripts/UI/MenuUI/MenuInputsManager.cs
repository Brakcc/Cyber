using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.MenuUI
{
    public class MenuInputsManager : MonoBehaviour
    {
        public void OnEscape(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
                SceneManager.LoadScene("StartScene");
        }
    }
}