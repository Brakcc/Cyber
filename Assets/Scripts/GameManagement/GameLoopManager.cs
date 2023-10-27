using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    #region fields
    public static int playerPlay = 1;
    List<GameObject> heroPlayer1 = new List<GameObject>();
    List<GameObject> heroPlayer2 = new List<GameObject>();

    public static int countPerso1;
    public static int countPerso2;

    //liste perso
    [SerializeField] GameObject perso1_1;
    [SerializeField] GameObject perso1_2;
    [SerializeField] GameObject perso1_3;
    [SerializeField] GameObject perso1_4;

    [SerializeField] GameObject perso2_1;
    [SerializeField] GameObject perso2_2;
    [SerializeField] GameObject perso2_3;
    [SerializeField] GameObject perso2_4;

    #endregion
    private void Update()
    {
        OnAddToList();
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
         if(playerPlay == 2)
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

    #region ListAdd

    void OnAddToList()
    {
        heroPlayer1.Add(perso1_1);
        heroPlayer1.Add(perso1_2);
        heroPlayer1.Add(perso1_3);
        heroPlayer1.Add(perso1_4);        
        
        heroPlayer2.Add(perso2_1);
        heroPlayer2.Add(perso2_2);
        heroPlayer2.Add(perso2_3);
        heroPlayer2.Add(perso2_4);

    }

    #endregion
}
