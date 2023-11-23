using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KapaGrab
{
    [SerializeField] private int range;
    [SerializeField] private int debufMP;

    public void Grab() { Debug.Log(range); }
}
