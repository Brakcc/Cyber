using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    #region fields
    [SerializeField] int teamPlaying;
    [SerializeField] List<GameObject> heroPlayer1 = new();
    [SerializeField] List<GameObject> heroPlayer2 = new();

    List<List<GameObject>> playerList = new();

    [SerializeField] List<int> countPlayer = new();

    public static GameLoopManager glManager;
    #endregion

    #region methodes
    void Awake()
    {
        glManager = this;
        playerList = new() { heroPlayer1, heroPlayer2 };
        countPlayer = new() { playerList[0].Count, playerList[1].Count };
        teamPlaying = 0;
    }
    void Start()
    {
        InitTeam(teamPlaying);
        UnTeam(teamPlaying == 0? 1 : 0);
    }

    void SwitchTeam(int newTeam) => InitTeam(newTeam);

    public void OnPlayerAction()
    {
        countPlayer[teamPlaying]--;
        if (countPlayer[teamPlaying] == 0)
        {
            SwitchTeam(teamPlaying == 1? 0 : 1);
        }
    }

    void InitTeam(int i)
    {
        teamPlaying = i;
        countPlayer[i] = 4;
        foreach (var player in playerList[i])
        {
            var u = player.GetComponent<Unit>();
            if (u.IsDead) { countPlayer[i]--;  continue; }

            u.CanPlay = true;
        }
    }

    void UnTeam(int i)
    {
        foreach (var player in playerList[i])
        {
            var u = player.GetComponent<Unit>();
            u.CanPlay = false;
            countPlayer[i]--;
        }
    }
    #endregion
}