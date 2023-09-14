using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] private InputActionReference pause;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject SRSettingsUI;
    [SerializeField] private GameObject SettingsUI;

    void Start()
    {
        SettingsUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || pause.action.WasPressedThisFrame())
        {
            if (gameIsPaused)
            {
                Resume();
                SettingsUI.SetActive(false);
                SRSettingsUI.SetActive(false);
            }
            else
            {
                Paused();
            }
        }
    }

    public void SettingsButton()
    {
        SettingsUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void CloseSettings()
    {
        SettingsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Paused()
    {
        //PlayerMovement.instance.enabled = false;
        pauseMenuUI.SetActive(true);

        Cursor.visible = true;

        gameIsPaused = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        //PlayerMovement.instance.enabled = true;
        pauseMenuUI.SetActive(false);

        Cursor.visible = false;

        gameIsPaused = false;
        Time.timeScale = 1; 
    }

    public void OpenSpeedRun()
    {
        SRSettingsUI.SetActive(true);
        SettingsUI.SetActive(false);
    }
    
    public void CloseSpeedRun()
    {
        SRSettingsUI.SetActive(false);
        SettingsUI.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Resume();
        GameManager.gm.SaveAll();
        SceneManager.LoadScene("Menu");
    }
}
