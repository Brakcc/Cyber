using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuStart : MonoBehaviour
{
    [SerializeField] private GameObject settingswindow;
    [SerializeField] private GameObject menuwindow;
    [SerializeField] private GameObject quitwindow;

    [SerializeField] private TMP_Text bestTime;
    [SerializeField] private GameObject tampon;

    void Start()
    {
        string path1 = Application.persistentDataPath + "/datas.data";
        string path2 = Application.persistentDataPath + "/modes.modes";
        if (!File.Exists(path1))
        {
            GameManager.gm.SaveAll();
            ResetGame();
        }
        Cursor.visible = true;
        settingswindow.SetActive(false);
        InitsUI();
        StartCoroutine(StartTheGame());
    }

    void InitsUI()
    {
        if (SavePlayerData.LoadCurrentBestRunTime() == 0f)
        {
            bestTime.text = "No Run Finished Yet";
        }
        else
        {
            bestTime.text = "Best Run Time \n\r" + Fonctions.FloatToHourClock(SavePlayerData.LoadCurrentBestRunTime());
        }

        if (SavePlayerData.LoadAchievement() == 1)
        {
            tampon.SetActive(true);
        }
    }

    IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(3f);
        menuwindow.SetActive(true);
    }

    #region buttons methodes
    public void StartButton()
    {
        GameManager.gm.SaveAll();
        SceneManager.LoadScene(1);
    }

    public void OpenSettingsButton()
    {
        menuwindow.SetActive(false);
        settingswindow.SetActive(true);
    }

    public void CloseSettingsButton()
    {
        menuwindow.SetActive(true);
        settingswindow.SetActive(false);
    }

    public void ResetGame()
    {
        GameManager.gm.dash = 0;
        GameManager.gm.checkPoint = new Vector3(-691.6f, 186.8f, 0);
        GameManager.gm.currentRunTime = 0f;
        GameManager.gm.NCOn = 0;
        GameManager.gm.state = 1;
        GameManager.gm.hasGameStarted = 0;
        GameManager.gm.musicID = 0;
        GameManager.gm.lights = 0;
        GameManager.gm.SaveAll();
        CloseSettingsButton();
    }

    public void wantToQuit()
    {
        menuwindow.SetActive(false);
        quitwindow.SetActive(true);
    }

    public void NotQuit()
    {
        menuwindow.SetActive(true);
        quitwindow.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    #endregion
}
