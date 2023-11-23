using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    #region fields
    [Range(0, 1, order = 1)][SerializeField] int firstTeamPlaying;
    int teamPlaying;
    [SerializeField] GameObject[] heroPlayer1;
    [SerializeField] GameObject[] heroPlayer2;

    GameObject[][] playerList = new GameObject[2][];

    int[] countPlayer = new int[2];

    [SerializeField] CameraMovement camM;

    public static GameLoopManager gLM;
    #endregion

    #region methodes
    void Awake()
    {
        gLM = this;

        playerList = new[] { heroPlayer1, heroPlayer2 };
        countPlayer = new[] { playerList[0].Length, playerList[1].Length };
        teamPlaying = firstTeamPlaying;
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

        camM.OnFollowPlayer(playerList[teamPlaying][0].GetComponent<Unit>());
    }
    #endregion
}