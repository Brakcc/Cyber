using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    #region fields
    [SerializeField] private Camera mainC;
    [SerializeField] private HexGridStore hexGrid;
    private InputsManager inputsMan;

    private RaycastHit2D? input;
    public LayerMask selectionMask;
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
        input = inputsMan.GetFocusedOnTile(selectionMask);
        HandleClick();
    }

    public void HandleClick()
    {
        GameObject result;
        if (/*FindTile(pos, out result)*/ input.HasValue && Input.GetMouseButtonDown(0))
        {
            result = input.Value.collider.gameObject;
            Hex selects = result.GetComponent<Hex>();
            selects.DisableGlow();

            foreach (Vector3Int n in neighbours)
            {
                hexGrid.GetTile(n).DisableGlow();
            }

            //neighbours = hexGrid.GetNeighbourgs(selects.hexCoords);
            PathResult pr = PathFind.PathGetRange(hexGrid, selects.hexCoords, 3);
            neighbours = new List<Vector3Int>(pr.GetRangePositions());

            foreach (Vector3Int n in neighbours)
            {
                hexGrid.GetTile(n).EnableGlow();
            }
        }
    }
    #endregion
}
