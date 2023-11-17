using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAManager: MonoBehaviour
{
    List<Vector3> TargetsPos = new List<Vector3>();
    

    public static IAManager instance;
    Dictionary<int, Vector3> posibility = new Dictionary<int, Vector3>();

    PathResult result = new PathResult();

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
            int posibilityValue = TargetsPos.Count;
            posibility.Add(posibilityValue, position);
        }
    }

    public void Start()
    {
        int posibilityValue = TargetsPos.Count;
        Debug.Log("test" + posibility[1]);
        Debug.Log("test" + posibility[2]);


        

      //  result.GetPathTo(new Vector3Int(2,4,0));
    }

}

