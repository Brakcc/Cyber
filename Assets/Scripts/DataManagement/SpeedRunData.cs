using UnityEngine;

[System.Serializable]
public class SpeedRunData
{
    public int totalTimeClockOn;
    public int currentRunClockOn;
    public int NCModOn;
    public int wasGameWon;
    public float musicVol;
    public float sfxVol;

    public SpeedRunData(GameManager gm)
    {
        totalTimeClockOn = gm.totalClockOn;
        currentRunClockOn = gm.currentRunOn;
        NCModOn = gm.NCOn;
        wasGameWon = gm.gameFinished;
        musicVol = gm.musicVol;
        sfxVol = gm.sfxVol;
    }
}