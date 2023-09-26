using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private HexGridStore hexGrid;
    [SerializeField] private MoveSystem moveSys;

    [SerializeField] private Unit selectedUnit;
    private Hex previousSelectedHex;

    public bool PlayerTurn { get; private set; } = true;

    public void HandleUnitSelected(GameObject unit)
    {
        if (!PlayerTurn) return;
        Debug.Log("yes");
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

    public void HandleTerrainSelect(Hex selectedHex)
    {
        if (selectedUnit == null || !PlayerTurn) return;
        Debug.Log("ah");
        //Hex selectedHex = hex.GetComponent<Hex>();

        if (/*HandleHexOutOfRange(selectedHex.hexCoords) ||*/ HandleSelectedHexIsUnitHex(selectedHex.hexCoords)) return;

        HandleTargetSelectedHex(selectedHex);
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


}
