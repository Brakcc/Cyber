using UnityEngine;
using System.IO;

public static partial class SavePlayerData
{
    public static void SavePlayer(GameManager gm)
    {
        string path = Application.persistentDataPath + "/datas.data";
        Debug.Log("save data");
        Data lastData = new (gm);
        File.WriteAllText(path, lastData.lastPos.ToString() + ";" 
            + lastData.stateP.ToString() + ";"
            + lastData.dashUnlock.ToString() + ";"
            + lastData.totalTime.ToString() + ";"
            + lastData.currentRunTime.ToString() + ";"
            + lastData.bestRunTime.ToString() + ";"
            + lastData.achieve.ToString() + ";"
            + lastData.gameStart.ToString() + ";"
            + lastData.idMusic.ToString() + ";"
            + lastData.lightState.ToString());
    }

    public static string[] LoadPlayer()
    {
        string path = $"{Application.persistentDataPath}/datas.data";
        if (File.Exists(path))
        {
            string playerData;
            string[] datasString;
            playerData = File.ReadAllText(path);
            datasString = Fonctions.UnpackData(playerData);
            //Debug.Log("player Position : " + datasString[0] + "player State : " + datasString[1] + "total Time Played" + datasString[2] + "current Run Time Played : " + datasString[3] + "done without check : " + datasString[4]);
            return datasString;
        }
        else { return null; }
    }

    #region LoadData
    public static Vector3 LoadPosition()
    {
        if (LoadPlayer() != null)
        {
            string playerPos = LoadPlayer()[0];
            Vector3 posData = Fonctions.StringToVector3(playerPos);
            return posData;
        }
        else
        {
            return new Vector3(-691.6f, 186.8f, 0);
        }
    }

    public static int LoadState()
    {
        if (LoadPlayer() != null)
        {
            string mode = LoadPlayer()[1];
            int modeData = int.Parse(mode);
            return modeData;
        }
        else { return 1; }
    }

    public static int LoadDash()
    {
        if (LoadPlayer() != null)
        {
            string dash = LoadPlayer()[2];
            int dashData = int.Parse(dash);
            return dashData;
        }
        else { return 0; }
    }

    public static float LoadTotalTime()
    {
        if (LoadPlayer() != null)
        {
            string totalTime = LoadPlayer()[3];
            float totalTimeData = float.Parse(totalTime);
            return totalTimeData;
        }
        else { return 0f; }
    }

    public static float LoadCurrentRunTime()
    {
        if (LoadPlayer() != null)
        {
            string currenRunTime = LoadPlayer()[4];
            float currentRunTimeData = float.Parse(currenRunTime);
            return currentRunTimeData;
        }
        else { return 0f; }
    }

    public static float LoadCurrentBestRunTime()
    {
        if (LoadPlayer() != null)
        {
            string currentBestRunTime = LoadPlayer()[5];
            float currentBestRunTimeData = float.Parse(currentBestRunTime);
            return currentBestRunTimeData;
        }
        else { return 0f; }
    }

    public static int LoadAchievement()
    {
        if (LoadPlayer() != null)
        {
            string achieve = LoadPlayer()[6];
            int achieveData = int.Parse(achieve);
            return achieveData;
        }
        else { return 0; }
    }

    public static int LoadGameStart()
    {
        if (LoadPlayer() != null)
        {
            string laodGameStart = LoadPlayer()[7];
            int laodGameStartData = int.Parse(laodGameStart);
            return laodGameStartData;
        }
        else { return 0; }
    }

    public static int LoadIDMusic()
    {
        if (LoadPlayer() != null)
        {
            string loadIdMusic = LoadPlayer()[8];
            int loadIdMusicData = int.Parse(loadIdMusic);
            return loadIdMusicData;
        }
        else { return 0; }
    }

    public static int LoadLightState()
    {
        if (LoadPlayer() != null)
        {
            string loadLigthState = LoadPlayer()[9];
            int loadLightStateData = int.Parse(loadLigthState);
            return loadLightStateData;
        }
        else { return 0; }
    }
    #endregion
}