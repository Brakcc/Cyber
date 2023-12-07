using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    #region fields
    MoveSystem _moveSys;

    //Unit currently stored
    IUnit _selectedUnit;
    public IUnit SelectedUnit { get => _selectedUnit; set => _selectedUnit = value; }
    //Hex Currently processed
    Hex _previousSelectedHex;

    //Is a Kapa Utility, pas vraiment de possibilité de store directement une Kapa
    //on store directement une list de patern, les methodes de kapas étant deja stored dans le cache AKapaSO
    public KapaType CurrentTypeKapaSelected { get; private set; }
    public KapaDir CurrentSelectedKapaDir { get; private set; }
    public bool IsKapaSelected { get; private set; }
    public bool IsKapaDirSelected { get; private set; }
    public List<Vector3Int> CurrentButtonPos {get; private set; }
    public List<Vector3Int> CurrentKapaPaternPos { get; private set; }  
    Vector3Int CurrentDirSelected { get; set; }

    //Player Turn à déplacer dans le GameLoopManager !!!!!
    public bool PlayerTurn { get; private set; } = true;
    #endregion

    #region Instance et Awake
    public static UnitManager unitManager;

    void Awake()
    {
        unitManager = this;
        _moveSys = new();
        _selectedUnit = null;
        _previousSelectedHex = null;
        CurrentTypeKapaSelected = KapaType.Default;
        IsKapaSelected = false;
        CurrentKapaPaternPos = null;
        CurrentButtonPos = null;
        Init(HexGridStore.hGS);
    }
    #endregion

    #region selections methodes (and update)
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
        if (_selectedUnit == null || !PlayerTurn) return;
        Hex selHex = selectedHex.GetComponent<Hex>();

        if (IsKapaSelected) { HandleKapaDirSelect(selHex.HexCoords, SelectedUnit); return; }
        if (_selectedUnit.IsPersoLocked) return;
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
    void PrepareUnitForMove(IUnit unitRef)
    {
        if (IsKapaSelected && IsKapaDirSelected) { unitRef.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGS, CurrentKapaPaternPos); }
        if (_selectedUnit != null && !IsKapaSelected) { ClearOldSelection(); ResetKapaData(); }
        
        _selectedUnit = unitRef;
        _selectedUnit.Select();
        _moveSys.ShowRange(_selectedUnit, HexGridStore.hGS);
    }

    /// <summary>
    /// Si Hex une nouvelle hex dans la range : new path glow
    /// / Si hex dej a select au bout du path : MovePlayer
    /// </summary>
    /// <param name="selects"></param>
    void HandleTargetSelectedHex(Hex selects)
    {
        if (_previousSelectedHex == null || _previousSelectedHex != selects)
        {
            _previousSelectedHex = selects;
            _moveSys.ShowPath(selects.HexCoords, HexGridStore.hGS);
        }
        else
        {
            ChargeNewUnitHexCoord();
            _moveSys.MoveUnit(_selectedUnit, HexGridStore.hGS);
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
    #region PreBuild methodes
    List<Vector3Int> GenerateButtonPos(IUnit unit, IKapa kapa) => kapa.OnGenerateButton(HexGridStore.hGS, unit);
    
    void OnExecuteKapa(IUnit unit, int i, List<Vector3Int> KapaPatternPos) => unit.UnitData.KapasList[i].OnExecute(HexGridStore.hGS, KapaPatternPos, unit);

    bool OnCheckKapa(IUnit unit, int i) => unit.UnitData.KapasList[i].OnCheckKapaPoints(unit);
    #endregion

    /// <summary>
    /// Basé sur la pos du l'unit et celle de la tile cliquée, on trace un VECTEUR entre la tile et la selectedUnit pour connaitre la dir selon la parité
    /// Reset de la direction deja selectionnée PUIS selection de la nouvelle direction
    /// -> modif la liste de tiles qui seront affectées par la kapa; la Kapa en elle meme est stocké grace à la kapatype, selectedUnit et kapalist de la selectedUnit
    /// -> refresh les affichages de boutons
    /// </summary>
    /// <param name="buttonPos"></param>
    /// <param name="unitRef"></param>
    void HandleKapaDirSelect(Vector3Int buttonPos, IUnit unitRef)
    {
        var dir = buttonPos - unitRef.CurrentHexPos;
        if (HandleHexOutOfButton(buttonPos)) return;

        //Active Kapa With Click On Map
        if (CurrentKapaPaternPos != null && IsKapaSelected && IsKapaDirSelected && CurrentDirSelected == dir)
        {
            if (OnCheckKapa(SelectedUnit, (int)CurrentTypeKapaSelected))
            {
                OnExecuteKapa(SelectedUnit, (int)CurrentTypeKapaSelected, CurrentKapaPaternPos);
                FullResetKapAndPlayer();
                return;
            }
        }

        if (IsKapaDirSelected) { unitRef.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGS, CurrentKapaPaternPos); }
        if (Direction.IsPariryEven(unitRef.CurrentHexPos.x))
        {
            CurrentKapaPaternPos = HandleKapaEvenDirPaternGen(dir, unitRef, unitRef.UnitData.KapasList[(int)CurrentTypeKapaSelected]);
        }
        else
        {
            CurrentKapaPaternPos = HandleKapaOddDirPaternGen(dir, unitRef, unitRef.UnitData.KapasList[(int)CurrentTypeKapaSelected]);
        }
        IsKapaDirSelected = true;
        CurrentDirSelected = dir;
    }

    #region Patern Application
    /// <summary>
    /// pour l'unit sur une case IMPAIRE, revoit le bon paterne de Kapa qui sera affiché sur les bonne tiles ensuite selon la pos de l'unit
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="unit"></param>
    List<Vector3Int> HandleKapaOddDirPaternGen(Vector3Int dir, IUnit unit, IKapa tempKapa) => (dir.x, dir.y, dir.z) switch
        {
            (0, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.OddNTiles),
            (1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.OddENTiles),
            (1, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.OddESTiles),
            (0, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.OddSTiles),
            (-1, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.OddWSTiles),
            (-1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.OddWNTiles),
            _ => new()
        };
    

    /// <summary>
    /// pour l'unit sur une case PAIRE, revoit le bon paterne de Kapa qui sera affiché sur les bonne tiles ensuite selon la pos de l'unit
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="unit"></param>
    List<Vector3Int> HandleKapaEvenDirPaternGen(Vector3Int dir, IUnit unit, IKapa tempKapa) => (dir.x, dir.y, dir.z) switch
        {
            (0, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.EvenNTiles),
            (1, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.EvenENTiles),
            (1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.EvenESTiles),
            (0, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.EvenSTiles),
            (-1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.EvenWSTiles),
            (-1, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGS, tempKapa.EvenWNTiles),
            _ => new()
        };
    #endregion

    /// <summary>
    /// Gère la selection des Kapas à la façon des Units, On doit selectionner à 2 fois une compétence avant de pouvoir l'executer.
    /// PENSER A AJOUTER LA METHODES POUR LA SELECTION DE LA DIRECTION DE LA COMPETENCE
    /// </summary>
    /// <param name="i"></param>
    public void HandleKapaSelect(int i)
    {
        if (_selectedUnit == null) return;
        if (!_selectedUnit.CanKapa) return;

        var type = SelectedUnit.UnitData.KapasList[i].KapaType;
        var kapa = SelectedUnit.UnitData.KapasList[i];

        //si switch sur autre Kapa
        if (CurrentTypeKapaSelected != KapaType.Default && IsKapaDirSelected)
        {
            kapa.OnDeselectTiles(HexGridStore.hGS, CurrentKapaPaternPos);
        }
        
        //supprimer l'outline de range 
        if (!IsKapaSelected && !SelectedUnit.IsPersoLocked)
        {
            ClearGraphKeepUnit();
        }

        //preselec Kapa
        if (!IsKapaSelected || CurrentTypeKapaSelected != type)
        {
            if (CurrentTypeKapaSelected != type)
            {
                try
                {
                    HideButtons(CurrentButtonPos);
                    CurrentButtonPos = GenerateButtonPos(SelectedUnit, kapa);
                }
                catch
                {
                    CurrentButtonPos = GenerateButtonPos(SelectedUnit, kapa);
                }
            }
            CurrentTypeKapaSelected = type;
            IsKapaSelected = true;
            IsKapaDirSelected = false;
            ShowButtons(CurrentButtonPos);

            if (_selectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].EffectType != EffectType.Hacked)
            {
                _selectedUnit.OnDeselectNetworkTiles();
            }

            return;
        }

        //Active Kapa With Bar Button
        if (CurrentKapaPaternPos != null || IsKapaSelected && CurrentTypeKapaSelected == type && IsKapaDirSelected || IsKapaSelected && CurrentButtonPos == null)
        {
            if (!OnCheckKapa(SelectedUnit, (int)CurrentTypeKapaSelected)) return;
            
            OnExecuteKapa(SelectedUnit, i, CurrentKapaPaternPos);
            FullResetKapAndPlayer();
        }
    }
    #region buttons graphs
    /// <summary>
    /// simple boucle d'AFFICHAGE GRAPH SEULEMENT des boutons autour d'une Unit
    /// </summary>
    /// <param name="butPos"></param>
    void ShowButtons(List<Vector3Int> butPos)
    {
        //verif si buttonPos existantes
        if (butPos == null) return;

        foreach (var i in butPos)
        {
            var j = HexGridStore.hGS.GetTile(i); j.EnableGlowButton();
        }
    }
    /// <summary>
    /// simple boucle de DESAFFICHAGE GRAPH SEULEMENT des boutons autour d'une Unit
    /// </summary>
    /// <param name="butPos"></param>
    static void HideButtons(List<Vector3Int> butPos)
    {
        if (butPos == null) return;
        
        foreach (var i in butPos)
        {
            var j = HexGridStore.hGS.GetTile(i); j.DisableGlowButton();
        }
    }
    #endregion

    #region Kapas resets
    /// <summary>
    /// Reset les datas stored sur les Kapas : 
    /// -> Bool isKapaSelected
    /// -> CurrentKapaType remis en Default
    /// </summary>
    void ResetKapaData()
    {
        HideButtons(CurrentButtonPos);
        CurrentTypeKapaSelected = KapaType.Default;
        CurrentSelectedKapaDir = KapaDir.Default;
        IsKapaSelected = false;
        IsKapaDirSelected = false;
        CurrentButtonPos = null;
        CurrentKapaPaternPos = null;
        CurrentDirSelected = Vector3Int.zero;
    }

    /// <summary>
    /// reset completement l'ensemble des datas d'une kapa pour switch sur une autre
    /// et celle du perso selected
    /// </summary>
    void FullResetKapAndPlayer()
    {
        //Kapa Reset
        HideButtons(CurrentButtonPos);
        CurrentTypeKapaSelected = KapaType.Default;
        CurrentSelectedKapaDir = KapaDir.Default;
        IsKapaSelected = false;
        IsKapaDirSelected = false;
        CurrentButtonPos = null;
        CurrentKapaPaternPos = null;
        CurrentDirSelected = Vector3Int.zero;
        //Unit Reset
        SelectedUnit.Deselect();
        SelectedUnit.IsPersoLocked = false;
        SelectedUnit.CanPlay = false;
        SelectedUnit = null;
        //GameLoop reset
        GameLoopManager.gLM.OnPlayerAction();
    }
    #endregion
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
    bool CheckIfCanSelectOtherUnitAndIfSameUnit(Entity unitRef)
    {
        //il n'y a pas d'unit pre-select
        if (_selectedUnit == null) return false;
        //On clique sur UNE AUTRE unit et la SELECTED UNIT n'est PAS LOCK
        else if (!_selectedUnit.IsPersoLocked && unitRef != _selectedUnit)
        {
            if (!IsKapaSelected)
            {
                ClearOldSelection();
                return false;
            }

            if (IsKapaDirSelected) { SelectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGS, CurrentKapaPaternPos); }
            ClearDataSelectionAvoidRange();
            ResetKapaData();
            
            //on inverse la sortie pour pouvoir continuer la methode de sélection des persos
            return false;
        }
        //On clique sur LA MEME Unit et elle est DEJA LOCK
        else if (_selectedUnit == unitRef && SelectedUnit.IsPersoLocked)
        {
            //feedbacks pour montrer que le perso doit faire une capa (ex : petit son de bip un peu techno des familles)
            return true;
        }
        //On clique sur LA MEME unit mais ELLE n'est PAS LOCK (Old CheckIfSameUnitSelected)
        else if (_selectedUnit == unitRef && !SelectedUnit.IsPersoLocked)
        {
            if (!IsKapaSelected)
            {
                ClearOldSelection();
                return true;
            }

            if (IsKapaDirSelected) { SelectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGS, CurrentKapaPaternPos); }
            ClearDataSelectionAvoidRange();
            ResetKapaData();
            
            return false;
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
        if (IsKapaSelected) { HandleKapaDirSelect(unitRef.CurrentHexPos, SelectedUnit); }
        return true;
    }

    /// <summary>
    /// force l'utilisation de capa ou skip capa du perso après son déplacement
    /// </summary>
    /// <param name="unit"></param>
    void LockUnitAfterMove()
    {
        SelectedUnit.IsPersoLocked = true;
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
        if (hexPos == SelectedUnit.CurrentHexPos)
        {
            SelectedUnit.Deselect();
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
        if (!_moveSys.IsHexInRange(hexPos)) { return true; }
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
        await Task.Delay(700);
        foreach (var u in FindObjectsOfType<Entity>())
        {
            Hex h = hex.GetTile(u.GetComponent<Entity>().CurrentHexPos);
            h.HasEntityOnIt = true;
            if (u.GetType() == typeof(Unit))
            {
                h.SetUnit(u.GetComponent<Unit>());
            }
        }
    }
    #endregion

    #region Clearing and Reset methodes
    /// <summary>
    /// reset le perso selectionné et les sélections graphiques
    /// </summary>
    void ClearOldSelection()
    {
        _previousSelectedHex = null;
        SelectedUnit.Deselect();
        _moveSys.HideRange(HexGridStore.hGS);
        _selectedUnit = null;
    }

    /// <summary>
    /// ne clear que la selection de datas unit et hex, pas de deselect graphic pour alleger la methode et eviter les NullRefExcep
    /// </summary>
    void ClearDataSelectionAvoidRange()
    {
        if (_selectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].EffectType == EffectType.Hacked)
        {
            _selectedUnit.OnDeselectNetworkTiles();
        }
        _previousSelectedHex = null;
        SelectedUnit.Deselect();
        _selectedUnit = null;
    }

    /// <summary>
    /// reset les selections graphiques mais GARDE LE SELECTED UNIT
    /// </summary>
    void ClearGraphKeepUnit()
    {
        _previousSelectedHex = null;
        _moveSys.HideRange(HexGridStore.hGS);
    }

    /// <summary>
    /// retire la selectedTile sur laquelle une Unit était et place la nouvelle tile en hasPlayerOnIt
    /// </summary>
    void ChargeNewUnitHexCoord()
    {
        HexGridStore.hGS.GetTile(SelectedUnit.CurrentHexPos).HasEntityOnIt = false;
        HexGridStore.hGS.GetTile(SelectedUnit.CurrentHexPos).ClearUnit();
        _previousSelectedHex.HasEntityOnIt = true;
        _previousSelectedHex.SetUnit(_selectedUnit);
        SelectedUnit.CurrentHexPos = _previousSelectedHex.HexCoords;
    }

    /// <summary>
    /// reset manuel de la gameloop temporaire
    /// </summary>
    public void ResetLoop()
    {
        PlayerTurn = true;
        foreach (GameObject u in GameObject.FindGameObjectsWithTag("Player")) { u.GetComponent<Unit>().CanPlay = true; }
    }
    #endregion
}