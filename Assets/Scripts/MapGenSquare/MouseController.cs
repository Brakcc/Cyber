using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MouseController : MonoBehaviour
{
    #region fields
    [SerializeField] private GameObject charaPref;
    private PlayerManager player;
    [SerializeField] private float speed;

    private List<Hovering> path = new List<Hovering>();

    private Range range;
    private List<Hovering> inRange = new List<Hovering>();
    #endregion

    #region unity
    private void Update()
    {
        //PathDef
        PathDef();
    }

    private void LateUpdate()
    {
        //PlayerMove
        if (path.Count > 0)
        {
            FollowPath();
        }
    }
    #endregion

    #region methodes
    private void ShowRangeTile() 
    {
        foreach (var i in inRange)
        {
            i.HideHover();
        }

        inRange = range.GetRangeTile(player.standingOnTile, 3);

        foreach (var i in inRange)
        {
            i.ShowHover();
        }
    }

    private void FollowPath()
    {
        var pas = speed * Time.fixedDeltaTime;

        var z = path[0].transform.position.z;
        player.transform.position = Vector2.MoveTowards(player.transform.position, path[0].transform.position, pas);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);

        if (Vector2.Distance(player.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionCharacterOnTile(path[0]);
            path.RemoveAt(0);
        }

        /*if (path.Count == 0)
        {
            ShowRangeTile();
        }*/

    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        else return null;
    }

    void PositionCharacterOnTile(Hovering tile)
    {
        player.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.001f, tile.transform.position.z);
        player.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        player.standingOnTile = tile;
    }

    private void PathDef()
    {
        var focusedTileHit = GetFocusedOnTile();

        if (focusedTileHit.HasValue)
        {
            Hovering hoveredTile = focusedTileHit.Value.collider.gameObject.GetComponent<Hovering>();
            transform.position = hoveredTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = hoveredTile.GetComponent<SpriteRenderer>().sortingOrder;

            //ShowRangeTile();
            if (Input.GetMouseButtonDown(0))
            {
                hoveredTile.GetComponent<Hovering>().ShowHover();

                if (player == null)
                {
                    player = Instantiate(charaPref).GetComponent<PlayerManager>();
                    PositionCharacterOnTile(hoveredTile);
                    //ShowRangeTile();
                }
                else
                {
                    path = PathFinder.FindPath(player.standingOnTile, hoveredTile);
                }
            }
        }
    }
    #endregion
}
