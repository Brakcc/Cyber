using UnityEngine;

public class UnitManager : MonoBehaviour
{
    #region fields
    [SerializeField] private HexGridStore hexGrid;
    private MoveSystem moveSys;

    private Unit selectedUnit;
    private Hex previousSelectedHex;

    //Kapa fields


    public bool PlayerTurn { get; private set; } = true;
    #endregion

    #region Instance et Awake
    public static UnitManager unitManager;

    void Awake()
    {
        unitManager = this;
        moveSys = GetComponent<MoveSystem>();
        selectedUnit = null;
        previousSelectedHex = null;
    }  
    #endregion

    #region selections methodes
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { PlayerTurn = true; }
    }

    /// <summary>
    /// Si l'unit peut jouer son tour : 2 possibilit�s :
    /// -> Si elle �tait d�j� s�lectionn�e : ClearOldSelection
    /// -> Sinon PreparUnitForMove
    /// </summary>
    /// <param name="unit"></param>
    public void HandleUnitSelected(GameObject unit)
    {
        if (!PlayerTurn) return;
        Unit unitReference = unit.GetComponent<Unit>();
        
        if (CheckIfUnitCanPlay(unitReference)) return;
        if (CheckIfCanSelectOtherUnitAndIfSameUnit(unitReference)) return;

        PrepareUnitForMove(unitReference);
    }

    /// <summary>
    /// Si une Unit est s�lectionn�e et le tour de l'Unit en cours : 
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

    public void HandleKapaSelected(GameObject unit)
    {
        Unit unitRef = unit.GetComponent<Unit>();
        if (!PlayerTurn || selectedUnit == null || !unitRef.CanPlay || unitRef.IsDead) return;

        //if ()

    }
    #endregion

    #region movements methodes
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
            // Ici on utilise pas le ClearOldSelection pour ne pas reset l'Unit Selected, On veut Lock l'Unit pour la phase de Capa
            ClearGraphKeepUnit();
            LockUnitAfterMove();
        }
    }
    #endregion

    #region passage d'une Unit � l'autre
    /// <summary>
    /// si une autre unit est selectionn�e avant le lock par d�placement de la selected unit ou si pas d'unit selectionn�e: 
    /// -> pas d'unit
    /// -> switch la s�lection d'unit
    /// -> sinon bloque la s�lection
    /// </summary>
    /// <param name="unitRef">�_�</param>
    /// <returns></returns>
    bool CheckIfCanSelectOtherUnitAndIfSameUnit(Unit unitRef)
    {
        //il n'y a pas d'unit pre-select
        if (selectedUnit == null) return false;
        //On clique sur UNE AUTRE unit et la SELECTED UNIT n'est PAS LOCK
        else if (selectedUnit != unitRef && !selectedUnit.IsPersoLocked)
        {
            ClearOldSelection();
            //on inverse la sortie pour pouvoir continuer la methode de s�lection des persos
            return false;
        }
        //On clique sur LA MEME Unit et elle est DEJA LOCK
        else if (selectedUnit == unitRef && selectedUnit.IsPersoLocked)
        {
            //feedbacks pour montrer que le perso doit faire une capa (ex : petit son de bip un peu techno des familles)
            return true;
        }
        //On clique sur LA MEME unit mais ELLE n'est PAS LOCK (Old CheckIfSameUnitSelected)
        else if (selectedUnit == unitRef && !selectedUnit.IsPersoLocked)
        {
            ClearOldSelection();
            return false;
        }
        //On clique sur UNE AUTRE Unit mais la SELECTED UNIT est DEJA LOCKED
        else
        {
            //action de refus : mettre des feedbacks
            //on inverse la sortie pour pouvoir continuer la methode de s�lection des persos
            return true;
        }
    }

    /// <summary>
    /// verif si le perso n'est pas mort et que son tour n'est pas pass�
    /// </summary>
    /// <param name="unitRef"></param>
    /// <returns></returns>
    bool CheckIfUnitCanPlay(Unit unitRef)
    {
        //Si l'unit s�lectionn�e peut faire son tour ET n'est pas morte
        if (unitRef.CanPlay && !unitRef.IsDead)
        {
            //condition invers�e pour continuer
            return false; 
        }
        //feedbacks un peu sad mais �lectro quand meme
        return true;
    }

    /// <summary>
    /// force l'utilisation de capa ou skip capa du perso apr�s son d�placement
    /// </summary>
    /// <param name="unit"></param>
    void LockUnitAfterMove()
    {
        selectedUnit.IsPersoLocked = true;
    }
    #endregion

    #region cas particuliers de Hexs
    /// <summary>
    /// Si hex s�lectionn�e celui de l'Unit, m�me effet que SameUnitSelected : 
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
    /// L'hex s�lectionn�e est ou non hors de port�e
    /// </summary>
    /// <param name="hexPos"></param>
    /// <returns></returns>
    bool HandleHexOutOfRange(Vector3Int hexPos)
    {
        if (!moveSys.IsHexInRange(hexPos)) { return true; }
        return false;
    }
    #endregion

    #region Clearing methodes
    /// <summary>
    /// reset le perso selectionn� et les s�lections graphiques
    /// </summary>
    void ClearOldSelection()
    {
        previousSelectedHex = null;
        selectedUnit.Deselect();
        moveSys.HideRange(hexGrid);
        selectedUnit = null;
    }

    /// <summary>
    /// reset les selections graphiques mais GARDE LE SELECTED UNIT
    /// </summary>
    void ClearGraphKeepUnit()
    {
        previousSelectedHex = null;
        selectedUnit.Deselect();
        moveSys.HideRange(hexGrid);
    }
    #endregion
}
