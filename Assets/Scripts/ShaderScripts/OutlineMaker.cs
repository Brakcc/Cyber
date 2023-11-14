using System.Collections.Generic;
using UnityEngine;

public class OutlineMaker : MonoBehaviour
{

    public RenderTexture renderTexture;
    private Vector2 Scale = new Vector2(1,1);
    private Vector2 Position = new Vector2();

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }

    public void DrawTexture(List<Hex> hexList)
    {
        foreach (Hex hex in hexList)
        {
            Texture2D spriteToDraw = hex.gameObject.GetComponent<Sprite>().texture;
            Graphics.Blit(spriteToDraw, renderTexture, Scale, Position);
        }
        
    }

}