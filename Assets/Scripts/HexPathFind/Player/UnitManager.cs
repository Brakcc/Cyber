using UnityEngine;

public class UnitManager : MonoBehaviour
{
    #region fields
    [SerializeField] private HexGridStore hexGrid;
    private MoveSystem moveSys;

    [SerializeField] private Unit selectedUnit;
    private Hex previousSelectedHex;

    public bool PlayerTurn { get; private set; } = true;
    #endregion

    #region methodes
    void Awake() => moveSys = GetComponent<MoveSystem>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { PlayerTurn = true; }
        if (Input.GetKeyDown(KeyCode.C)) { ClearOldSelection(); }
    }

    public void HandleUnitSelected(GameObject unit)
    {
        if (!PlayerTurn) return;
        Unit unitReference = unit.GetComponent<Unit>();

        if (CheckIfTheSameUnitSelected(unitReference)) return;

        PrepareUnitForMove(unitReference);
    }

    bool CheckIfTheSameUnitSelected(Unit unitRef)
    {
        if (selectedUnit == unitRef)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    public void HandleTerrainSelect(GameObject selectedHex)
    {
        if (selectedUnit == null || !PlayerTurn) return;
        Hex selHex = selectedHex.GetComponent<Hex>();

        if (HandleHexOutOfRange(selHex.hexCoords) || HandleSelectedHexIsUnitHex(selHex.hexCoords)) return;

        HandleTargetSelectedHex(selHex);
    }

    void PrepareUnitForMove(Unit unitRef)
    {
        if (selectedUnit != null) { ClearOldSelection(); }

        selectedUnit = unitRef;
        selectedUnit.Select();
        moveSys.ShowRange(selectedUnit, hexGrid);
    }

    void ClearOldSelection()
    {
        previousSelectedHex = null;
        selectedUnit.Deselect();
        moveSys.HideRange(hexGrid);
        selectedUnit = null;
    }

    void HandleTargetSelectedHex(Hex selects)
    {
        if (previousSelectedHex == null || previousSelectedHex != selects)
        {
            previousSelectedHex = selects;
            moveSys.ShowPath(selects.hexCoords, hexGrid);
        }
        else
        {
            moveSys.MoveUnit(selectedUnit, hexGrid);
            PlayerTurn = false;
            ClearOldSelection();
        }
    }

    bool HandleSelectedHexIsUnitHex(Vector3Int hexPos)
    {
        if (hexPos == hexGrid.GetClosestHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }

    bool HandleHexOutOfRange(Vector3Int hexPos)
    {
        if (!moveSys.IsHexInRange(hexPos)) { return true; }
        return false;
    }
    #endregion
}
