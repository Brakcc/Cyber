using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAManager: MonoBehaviour
{
    List<Vector3> TargetsPos = new List<Vector3>();


    public static IAManager instance;


    private void Awake()
    {
        if ( instance != null)
        {
            Debug.Log("Coup dur pour le Singleton");
            return;
        }
        instance = this;
    }

    public void Choose(Vector3 position)
    {

        TargetsPos.Add(position);

        foreach (var x in TargetsPos)
        {
            Debug.Log(x.ToString());
        }
    }

}

