using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KapaDash
{
    [SerializeField] private int range;
    [SerializeField] private int speed;

    public void Grab() { Debug.Log(range); }
}
