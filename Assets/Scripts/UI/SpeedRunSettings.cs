using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedRunSettings : MonoBehaviour
{
    [SerializeField] private GameObject totalPlay;
    [SerializeField] private GameObject currentTotalTime;
    //GameObject[] allChecks;

    [SerializeField] private Image CT; 
    [SerializeField] private Image TT; 
    [SerializeField] private Image NC; 

    private bool tt;
    private bool ct;
    private bool nc;

    private void Start()
    {
        //allChecks = GameObject.FindGameObjectsWithTag("CheckPoints");
        InitializeUIs();
    }

    void InitializeUIs()
    {
        if (SavePlayerData.LoadTotalMode() == 1) { totalPlay.SetActive(true); tt = true; TT.color = Color.red; }
        else if (SavePlayerData.LoadTotalMode() == 0) { totalPlay.SetActive(false); tt = false; }

        if (SavePlayerData.LoadCurrentMode() == 1) { currentTotalTime.SetActive(true); ct = true; CT.color = Color.red; }
        else if (SavePlayerData.LoadCurrentMode() == 0) { currentTotalTime.SetActive(false); ct = false; }

        if (SavePlayerData.LoadNCMode() == 1) { nc = true; NC.color = Color.red; }
        else if (SavePlayerData.LoadNCMode() == 0) { nc = false; }
    }

    public void SetTotalPlay()
    {
        if (!tt)
        {
            tt = true;
            TT.color = Color.red;
            GameManager.gm.totalClockOn = 1;
        }
        else
        {
            tt = false;
            TT.color = Color.white;
            GameManager.gm.totalClockOn = 0;
        }
        totalPlay.SetActive(tt);
    }
    public void SetCurrentTime()
    {
        if (!ct)
        {
            ct = true;
            CT.color = Color.red;
            GameManager.gm.currentRunOn = 1;
        }
        else
        {
            ct = false;
            CT.color = Color.white;
            GameManager.gm.currentRunOn = 0;
        }
        currentTotalTime.SetActive(ct);
    }
    public void SetNoCheck()
    {
        if (!nc)
        {
            nc = true;
            NC.color = Color.red;
            //GameManager.gm.NCStart();
            GameManager.gm.NCOn = 1;
        }
        else
        {
            nc = false;
            NC.color = Color.white;
            //GameManager.gm.NCStop();
            GameManager.gm.NCOn = 0;
        }
    }
}
