using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MenuUI
{
    public class StartSceneManager : MonoBehaviour
    {
        public void OnLoadScene(string arg)
        {
            SceneManager.LoadScene(arg);
        }

        public void OnQuiGame()
        {
            Application.Quit();
        }
    }
}