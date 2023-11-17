using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    #region fields
    public static int playerPlay = 1;
    public List<GameObject> heroPlayer1 = new();
    public List<GameObject> heroPlayer2 = new();

    public static int countPerso1 = 0;
    public static int countPerso2 = 0;

    #endregion
    private void Update()
    {
        OnSelectionPlayer();
    }

    #region selectionPlayer

    void OnSelectionPlayer()
    {
        if (playerPlay == 1)
        {
            OnPlayer1Play();
            if (heroPlayer1.Count <= countPerso1)
            {
                playerPlay = 2;
                countPerso1 = 0;
            }
        }
        if (playerPlay == 2)
        {
            OnPlayer2Play();
            if (heroPlayer2.Count <= countPerso2)
            {
                playerPlay = 1;
                countPerso2 = 0;
            }
        }
    }


    #endregion

    #region PlayerPlay
    void OnPlayer1Play()
    {
        foreach (GameObject Bob in heroPlayer1)
        {
            Bob.GetComponent<Unit>().CanPlay = true;
        }
        foreach (GameObject Bob in heroPlayer2)
        {
            Bob.GetComponent<Unit>().CanPlay = false;
        }
    }

    void OnPlayer2Play()
    {
        foreach (GameObject Bob in heroPlayer2)
        {
            Bob.GetComponent<Unit>().CanPlay = true;

        }
        foreach (GameObject Bob in heroPlayer1)
        {
            Bob.GetComponent<Unit>().CanPlay = false;
        }
    }

    #endregion


}