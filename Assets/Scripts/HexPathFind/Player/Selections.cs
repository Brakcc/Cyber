using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    #region fields
    [SerializeField] private Camera mainC;
    [SerializeField] private HexGridStore hexGrid;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private MoveSystem move;
    private InputsManager inputsMan;

    private RaycastHit2D? tileSelect;
    private RaycastHit2D? playerSelect;

    public LayerMask tileSelectionMask;
    public LayerMask playerSelectionMask;
    private List<Vector3Int> neighbours = new List<Vector3Int>();
    #endregion

    #region methodes
    void Awake()
    {
        if (mainC == null) { mainC = Camera.main; }
        inputsMan = new InputsManager();
    }

    void Update()
    {
        tileSelect = inputsMan.GetFocusedOnTile(tileSelectionMask);
        playerSelect = inputsMan.GetFocusedOnTile(playerSelectionMask);
        HandleClick();
    }

    public void HandleClick()
    {
        GameObject result;
        if (tileSelect.HasValue && Input.GetMouseButtonDown(0))
        {
            Perso1 player = playerSelect.Value.collider.gameObject.GetComponent<Perso1>();
            result = tileSelect.Value.collider.gameObject;
            Hex selects = result.GetComponent<Hex>();
            List<Vector3Int> path = new PathResult().GetPathTo(
                new Vector3Int (
                    Mathf.CeilToInt(tileSelect.Value.collider.gameObject.transform.position.x),
                    Mathf.CeilToInt(tileSelect.Value.collider.gameObject.transform.position.y),
                    Mathf.CeilToInt(tileSelect.Value.collider.gameObject.transform.position.z)));

            /*selects.DisableGlow();

            foreach (Vector3Int n in neighbours)
            {
                hexGrid.GetTile(n).DisableGlow();
            }

            PathResult pr = PathFind.PathGetRange(hexGrid, selects.hexCoords, 3);
            neighbours = new List<Vector3Int>(pr.GetRangePositions());

            foreach (Vector3Int n in neighbours)
            {
                hexGrid.GetTile(n).EnableGlow();
            }*/
            /*unitManager.HandleTerrainSelect(selects);
            Debug.Log("yes");*/

            //move.MoveUnit(playerSelect.Value.collider.gameObject.GetComponent<Perso1>(), hexGrid);
            List<Vector3> path2 = new List<Vector3>();
            foreach (Vector3Int v in path)
            {
                path2.Add(new Vector3(v.x, v.y, v.z));
            }

            player.FollowPath(path2);
        }
        else if (playerSelect.HasValue && Input.GetMouseButtonDown(0))
        {
            /*result = playerSelect.Value.collider.gameObject;
            unitManager.HandleUnitSelected(result);
            Debug.Log("no");*/
        }
    }
    #endregion
}
