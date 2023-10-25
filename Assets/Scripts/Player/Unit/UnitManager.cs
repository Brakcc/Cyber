using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class UnitManager : MonoBehaviour
{
    #region fields
    [SerializeField] private HexGridStore hexGrid;
    private MoveSystem moveSys;

    //Unit currently stored
    private Unit selectedUnit;
    public Unit SelectedUnit { get => selectedUnit;
                               set { selectedUnit = value; } }
    //Hex Currently processed
    private Hex previousSelectedHex;
    public Hex PreviousSelectedHex { get => previousSelectedHex; }

    //Is a Kapa Utility, pas vraiment de possibilit� de store directement une Kapa
    //on store directement une list de patern, les methodes de kapas �tant deja stored dans le cache AKapaSO
    public KapaType CurrentTypeKapaSelected { get; private set; }
    public KapaDir CurrentSelectedKapaDir { get; private set; }
    public bool IsKapaSelected { get; private set; }
    public bool IsKapaDirSelected { get; private set; }
    public List<Vector3Int> CurrentButtonPos {get; private set; }
    public List<Vector3Int> CurrentKapaPaternPos { get; private set; }  

    //Player Turn � d�placer dans le GameLoopManager !!!!!
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
        CurrentTypeKapaSelected = KapaType.Default;
        IsKapaSelected = false;
        CurrentKapaPaternPos = new();
        CurrentButtonPos = new();
        Init(hexGrid);
    }
    #endregion

    #region selections methodes and update
    void Update()
    {
        //Update uniquement utile actuellement pour faire du debug, vou�e � disparaitre
        if (Input.GetKeyDown(KeyCode.Space)) { PlayerTurn = true; ResetLoop(); }
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            foreach (Vector3Int v in hexGrid.GetNeighbourgs(HexCoordonnees.GetClosestHex(selectedUnit.transform.position))) { Debug.Log(v); }
        }
        if (Input.GetKeyDown(KeyCode.T)) { Debug.Log(selectedUnit.CurrentHexPos); }
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
        //ShowKapasUI(unitReference);
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

        if (IsKapaSelected) { HandleKapaDirSelect(selHex.hexCoords, SelectedUnit); return; }
        if (HandleHexOutOfRange(selHex.hexCoords) || HandleSelectedHexIsUnitHex(selHex.hexCoords)) return;

        HandleTargetSelectedHex(selHex);
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
        foreach (AKapaSO AK in unitRef.KapasList)
        {
            if (AK.KapaType == CurrentTypeKapaSelected) { AK.DeselectTiles(hexGrid); }
        }
        if (selectedUnit != null && !IsKapaSelected) { ClearOldSelection(); ResetKapaData(); }
        
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
            ChargeNewUnitHexCoord();
            moveSys.MoveUnit(selectedUnit, hexGrid);
            //PlayerTurn = false; trouver une autre methode pour bloquer le joueur en listant l'ensemble des units
            // Ici on utilise pas le ClearOldSelection pour ne pas reset l'Unit Selected, On veut Lock l'Unit pour la phase de Capa
            ClearGraphKeepUnit();
            LockUnitAfterMove();
        }
    }
    #endregion

    #region UI and Graph Management
    /*void InitKapaUI()
    {
        foreach(RectTransform c in bGAnimator.GetComponentInChildren<RectTransform>())
        {
            c.localScale = new Vector3(0, 0, 1);
        }
    }*/

    /*void ShowKapasUI(Unit unit)
    {
        
    }*/
    #endregion

    #region KapasCalls
    List<Vector3Int> GenerateButtonPos(Unit unit, HexGridStore hexGrid) => hexGrid.GetNeighbourgs(unit.CurrentHexPos);

    void HandleKapaDirSelect(Vector3Int buttonPos, Unit unitRef)
    {
        var dir = buttonPos - unitRef.CurrentHexPos;
        if (HandleHexOutOfButton(buttonPos)) return;
        unitRef.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
        if (Direction.IsPariryEven(unitRef.CurrentHexPos.x))
        {
            /*CurrentKapaPaternOffset = HandleKapaEvenDirPaternGen(dir, unitRef);
            unitRef.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unitRef, hexGrid, CurrentKapaPaternOffset);*/
            HandleKapaEvenDirPaternGen(dir, unitRef);
        }
        else
        {
            /*CurrentKapaPaternOffset = HandleKapaOddDirPaternGen(dir, unitRef);
            unitRef.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unitRef, hexGrid, CurrentKapaPaternOffset);*/
            HandleKapaOddDirPaternGen(dir, unitRef);
        }
        ShowButtons(CurrentButtonPos);
        IsKapaDirSelected = true;
    }

    void HandleKapaOddDirPaternGen(Vector3Int dir, Unit unit)
    {
        if (dir == new Vector3Int(0, 1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenNTiles); }
        if (dir == new Vector3Int(1, 0, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenENTiles); }
        if (dir == new Vector3Int(1, -1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenESTiles); }
        if (dir == new Vector3Int(0, -1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenSTiles); }
        if (dir == new Vector3Int(-1, -1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenWSTiles); }
        if (dir == new Vector3Int(-1, 0, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenWNTiles); }
    }

    void HandleKapaEvenDirPaternGen(Vector3Int dir, Unit unit)
    {
        if (dir == new Vector3Int(0, 1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenNTiles); }
        if (dir == new Vector3Int(1, 1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenENTiles); }
        if (dir == new Vector3Int(1, 0, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenESTiles); }
        if (dir == new Vector3Int(0, -1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenSTiles); }
        if (dir == new Vector3Int(-1, 0, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenWSTiles); }
        if (dir == new Vector3Int(-1, 1, 0)) { CurrentKapaPaternPos = unit.KapasList[(int)CurrentTypeKapaSelected].SelectGraphTiles(unit, hexGrid, unit.KapasList[(int)CurrentTypeKapaSelected].EvenWNTiles); }
    }
    #region Buttons
    /*//Odd ones
    void OnNorthOdd(Unit u)
    {
        CurrentSelectedKapaDir = KapaDir.North;
        CurrentKapaPaternPos = u.KapasList[(int)CurrentTypeKapaSelected].OddNTiles;
    }
    void OnEastNorthOdd(Unit u) { CurrentSelectedKapaDir = KapaDir.NorthEast; return u.KapasList[(int)CurrentTypeKapaSelected].OddENTiles; }
    void OnEastSouthOdd(Unit u) { CurrentSelectedKapaDir = KapaDir.SouthEast; return u.KapasList[(int)CurrentTypeKapaSelected].OddESTiles; }
    void OnSouthOdd(Unit u) { CurrentSelectedKapaDir = KapaDir.South; return u.KapasList[(int)CurrentTypeKapaSelected].OddSTiles; }
    void OnWestSouthOdd(Unit u) { CurrentSelectedKapaDir = KapaDir.SouthWest; return u.KapasList[(int)CurrentTypeKapaSelected].OddWSTiles; }
    void OnWestNorthOdd(Unit u) { CurrentSelectedKapaDir = KapaDir.NorthWest; return u.KapasList[(int)CurrentTypeKapaSelected].OddWNTiles; }
    //Even ones
    void OnNorthEven(Unit u) { CurrentSelectedKapaDir = KapaDir.North; return u.KapasList[(int)CurrentTypeKapaSelected].OddNTiles; }
    void OnEastNorthEven(Unit u) { CurrentSelectedKapaDir = KapaDir.NorthEast; return u.KapasList[(int)CurrentTypeKapaSelected].OddENTiles; }
    void OnEastSouthEven(Unit u) { CurrentSelectedKapaDir = KapaDir.SouthEast; return u.KapasList[(int)CurrentTypeKapaSelected].OddESTiles; }
    void OnSouthEven(Unit u) { CurrentSelectedKapaDir = KapaDir.South; return u.KapasList[(int)CurrentTypeKapaSelected].OddSTiles; }
    void OnWestSouthEven(Unit u) { CurrentSelectedKapaDir = KapaDir.SouthWest; return u.KapasList[(int)CurrentTypeKapaSelected].OddWSTiles; }
    void OnWestNorthEven(Unit u) { CurrentSelectedKapaDir = KapaDir.NorthWest; return u.KapasList[(int)CurrentTypeKapaSelected].OddWNTiles; }*/

    /*Vector3Int[] FromOffsetToCoord(Vector3Int[] offset, Unit unit)
    {
        Vector3Int[] v = new Vector3Int[offset.Length];
        for (int i = 0; i < offset.Length; i++) { }
    }*/
    #endregion

    /// <summary>
    /// G�re la selection des Kapas � la fa�on des Units, On doit selectionner � 2 fois une comp�tence avant de pouvoir l'executer.
    /// PENSER A AJOUTER LA METHODES POUR LA SELECTION DE LA DIRECTION DE LA COMPETENCE
    /// </summary>
    /// <param name="i"></param>
    public void HandleKapaSelect(int i)
    {
        if (selectedUnit == null) return;
        KapaType type = selectedUnit.KapasList[i].KapaType;
        if (CurrentTypeKapaSelected != KapaType.Default) selectedUnit.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
        if (!IsKapaSelected && !selectedUnit.IsPersoLocked) ClearGraphKeepUnit();

        //preselec Kapa
        if (!IsKapaSelected || CurrentTypeKapaSelected != type)
        {
            CurrentButtonPos = GenerateButtonPos(SelectedUnit, hexGrid);
            CurrentTypeKapaSelected = type;
            IsKapaSelected = true;
            ShowButtons(CurrentButtonPos);
            return;
        }

        //Active Kapa
        if (CurrentKapaPaternPos != null)
        {
            selectedUnit.KapasList[i].Execute();
            FullResetKapAndPlayer();
        }
    }

    void ShowButtons(List<Vector3Int> butPos)
    {
        foreach (var j in butPos) { hexGrid.GetTile(j).EnableGlowButton(); }
    }

    /// <summary>
    /// Reset les datas stored sur les Kapas : 
    /// -> Bool isKapaSelected
    /// -> CurrentKapaType remis en Default
    /// </summary>
    void ResetKapaData()
    {
        CurrentTypeKapaSelected = KapaType.Default;
        CurrentSelectedKapaDir = KapaDir.Default;
        IsKapaSelected = false;
        IsKapaDirSelected = false;
        CurrentButtonPos = null;
        CurrentKapaPaternPos = null;
    }

    /// <summary>
    /// reset completement l'ensemble des datas d'une kapa pour switch sur une autre
    /// et celle du perso selected
    /// </summary>
    void FullResetKapAndPlayer()
    {
        //Kapa Reset
        CurrentTypeKapaSelected = KapaType.Default;
        CurrentSelectedKapaDir = KapaDir.Default;
        IsKapaSelected = false;
        IsKapaDirSelected = false;
        CurrentButtonPos = null;
        CurrentKapaPaternPos = null;
        //Unit Reset
        SelectedUnit.IsPersoLocked = false;
        SelectedUnit.CanPlay = false;
        SelectedUnit = null;
    }
    #endregion

    #region passage d'une Unit � l'autre
    /// <summary>
    /// si une autre unit est selectionn�e avant le lock par d�placement de la selected unit ou si pas d'unit selectionn�e: 
    /// -> pas d'unit
    /// -> switch la s�lection d'unit
    /// -> sinon bloque la s�lection
    /// GERE EGALEMENT LA TRANSITION ENTRE UNIT ET KAPA
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
            ///Debug.Log("Switch selected Unit");
            if (!IsKapaSelected)
            {
                ClearOldSelection();
                return false;
            }
            else
            {
                selectedUnit.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
                ClearDataSelectionAvoidRange();
                ResetKapaData();
                return false;
            }
            //on inverse la sortie pour pouvoir continuer la methode de s�lection des persos
        }
        //On clique sur LA MEME Unit et elle est DEJA LOCK
        else if (selectedUnit == unitRef && selectedUnit.IsPersoLocked)
        {
            ///Debug.Log("Meme Unit et Locked");
            //feedbacks pour montrer que le perso doit faire une capa (ex : petit son de bip un peu techno des familles)
            return true;
        }
        //On clique sur LA MEME unit mais ELLE n'est PAS LOCK (Old CheckIfSameUnitSelected)
        else if (selectedUnit == unitRef && !selectedUnit.IsPersoLocked)
        {
            ///Debug.Log("Same Unit selected");
            if (!IsKapaSelected)
            {
                ClearOldSelection();
                ///Debug.Log("1");
                return true;
            }
            else
            {
                selectedUnit.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
                ClearDataSelectionAvoidRange();
                ResetKapaData();
                ///Debug.Log("2");
                return false;
            }
        }
        //On clique sur UNE AUTRE Unit mais la SELECTED UNIT est DEJA LOCKED
        else
        {
            ///Debug.Log("selectedUnit Locked");
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
        if (hexPos == selectedUnit.CurrentHexPos)
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

    bool HandleHexOutOfButton(Vector3Int hexPos)
    {
        if (CurrentButtonPos.Contains(hexPos)) return false;
        return true;
    }
    #endregion

    #region Inits
    async void Init(HexGridStore hex)
    {
        await Task.Delay(100);
        foreach (GameObject u in GameObject.FindGameObjectsWithTag("Player")) { hex.GetTile(u.GetComponent<Unit>().CurrentHexPos).hasPlayerOnIt = true; }
    }
    #endregion

    #region Clearing and Reset methodes
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
    /// ne clear que la selection de datas unit et hex, pas de deselect graphic pour alleger la methode et eviter les NullRefExcep
    /// </summary>
    void ClearDataSelectionAvoidRange()
    {
        previousSelectedHex = null;
        selectedUnit.Deselect();
        selectedUnit = null;
    }

    /// <summary>
    /// reset les selections graphiques mais GARDE LE SELECTED UNIT
    /// </summary>
    void ClearGraphKeepUnit()
    {
        previousSelectedHex = null;
        moveSys.HideRange(hexGrid);
    }

    /// <summary>
    /// retire la selectedTile sur laquelle une Unit �tait et place la nouvelle tile en hasPlayerOnIt
    /// </summary>
    void ChargeNewUnitHexCoord()
    {
        hexGrid.GetTile(selectedUnit.CurrentHexPos).hasPlayerOnIt = false;
        previousSelectedHex.hasPlayerOnIt = true;
        selectedUnit.CurrentHexPos = previousSelectedHex.hexCoords;
    }

    /// <summary>
    /// reset manuel de la gameloop temporaire
    /// </summary>
    void ResetLoop()
    {
        foreach (GameObject u in GameObject.FindGameObjectsWithTag("Player")) { u.GetComponent<Unit>().CanPlay = true; }
    }
    #endregion
}