using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MouseController : MonoBehaviour
{
    #region fields
    [SerializeField] private GameObject charaPref;
    private PlayerManager player;
    [SerializeField] private float speed;

    private PathFinder pathFinder;
    private List<Hovering> path = new List<Hovering>();
    #endregion

    #region methodes
    private void Start()
    {
        pathFinder = new PathFinder();
    }

    private void LateUpdate()
    {
        var focusedTileHit = GetFocusedOnTile();

        if (focusedTileHit.HasValue)
        {
            Hovering hoveredTile = focusedTileHit.Value.collider.gameObject.GetComponent<Hovering>();
            transform.position = hoveredTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = hoveredTile.GetComponent<SpriteRenderer>().sortingOrder;

            if (Input.GetMouseButtonDown(0))
            {
                hoveredTile.GetComponent<Hovering>().ShowHover();

                if (player == null)
                {
                    player = Instantiate(charaPref).GetComponent<PlayerManager>();
                    PositionCharacterOnTile(hoveredTile);
                }
                else
                {
                    path = pathFinder.FindPath(player.standingOnTile, hoveredTile);
                }
            }
        }

        if (path.Count > 0)
        {
            FollowPath();
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
    #endregion
}
