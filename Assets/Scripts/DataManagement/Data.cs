using UnityEngine;

[System.Serializable]
public class Data
{
    public Vector3 lastPos;
    public int stateP;
    public int dashUnlock;
    public float totalTime;
    public float currentRunTime;
    public float bestRunTime;
    public int achieve;
    public int gameStart;
    public int idMusic;
    public int lightState;

    public Data(GameManager gm)
    {
        lastPos = gm.checkPoint;
        stateP = gm.state;
        dashUnlock = gm.dash;
        totalTime = gm.totalPlayTime;
        currentRunTime = gm.currentRunTime;
        bestRunTime = gm.bestRunPlayTime;
        achieve = gm.noCheckAchieve;
        gameStart = gm.hasGameStarted;
        idMusic = gm.musicID;
        lightState = gm.lights;
    }
}