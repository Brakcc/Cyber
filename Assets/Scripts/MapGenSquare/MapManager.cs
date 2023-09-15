using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    #region fields
    [SerializeField] private Hovering hoverPref;
    [SerializeField] private GameObject hoverContainer;
    public Dictionary<Vector2Int, Hovering> dict;
    #endregion

    #region singleton
    private static MapManager map;
    public static MapManager Map { get { return map; } }
    private void Awake()
    {
        if (map != null && Map != null)
        {
            Debug.LogError("Il y a plus d'une instance de MapManager dans la scene");
        }
        else map = this;
    }
    #endregion

    #region methodes
    private void Start()
    {
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();
        dict = new Dictionary<Vector2Int, Hovering>();

        BoundsInt bounds = tileMap.cellBounds;

        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    var tileLocation = new Vector3Int(x, y, z);
                    var tileKey = new Vector2Int(x, y);

                    if (tileMap.HasTile(tileLocation) && !dict.ContainsKey(tileKey))
                    {
                        var hover = Instantiate(hoverPref, hoverContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        hover.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z - 1);
                        hover.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder;

                        hover.gridLoaction = tileLocation;

                        //Debug.Log("(" + hover.gridLoaction.x + ", " + hover.gridLoaction.y + ")" + " ; " + "(" + hover.transform.position.x + ", " + hover.transform.position.y + ")");
                        dict.Add(tileKey, hover);
                    }
                }
            }
        }
    }

    public List<Hovering> GetAdjTiles(Hovering current)
    {
        var map = dict;

        List<Hovering> adjs = new List<Hovering>();

        //top
        Vector2Int locationToCheck = new Vector2Int(current.gridLoaction.x + 1, current.gridLoaction.y);
        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(current.gridLoaction.z - map[locationToCheck].gridLoaction.z) < 1)
                adjs.Add(map[locationToCheck]);
        }

        //bottom
        locationToCheck = new Vector2Int(current.gridLoaction.x - 1, current.gridLoaction.y);
        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(current.gridLoaction.z - map[locationToCheck].gridLoaction.z) < 1)
                adjs.Add(map[locationToCheck]);
        }

        //top left
        locationToCheck = new Vector2Int(current.gridLoaction.x + 1, current.gridLoaction.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(current.gridLoaction.z - map[locationToCheck].gridLoaction.z) < 1)
                adjs.Add(map[locationToCheck]);
        }

        //bottom left
        locationToCheck = new Vector2Int(current.gridLoaction.x, current.gridLoaction.y - 1);
        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(current.gridLoaction.z - map[locationToCheck].gridLoaction.z) < 1)
                adjs.Add(map[locationToCheck]);
        }

        //top right
        locationToCheck = new Vector2Int(current.gridLoaction.x + 1, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(current.gridLoaction.z - map[locationToCheck].gridLoaction.z) < 1)
                adjs.Add(map[locationToCheck]);
        }

        //bottom right
        locationToCheck = new Vector2Int(current.gridLoaction.x, current.gridLoaction.y + 1);
        if (map.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(current.gridLoaction.z - map[locationToCheck].gridLoaction.z) < 1)
                adjs.Add(map[locationToCheck]);
        }

        return adjs;
    }
    #endregion
}
