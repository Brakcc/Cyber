using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.InGameUI
{
    public class PauseMenuManager : MonoBehaviour
    {
        #region fields

        [SerializeField] private GameObject pauseMenuUI;

        #endregion

        #region methodes

        private void Start()
        {
            pauseMenuUI.SetActive(false);
        }

        public void OnEnableMenu(InputAction.CallbackContext ctx)
        {
            if (!ctx.started)
                return;
            
            pauseMenuUI.SetActive(true);
            GetComponent<PlayerInput>().enabled = false;
            Time.timeScale = 0;
        }

        public void OnResume()
        {
            pauseMenuUI.SetActive(false);
            GetComponent<PlayerInput>().enabled = true;
            Time.timeScale = 1;
        }

        public void OnReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        
        #endregion
    }
}