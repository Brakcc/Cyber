using UnityEngine;
using System.IO;

public static partial class SavePlayerData
{
    public static void SaveModes(GameManager gm)
    {
        string path = Application.persistentDataPath + "/modes.modes";
        SpeedRunData sPData = new SpeedRunData(gm);
        File.WriteAllText(path, sPData.totalTimeClockOn.ToString() + ";" 
            + sPData.currentRunClockOn.ToString() + ";" 
            + sPData.NCModOn.ToString() + ";"
            + sPData.wasGameWon.ToString() + ";"
            + sPData.musicVol.ToString() + ";"
            + sPData.sfxVol.ToString());
    }

    public static string[] LoadModes()
    {
        string path = Application.persistentDataPath + "/modes.modes";
        if (File.Exists(path))
        {
            string playerModes;
            string[] dataModes;
            playerModes = File.ReadAllText(path);
            dataModes = Fonctions.UnpackData(playerModes);
            return dataModes;
        }
        else { return null; }
    }

    #region LoadModes
    public static int LoadTotalMode()
    {
        if (LoadModes() != null)
        {
            string totalMode = LoadModes()[0];
            int totalData = int.Parse(totalMode);
            return totalData;
        }
        else { return 0; }
    }

    public static int LoadCurrentMode()
    {
        if (LoadModes() != null)
        {
            string currentMode = LoadModes()[1];
            int currentData = int.Parse(currentMode);
            return currentData;
        }
        else { return 0; }
    }

    public static int LoadNCMode()
    {
        if (LoadModes() != null)
        {
            string NCMode = LoadModes()[2];
            int NCData = int.Parse(NCMode);
            return NCData;
        }
        else { return 0; }
    }

    public static int LoadGameWon()
    {
        if (LoadModes() != null)
        {
            string gameWon = LoadModes()[3];
            int gameWonData = int.Parse(gameWon);
            return gameWonData;
        }
        else { return 0; }
    }

    public static float LoadMusicVolume()
    {
        if (LoadModes() != null)
        {
            string musVol = LoadModes()[4];
            float musVolData = float.Parse(musVol);
            return musVolData;
        }
        else { return 0; }
    }

    public static float LoadSFXVolume()
    {
        if (LoadModes() != null)
        {
            string sfxVol = LoadModes()[5];
            float sfxVolData = float.Parse(sfxVol);
            return sfxVolData;
        }
        else { return -5; }
    }
    #endregion
}