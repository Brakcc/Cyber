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

    #region Instance et Awake
    public static UnitManager unitManager;

    void Awake()
    {
        unitManager = this;
        moveSys = GetComponent<MoveSystem>();
    }  
    #endregion

    #region methodes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { PlayerTurn = true; }
    }

    /// <summary>
    /// Si l'unit peut jouer son tour : 2 possibilités :
    /// -> Si elle était déjà sélectionnée : ClearOldSelection
    /// -> Sinon PreparUnitForMove
    /// </summary>
    /// <param name="unit"></param>
    public void HandleUnitSelected(GameObject unit)
    {
        if (!PlayerTurn) return;
        Unit unitReference = unit.GetComponent<Unit>();

        if (CheckIfTheSameUnitSelected(unitReference)) return;

        PrepareUnitForMove(unitReference);
    }

    /// <summary>
    /// Si une Unit est sélectionnée et le tour de l'Unit en cours : 
    /// -> Si l'hex est hors de range ou celle du perso (Si celle du perso le perso est unselect et ClearOldSelection) : 
    /// -> HandleTargetSelected
    /// </summary>
    /// <param name="selectedHex"></param>
    public void HandleTerrainSelect(GameObject selectedHex)
    {
        if (selectedUnit == null || !PlayerTurn) return;
        Hex selHex = selectedHex.GetComponent<Hex>();

        if (HandleHexOutOfRange(selHex.hexCoords) || HandleSelectedHexIsUnitHex(selHex.hexCoords)) return;

        HandleTargetSelectedHex(selHex);
    }

    /// <summary>
    /// si Unit est selected : 
    /// -> Select = ShowGlow 
    /// -> ShowRange
    /// </summary>
    /// <param name="unitRef"></param>
    void PrepareUnitForMove(Unit unitRef)
    {
        if (selectedUnit != null) { ClearOldSelection(); }

        selectedUnit = unitRef;
        selectedUnit.Select();
        moveSys.ShowRange(selectedUnit, hexGrid);
    }

    /// <summary>
    /// reset le perso selectionné et les sélections graphiques
    /// </summary>
    void ClearOldSelection()
    {
        previousSelectedHex = null;
        selectedUnit.Deselect();
        moveSys.HideRange(hexGrid);
        selectedUnit = null;
    }

    /// <summary>
    /// Si Hex une nouvelle hex dans la range : new path glow
    /// / Si hex dej a select au bout du path : MovePlayer
    /// </summary>
    /// <param name="selects"></param>
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

    /// <summary>
    /// Si même Unit sélectionnée 2 fois, elle est désélectionnée 
    /// </summary>
    /// <param name="unitRef"></param>
    /// <returns></returns>
    bool CheckIfTheSameUnitSelected(Unit unitRef)
    {
        if (selectedUnit == unitRef)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Si hex sélectionnée celui de l'Unit, même effet que SameUnitSelected : 
    /// -> ClearOldSelection
    /// </summary>
    /// <param name="hexPos"></param>
    /// <returns></returns>
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

    /// <summary>
    /// L'hex sélectionnée est ou non hors de portée
    /// </summary>
    /// <param name="hexPos"></param>
    /// <returns></returns>
    bool HandleHexOutOfRange(Vector3Int hexPos)
    {
        if (!moveSys.IsHexInRange(hexPos)) { return true; }
        return false;
    }
    #endregion
}
