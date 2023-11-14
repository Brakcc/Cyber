using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineMaker : MonoBehaviour
{
    private OutlineSystem outSys;
    private List<Hex> hexList;
    public RenderTexture renderTexture;
    private Vector2 Scale = new Vector2(1,1);
    private Vector2 Position = new Vector2();
    public HexGridStore hexGrid;
    private Vector3Int pos;

    public void Start()
    {
        outSys = new();
        hexList = new List<Hex>();
        
    }

    public void Update()
    {
        
        Debug.Log("OutlineManager Active");
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pos = UnitManager.unitManager.SelectedUnit.CurrentHexPos;
            Debug.Log("Clicked");
            hexList = outSys.GetOutlineList(pos, hexGrid, 1).Values.ToList();
        }
        //DrawTexture(hexList);
    }
    /*
    public void DrawTexture(List<Hex> hexList)
    {
        
        foreach (Hex hex in hexList)
        {
            Texture2D spriteToDraw = hex.gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            Graphics.Blit(spriteToDraw, renderTexture, Scale, Position);
        }
        
    }
*/
}