using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    #region fields
    private HexGridStore hexGrid;
    private MoveSystem moveSys;

    //Unit currently stored
    private Unit selectedUnit;
    public Unit SelectedUnit { get => selectedUnit;
                               set { selectedUnit = value; } }
    //Hex Currently processed
    private Hex previousSelectedHex;
    public Hex PreviousSelectedHex { get => previousSelectedHex; }

    //Is a Kapa Utility, pas vraiment de possibilité de store directement une Kapa
    //on store directement une list de patern, les methodes de kapas étant deja stored dans le cache AKapaSO
    public KapaType CurrentTypeKapaSelected { get; private set; }
    public KapaDir CurrentSelectedKapaDir { get; private set; }
    public bool IsKapaSelected { get; private set; }
    public bool IsKapaDirSelected { get; private set; }
    public List<Vector3Int> CurrentButtonPos {get; private set; }
    public List<Vector3Int> CurrentKapaPaternPos { get; private set; }  

    //Player Turn à déplacer dans le GameLoopManager !!!!!
    public bool PlayerTurn { get; private set; } = true;
    #endregion

    #region Instance et Awake
    public static UnitManager unitManager;

    void Awake()
    {
        unitManager = this;
        moveSys = new();
        hexGrid = GetComponent<HexGridStore>();
        selectedUnit = null;
        previousSelectedHex = null;
        CurrentTypeKapaSelected = KapaType.Default;
        IsKapaSelected = false;
        CurrentKapaPaternPos = null;
        CurrentButtonPos = null;
        Init(hexGrid);
    }
    #endregion

    #region selections methodes and update
    void Update()
    {
        //Update uniquement utile actuellement pour faire du debug, vouée à disparaitre
        if (Input.GetKeyDown(KeyCode.Space)) { PlayerTurn = true; ResetLoop(); }
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            foreach (Vector3Int v in hexGrid.GetNeighbourgs(HexCoordonnees.GetClosestHex(selectedUnit.transform.position))) { Debug.Log(v); }
        }
        if (Input.GetKeyDown(KeyCode.T)) { Debug.Log(selectedUnit.CurrentHexPos); }
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
        
        if (CheckIfUnitCanPlay(unitReference)) return;
        if (CheckIfCanSelectOtherUnitAndIfSameUnit(unitReference)) return;

        PrepareUnitForMove(unitReference);
        //ShowKapasUI(unitReference);
    }

    /// <summary>
    /// Si une Unit est sélectionnée et le tour de l'Unit en cours : 
    /// =>>> Gère la selection de kapaDir !!!!!
    /// -> Si l'hex est hors de range ou celle du perso (Si celle du perso le perso est unselect et ClearOldSelection) : 
    /// -> HandleTargetSelected
    /// </summary>
    /// <param name="selectedHex"></param>
    public void HandleTerrainSelect(GameObject selectedHex)
    {
        if (selectedUnit == null || !PlayerTurn) return;
        Hex selHex = selectedHex.GetComponent<Hex>();

        if (IsKapaSelected) { HandleKapaDirSelect(selHex.HexCoords, SelectedUnit); return; }
        if (selectedUnit.IsPersoLocked) return;
        if (HandleHexOutOfRange(selHex.HexCoords) || HandleSelectedHexIsUnitHex(selHex.HexCoords)) return;

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
        foreach (AKapaSO AK in unitRef.UnitData.KapasList)
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
            moveSys.ShowPath(selects.HexCoords, hexGrid);
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
    /// <summary>
    /// Génere une liste de bouton de coordo basé sur la fonction GetNeighbours pour recup les boutons dispo autour du joueur
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="hexGrid"></param>
    /// <returns></returns>
    List<Vector3Int> GenerateButtonPos(Unit unit, HexGridStore hexGrid, AKapaSO kapa) => kapa.GenerateButton(hexGrid, unit);

    /// <summary>
    /// Basé sur la pos du l'unit et celle de la tile cliquée, on trace un VECTEUR entre la tile et la selectedUnit pour connaitre la dir selon la parité
    /// Reset de la direction deja selectionnée PUIS selection de la nouvelle direction
    /// -> modif la liste de tiles qui seront affectées par la kapa; la Kapa en elle meme est stocké grace à la kapatype, selectedUnit et kapalist de la selectedUnit
    /// -> refresh les affichages de boutons
    /// </summary>
    /// <param name="buttonPos"></param>
    /// <param name="unitRef"></param>
    void HandleKapaDirSelect(Vector3Int buttonPos, Unit unitRef)
    {
        var dir = buttonPos - unitRef.CurrentHexPos;
        if (HandleHexOutOfButton(buttonPos)) return;
        unitRef.UnitData.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
        if (Direction.IsPariryEven(unitRef.CurrentHexPos.x))
        {
            CurrentKapaPaternPos = HandleKapaEvenDirPaternGen(dir, unitRef);
        }
        else
        {
            CurrentKapaPaternPos = HandleKapaOddDirPaternGen(dir, unitRef);
        }
        ShowButtons(CurrentButtonPos);
        ShowKapa(CurrentKapaPaternPos);
        IsKapaDirSelected = true;
    }

    #region Patern Application
    /// <summary>
    /// pour l'unit sur une case IMPAIRE, revoit le bon paterne de Kapa qui sera affiché sur les bonne tiles ensuite selon la pos de l'unit
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="unit"></param>
    List<Vector3Int> HandleKapaOddDirPaternGen(Vector3Int dir, Unit unit)
    {
        AKapaSO tempKapa = unit.UnitData.KapasList[(int)CurrentTypeKapaSelected];
        return (dir.x, dir.y, dir.z) switch
        {
            (0, 1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.OddNTiles),
            (1, 0, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.OddENTiles),
            (1, -1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.OddESTiles),
            (0, -1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.OddSTiles),
            (-1, -1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.OddWSTiles),
            (-1, 0, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.OddWNTiles),
            _ => new()
        };
    }

    /// <summary>
    /// pour l'unit sur une case PAIRE, revoit le bon paterne de Kapa qui sera affiché sur les bonne tiles ensuite selon la pos de l'unit
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="unit"></param>
    List<Vector3Int> HandleKapaEvenDirPaternGen(Vector3Int dir, Unit unit)
    {
        AKapaSO tempKapa = unit.UnitData.KapasList[(int)CurrentTypeKapaSelected];
        return (dir.x, dir.y, dir.z) switch
        {
            (0, 1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.EvenNTiles),
            (1, 1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.EvenENTiles),
            (1, 0, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.EvenESTiles),
            (0, -1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.EvenSTiles),
            (-1, 0, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.EvenWSTiles),
            (-1, 1, 0) => tempKapa.SelectGraphTiles(unit, hexGrid, tempKapa.EvenWNTiles),
            _ => new()
        };
    }
    #endregion

    /// <summary>
    /// Gère la selection des Kapas à la façon des Units, On doit selectionner à 2 fois une compétence avant de pouvoir l'executer.
    /// PENSER A AJOUTER LA METHODES POUR LA SELECTION DE LA DIRECTION DE LA COMPETENCE
    /// </summary>
    /// <param name="i"></param>
    public void HandleKapaSelect(int i)
    {
        if (selectedUnit == null) return;

        KapaType type = selectedUnit.UnitData.KapasList[i].KapaType;
        AKapaSO kapa = selectedUnit.UnitData.KapasList[i];

        if (CurrentTypeKapaSelected != KapaType.Default) kapa.DeselectTiles(hexGrid);
        if (!IsKapaSelected && !selectedUnit.IsPersoLocked) ClearGraphKeepUnit();

        //preselec Kapa
        if (!IsKapaSelected || CurrentTypeKapaSelected != type)
        {
            CurrentButtonPos = GenerateButtonPos(SelectedUnit, hexGrid, kapa);
            CurrentTypeKapaSelected = type;
            IsKapaSelected = true;
            ShowButtons(CurrentButtonPos);
            return;
        }

        //Active Kapa
        if (CurrentKapaPaternPos != null || IsKapaSelected && CurrentTypeKapaSelected == type)
        {
            if (selectedUnit.UnitData.KapasList[i].Execute(SelectedUnit)) { FullResetKapAndPlayer(); }
        }
    }

    /// <summary>
    /// simple boucle d'AFFICHAGE GRAPH SEULEMENT des boutons autour d'une Unit
    /// </summary>
    /// <param name="butPos"></param>
    void ShowButtons(List<Vector3Int> butPos)
    {
        if (butPos != null)
        foreach (var i in butPos) { var j = hexGrid.GetTile(i); j.EnableGlowButton(); j.GetColorGlowButton(); }
    }

    void ShowKapa(List<Vector3Int> kapaPos)
    {
        if (kapaPos != null)
        foreach (var i in kapaPos) { var j = hexGrid.GetTile(i); j.GlowOnButton(); }
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
        SelectedUnit.Deselect();
        SelectedUnit.IsPersoLocked = false;
        SelectedUnit.CanPlay = false;
        SelectedUnit = null;
        //GameLoop reset
        /*if (GameLoopManager.playerPlay == 1)
        {
            GameLoopManager.CountPerso1++;
        }
        if (GameLoopManager.playerPlay == 2)
        {
            GameLoopManager.CountPerso2++;
        }*/
    }
    #endregion

    #region passage d'une Unit à l'autre
    /// <summary>
    /// si une autre unit est selectionnée avant le lock par déplacement de la selected unit ou si pas d'unit selectionnée: 
    /// -> pas d'unit
    /// -> switch la sélection d'unit
    /// -> sinon bloque la sélection
    /// GERE EGALEMENT LA TRANSITION ENTRE UNIT ET KAPA
    /// </summary>
    /// <param name="unitRef">é_é</param>
    /// <returns></returns>
    bool CheckIfCanSelectOtherUnitAndIfSameUnit(Unit unitRef)
    {
        //il n'y a pas d'unit pre-select
        if (selectedUnit == null) return false;
        //On clique sur UNE AUTRE unit et la SELECTED UNIT n'est PAS LOCK
        else if (selectedUnit != unitRef && !selectedUnit.IsPersoLocked)
        {
            if (!IsKapaSelected)
            {
                ClearOldSelection();
                return false;
            }
            else
            {
                selectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
                ClearDataSelectionAvoidRange();
                ResetKapaData();
                return false;
            }
            //on inverse la sortie pour pouvoir continuer la methode de sélection des persos
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
            if (!IsKapaSelected)
            {
                ClearOldSelection();
                return true;
            }
            else
            {
                selectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].DeselectTiles(hexGrid);
                ClearDataSelectionAvoidRange();
                ResetKapaData();
                return false;
            }
        }
        //On clique sur UNE AUTRE Unit mais la SELECTED UNIT est DEJA LOCKED
        else
        {
            //action de refus : mettre des feedbacks
            //on inverse la sortie pour pouvoir continuer la methode de sélection des persos
            //ATENTION !!!!! dans le cas particulier ou une Kapa vise unu Unit au contact, on skip la selection de l'Unit mais on la selection de kapaDir à la pos de l'Unit visée
            if (IsKapaSelected) { HandleKapaDirSelect(unitRef.CurrentHexPos, SelectedUnit); }
            return true;
        }
    }

    /// <summary>
    /// verif si le perso n'est pas mort et que son tour n'est pas passé
    /// </summary>
    /// <param name="unitRef"></param>
    /// <returns></returns>
    bool CheckIfUnitCanPlay(Unit unitRef)
    {
        //Si l'unit sélectionnée peut faire son tour ET n'est pas morte
        if (unitRef.CanPlay && !unitRef.IsDead)
        {
            //condition inversée pour continuer
            return false; 
        }
        //feedbacks un peu sad mais électro quand meme
        return true;
    }

    /// <summary>
    /// force l'utilisation de capa ou skip capa du perso après son déplacement
    /// </summary>
    /// <param name="unit"></param>
    void LockUnitAfterMove()
    {
        selectedUnit.IsPersoLocked = true;
    }
    #endregion

    #region cas particuliers de Hexs
    /// <summary>
    /// Si hex sélectionnée celui de l'Unit, même effet que SameUnitSelected : 
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
    /// L'hex sélectionnée est ou non hors de portée
    /// </summary>
    /// <param name="hexPos"></param>
    /// <returns></returns>
    bool HandleHexOutOfRange(Vector3Int hexPos)
    {
        if (!moveSys.IsHexInRange(hexPos)) { return true; }
        return false;
    }

    /// <summary>
    /// vérifie si la tile selectionnée est un bouton autour du player ou juste une case sans action
    /// </summary>
    /// <param name="hexPos"></param>
    /// <returns></returns>
    bool HandleHexOutOfButton(Vector3Int hexPos)
    {
        if (CurrentButtonPos == null) return true;
        if (CurrentButtonPos.Contains(hexPos)) return false;
        return true;
    }
    #endregion

    #region Init
    /// <summary>
    /// Initialise la grille pour detecter les pos des joueurs en début de partie pour rendre impossible les dep sur ces tiles
    /// </summary>
    /// <param name="hex"></param>
    async void Init(HexGridStore hex)
    {
        await Task.Delay(100);
        foreach (GameObject u in GameObject.FindGameObjectsWithTag("Player")) { hex.GetTile(u.GetComponent<Unit>().CurrentHexPos).HasPlayerOnIt = true; }
    }
    #endregion

    #region Clearing and Reset methodes
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
    /// retire la selectedTile sur laquelle une Unit était et place la nouvelle tile en hasPlayerOnIt
    /// </summary>
    void ChargeNewUnitHexCoord()
    {
        hexGrid.GetTile(selectedUnit.CurrentHexPos).HasPlayerOnIt = false;
        previousSelectedHex.HasPlayerOnIt = true;
        selectedUnit.CurrentHexPos = previousSelectedHex.HexCoords;
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