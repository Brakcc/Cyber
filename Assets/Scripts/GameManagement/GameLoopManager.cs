using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLoopManager : MonoBehaviour
{
    #region fields
    #region team inits
    [Range(0, 1, order = 1)][SerializeField] int firstTeamPlaying;
    [SerializeField] GameObject DeActButton;
    int teamPlaying;
    public GameObject[] heroPlayer0;
    public GameObject[] heroPlayer1;
    int[] countPlayer = new int[2];
    #endregion

    #region team inventory
    GameObject[][] playerList = new GameObject[2][];
    public int[] CompPoints { get; set; }
    public int[] TurretNumber { get; set; }
    #endregion

    //general objectif
    public int ComputerNumber { get; set; }
    [SerializeField] int maxObjectif;

    //other fields
    [SerializeField] CameraMovement camM;
    [SerializeField] List<TMP_Text> cPUI;
    [SerializeField] List<TMP_Text> tNbUI;
    [SerializeField] Image[] computerUI;

    public static GameLoopManager gLM;
    #endregion

    #region methodes
    void Awake()
    {
        gLM = this;

        playerList = new[] { heroPlayer0, heroPlayer1 };
        foreach (var i in heroPlayer0) { i.GetComponent<Unit>().TeamNumber = 0; }
        foreach (var i in heroPlayer1) { i.GetComponent<Unit>().TeamNumber = 1; }
        countPlayer = new[] { playerList[0].Length, playerList[1].Length };
        CompPoints = new int[2] { 0, 0 };
        TurretNumber = new int[2] { 2, 2 };
        teamPlaying = firstTeamPlaying;
        foreach (var i in cPUI) { i.text = 0.ToString(); i.color = Color.red; }
        foreach (var i in tNbUI) { i.text = 2.ToString(); i.color = Color.green; }
        foreach (var i in computerUI) { i.color = Color.red; }
    }
    void Start()
    {
        InitTeam(teamPlaying);
    }

    /// <summary>
    /// action à réaliser dans un switch de team
    /// PLACE HOLDER POSSIBLE POUR UN MOUVEMENT DE CAM OU AUTRE FEEDBACK DE SWITCH
    /// </summary>
    /// <param name="newTeam"></param>
    void SwitchTeam(int newTeam) => InitTeam(newTeam);

    /// <summary>
    /// appelee à la fin d'une action de kapa d'une des Units.
    /// </summary>
    public void OnPlayerAction()
    {
        countPlayer[teamPlaying]--;
        if (countPlayer[teamPlaying] == 0)
        {
            SwitchTeam(teamPlaying == 1? 0 : 1);
        }
    }

    /// <summary>
    /// initialisation d'une équipe apres fin de tour d'une autre équipe 
    /// </summary>
    /// <param name="i"></param>
    void InitTeam(int i)
    {
        teamPlaying = i;
        countPlayer[i] = playerList[i].Length;
        foreach (var player in playerList[i])
        {
            var u = player.GetComponent<Unit>();
            if (u.IsDead) { countPlayer[i]--;  continue; }

            u.CanPlay = true;
        }

        if (i == 1) DeActButton.SetActive(true);
        else DeActButton.SetActive(false);

        camM.OnFollowPlayer(playerList[teamPlaying][0].GetComponent<Unit>());
    }

    public void HandleCompPointValueChange(int teamNb, int pC)
    {
        CompPoints[teamNb] += pC;
        cPUI[teamNb].text = CompPoints[teamNb].ToString();
        if (CompPoints[teamNb] > 0) { cPUI[teamNb].color = Color.green; }
    }

    public void HandleTurretUse(int teamNb)
    {
        TurretNumber[teamNb]--;
        tNbUI[teamNb].text = TurretNumber[teamNb].ToString();
        if (TurretNumber[teamNb] == 0) { tNbUI[teamNb].color = Color.red; }
    }

    public void HandleComputerValueChange()
    {
        ComputerNumber++;
        computerUI[ComputerNumber - 1].color = Color.green;
        //ajouter la fin du jeu d'urgence /!\
        if (ComputerNumber == maxObjectif) { Time.timeScale = 0; }
    }
    #endregion
}