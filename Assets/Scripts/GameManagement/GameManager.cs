using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    #region fields
    //objets externes
    [SerializeField] private Volume generalVolume;
    private GameObject player;
    private Light2D lightPlayer;
    private Light2D globalLigth;
    private GameObject firstApp;
    private GameObject[] allChecks;

    private float cursor;

    //UIs
    [SerializeField] private Text totalTimeUI;
    [SerializeField] private Text currentRunTimeUI;

    //Gestion des démarages de clocks
    private bool canStartTotalClock = false;
    private bool canStartCurrentClock = false;

    //Saved Datas
    [HideInInspector] public Vector3 checkPoint;
    [HideInInspector] public int state;
    [HideInInspector] public int dash;
    [HideInInspector] public float totalPlayTime;
    [HideInInspector] public float currentRunTime;
    [HideInInspector] public float bestRunPlayTime;
    [HideInInspector] public int noCheckAchieve;
    [HideInInspector] public int hasGameStarted;
    [HideInInspector] public int musicID;
    [HideInInspector] public int lights;

    //Saved Modes
    [HideInInspector] public int totalClockOn;
    [HideInInspector] public int currentRunOn;
    [HideInInspector] public int NCOn;
    [HideInInspector] public int gameFinished;
    [HideInInspector] public float musicVol;
    [HideInInspector] public float sfxVol;
    

    public static GameManager gm;
    private void Awake()
    {
        LoadPlayer();
        LoadModes();
        gm = this;
        Cursor.visible = false;
    }
    #endregion

    #region Methodes
    private void Start()
    {
        LoadPlayer();
        LoadModes();
        InitsGameScene();
    }

    private void Update()
    {
        ManageTotalTime();
        ManageCurrentRunTime();
    }

    void InitsGameScene()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            firstApp = GameObject.Find("FirstApp");
            player = GameObject.FindGameObjectWithTag("Player");
            lightPlayer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Light2D>();
            globalLigth = GameObject.FindGameObjectWithTag("GLight").GetComponent<Light2D>();
            allChecks = GameObject.FindGameObjectsWithTag("CheckPoints");

            InitGame();

            canStartTotalClock = true;
            canStartCurrentClock = true;
        }
    }

    void InitGame()
    {
        //rdémarage du jeu après victoire
        if (gameFinished == 1)
        {
            hasGameStarted = 0;
            checkPoint = firstApp.transform.position;
            dash = 0;
            gameFinished = 0;
            player.transform.position = checkPoint;
            currentRunTime = 0f;
            lights = 0;
            generalVolume.weight = 0;
            //PlayerMovement.instance.isHardened = false;
            if (NCOn == 1)
            {
                NCOn = 0;
            }
        }
        else if (gameFinished == 0)
        {
            //redémarage en mode NC ou non
            if (NCOn == 1)
            {
                //NCStart();
            }
            else if (NCOn == 0)
            {
                player.transform.position = checkPoint;
                if (state == 1) { /*PlayerMovement.instance.isHardened = false;*/ }
                else if (state == 2) { /*PlayerMovement.instance.isHardened = true;*/ }
                if (lights == 0) { lightPlayer.intensity = 0; globalLigth.intensity = 0.9f; generalVolume.weight = 0; }
                else if (lights == 1) { lightPlayer.intensity = 0.9f; globalLigth.intensity = 0.2f; generalVolume.weight = 1; }

                //Ajouter la musique jouée
            }
        }
    }

    void ManageTotalTime()
    {
        if (canStartTotalClock)
        {
            totalPlayTime += Time.deltaTime;
            totalTimeUI.text = Fonctions.FloatToHourClock(totalPlayTime);
        }
    }

    void ManageCurrentRunTime()
    {
        if (canStartCurrentClock)
        {
            currentRunTime += Time.deltaTime;
            currentRunTimeUI.text = Fonctions.FloatToHourClock(currentRunTime);
        }
        if (Time.time >= cursor + 2f && gameFinished == 1)
        {
            canStartCurrentClock = false;
            cursor = Time.time;
            if (bestRunPlayTime <= 1)
            {
                bestRunPlayTime = currentRunTime;
                SaveAll();
            }
            if (currentRunTime < bestRunPlayTime)
            {
                bestRunPlayTime = currentRunTime;
                SaveAll();
            }
        }
    }
    #endregion

    #region Saves
    public void SaveAll()
    {
        SavePlayerData.SavePlayer(this);
        SavePlayerData.SaveModes(this);
    }

    private void OnApplicationQuit()
    {
        SaveAll();
    }
    #endregion

    #region Loadings
    public void LoadPlayer()
    {
        checkPoint = SavePlayerData.LoadPosition();
        state = SavePlayerData.LoadState();
        dash = SavePlayerData.LoadDash();
        totalPlayTime = SavePlayerData.LoadTotalTime();
        currentRunTime = SavePlayerData.LoadCurrentRunTime();
        bestRunPlayTime = SavePlayerData.LoadCurrentBestRunTime();
        noCheckAchieve = SavePlayerData.LoadAchievement();
        hasGameStarted = SavePlayerData.LoadGameStart();
        musicID = SavePlayerData.LoadIDMusic();
        lights = SavePlayerData.LoadLightState();
    }

    public void LoadModes()
    {
        totalClockOn = SavePlayerData.LoadTotalMode();
        currentRunOn = SavePlayerData.LoadCurrentMode();
        NCOn = SavePlayerData.LoadNCMode();
        gameFinished = SavePlayerData.LoadGameWon();
        musicVol = SavePlayerData.LoadMusicVolume();
        sfxVol = SavePlayerData.LoadSFXVolume();
    }
    #endregion
}
