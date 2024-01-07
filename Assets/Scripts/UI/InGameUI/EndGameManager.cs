using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.InGameUI
{
    public class EndGameManager : MonoBehaviour
    {
        public void StartNewGame()
        {
            SceneManager.LoadScene("Menu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}