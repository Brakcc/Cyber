using System.Collections.Generic;
using Enums.UnitEnums.KapaEnums;
using FeedBacks;
using GameContent.GameManagement;
using GameContent.GridManagement;
using GameContent.GridManagement.HexPathFind;
using Interfaces.Kapas;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking
{
    public class UnitManager : MonoBehaviour
    {
        #region fields
        
        private MoveSystem _moveSys;
        [SerializeField] private VFXManager vFx;
        
        //Unit currently stored
        private IUnit SelectedUnit { get; set; }
        private bool _hasKapaBeenPlayed;
        
        //Hex Currently processed
        private Hex _previousSelectedHex;

        //Is a Kapa Utility, pas vraiment interessant de store directement une Kapa
        //on store directement une list de pattern et son Type, les methodes de kapas étant deja stored dans le cache AKapaSO
        private KapaType CurrentTypeKapaSelected { get; set; }
        private bool IsKapaSelected { get; set; }
        private bool IsKapaDirSelected { get; set; }
        private bool IsTargetSelected { get; set; }
        private List<Vector3Int> CurrentButtonPos {get; set; }
        private List<Vector3Int> CurrentKapaPatternPos { get; set; }  
        private Vector3Int CurrentDirSelected { get; set; }
        private Vector3Int CurrentTargetSelected { get; set; }

        #endregion

        #region Instance et Awake

        private void Awake()
        {
            _moveSys = new();
            SelectedUnit = null;
            _previousSelectedHex = null;
            CurrentTypeKapaSelected = KapaType.Default;
            IsKapaSelected = false;
            CurrentKapaPatternPos = null;
            CurrentButtonPos = null;
            _hasKapaBeenPlayed = false;
        }
        #endregion

        #region selections methodes (and update)
        
        /// <summary>
        /// Si l'unit peut jouer son tour : 2 possibilités :
        /// -> Si elle était déjà sélectionnée : ClearOldSelection
        /// -> Sinon PrepareUnitForMove
        /// </summary>
        /// <param name="unit"></param>
        public void HandleUnitSelected(GameObject unit)
        {
            var unitReference = unit.GetComponent<Unit>();
        
            if (CheckIfUnitCanPlay(unitReference)) 
                return;
            if (CheckIfCanSelectOtherUnitAndIfSameUnit(unitReference)) 
                return;

            PrepareUnitForMove(unitReference);
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
            if (SelectedUnit == null) 
                return;
            var selHex = selectedHex.GetComponent<Hex>();

            if (IsKapaSelected)
            {
                HandleKapaDirSelect(selHex.HexCoords, SelectedUnit);
                return;
            }
            if (SelectedUnit.IsPersoLocked && !_hasKapaBeenPlayed)
                return;
            if (HandleHexOutOfRange(selHex.HexCoords) || HandleSelectedHexIsUnitHex(selHex.HexCoords)) 
                return;

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
        private void PrepareUnitForMove(IUnit unitRef)
        {
            if (IsKapaSelected && IsKapaDirSelected)
            {
                unitRef.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGs, CurrentKapaPatternPos);
            }

            if (SelectedUnit != null && !IsKapaSelected)
            {
                ClearOldSelection(); 
                ResetKapaData();
            }
        
            SelectedUnit = unitRef;
            SelectedUnit.Select();
            _moveSys.ShowRange(SelectedUnit, HexGridStore.hGs);
        }

        private void OnPrepareForMoveAfterKapa(IUnit unitRef)
        {
            _moveSys.ShowRange(unitRef, HexGridStore.hGs);
            _hasKapaBeenPlayed = true;
        }

        /// <summary>
        /// Si Hex une nouvelle hex dans la range : new path glow
        /// / Si hex dej a select au bout du path : MovePlayer
        /// </summary>
        /// <param name="selects"></param>
        private void HandleTargetSelectedHex(Hex selects)
        {
            if (_previousSelectedHex == null || _previousSelectedHex != selects)
            {
                _previousSelectedHex = selects;
                _moveSys.ShowPath(selects.HexCoords, HexGridStore.hGs);
                SelectedUnit.OnDeselectSelfTile(SelectedUnit, HexGridStore.hGs);
            }
            else
            {
                ChargeNewUnitHexCoord();
                _moveSys.MoveUnit(SelectedUnit, HexGridStore.hGs);
                ClearGraphKeepUnit();
                if (_hasKapaBeenPlayed)
                {
                    FullResetKapaAndUnit();
                    return;
                }
                LockUnitAfterAction();
            }
        }

        private void SoftSkipMove()
        {
            //Considere les actions de deplacement null apres l'utilisation de la Kapa
            var tempSelectedTile = HexGridStore.hGs.GetTile(SelectedUnit.CurrentHexPos);

            if (_previousSelectedHex == null || _previousSelectedHex != tempSelectedTile)
            {
                _previousSelectedHex = tempSelectedTile;
                SelectedUnit.OnSelectSelfTile(SelectedUnit, HexGridStore.hGs);
                _moveSys.HidePath(HexGridStore.hGs);
            }
            else
            {
                ChargeNewUnitHexCoord();
                SelectedUnit.OnDeselectSelfTile(SelectedUnit, HexGridStore.hGs);
                ClearGraphKeepUnit();
                FullResetKapaAndUnit();
            }
        }
        
        #endregion
        
        #region KapasCalls
        
        #region PreBuild methodes

        private static List<Vector3Int> GenerateButtonPos(IUnit unit, IKapa kapa) 
            => kapa.OnGenerateButton(HexGridStore.hGs, unit);

        private static void OnExecuteKapa(IUnit unit, int i, List<Vector3Int> kapaPatternPos)
            => unit.UnitData.KapasList[i].OnExecute(HexGridStore.hGs, kapaPatternPos, unit, false);

        private static bool OnCheckKapa(IUnit unit, int i) 
            => unit.UnitData.KapasList[i].OnCheckKapaPoints(unit);
        
        #endregion

        #region Encapsuls

        private void TryKapaActiveOnMap(IUnit uRef, Vector3Int butPos, Vector3Int dir, out bool active)
        {
            if (IsTargetSelected && CurrentTargetSelected == butPos && IsRangeAtk(uRef, CurrentTypeKapaSelected))
            {
                if (OnCheckKapa(uRef, (int)CurrentTypeKapaSelected))
                {
                    OnExecuteKapa(uRef, (int)CurrentTypeKapaSelected, CurrentKapaPatternPos);
                    active = true;
                    ResetAfterKapa(uRef);
                    return;
                }
            }

            if (IsKapaDirSelected && CurrentDirSelected == dir)
            {
                if (OnCheckKapa(uRef, (int)CurrentTypeKapaSelected))
                {
                    OnExecuteKapa(uRef, (int)CurrentTypeKapaSelected, CurrentKapaPatternPos);
                    active = true;
                    ResetAfterKapa(uRef);
                    return;
                }
            }

            active = false;
        }

        private void ResetAfterKapa(IUnit uRef)
        {
            switch (uRef.IsPersoLocked)
            {
                case true:
                    FullResetKapaAndUnit();
                    break;
                        
                case false:
                    ResetKapaData();
                    LockUnitAfterAction(); 
                    OnPrepareForMoveAfterKapa(uRef);
                    break;
            }
        }
        
        private void OnActiveKapaWithButton(IUnit uRef, int index, KapaType type, List<Vector3Int> currentPatternPos)
        {
            if (!OnCheckKapa(uRef, (int)type)) return;

            switch (IsAoe(uRef, type))
            {
                case true:
                    OnExecuteKapa(uRef, index, uRef.GlobalNetwork);
                    break;
                case false:
                    OnExecuteKapa(uRef, index, currentPatternPos);
                    break;
            }
            
            ResetAfterKapa(uRef);
        }

        private void OnGarbageSelectionDir(IUnit uRef)
        {
            uRef.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGs, CurrentKapaPatternPos);
        }

        private void OnTargetSelection(IUnit uRef, Vector3Int butPos)
        {
            CurrentKapaPatternPos = HandleKapaSelectOnRange(butPos, uRef,
                uRef.UnitData.KapasList[(int)CurrentTypeKapaSelected]);
            IsTargetSelected = true;
            CurrentTargetSelected = butPos;
        }

        private void OnDirSelection(IUnit uRef, Vector3Int dir)
        {
            CurrentKapaPatternPos = Direction.IsPariryEven(uRef.CurrentHexPos.x)
                ? HandleKapaEvenDirPatternGen(dir, uRef, uRef.UnitData.KapasList[(int)CurrentTypeKapaSelected])
                : HandleKapaOddDirPatternGen(dir, uRef, uRef.UnitData.KapasList[(int)CurrentTypeKapaSelected]);
            IsKapaDirSelected = true;
            CurrentDirSelected = dir;
        }

        private void OnSwitchKapaSelection(IKapa kapa)
        {
            if (CurrentTypeKapaSelected != KapaType.Default && (IsKapaDirSelected || IsTargetSelected))
                kapa.OnDeselectTiles(HexGridStore.hGs, CurrentKapaPatternPos);
            
            switch (IsKapaSelected)
            {
                case true:
                {
                    if (IsAoe(SelectedUnit, CurrentTypeKapaSelected))
                    {
                        SelectedUnit.OnDeselectNetworkTiles();
                    }
                    break;
                }
                
                //supprimer l'outline de range 
                case false when !SelectedUnit.IsPersoLocked:
                    ClearGraphKeepUnit();
                    break;
            }
            
            if (CurrentTypeKapaSelected != kapa.KapaType)
                SoftResetKapaData();
        }

        private void OnPreselecKapa(IKapa kapa, KapaType type)
        {
            if (IsAoe(SelectedUnit, type))
            {
                if (IsKapaSelected)
                {
                    HideButtons(CurrentButtonPos);
                }
                SelectedUnit.OnSelectNetworkTiles();
                CurrentTypeKapaSelected = type;
                IsKapaSelected = true;
                IsKapaDirSelected = false;
                return;
            }
                
            HideButtons(CurrentButtonPos);
            CurrentButtonPos = GenerateButtonPos(SelectedUnit, kapa);
                
            CurrentTypeKapaSelected = type;
            IsKapaSelected = true;
            IsKapaDirSelected = false;
            ShowButtons(CurrentButtonPos);
        }

        #endregion
        
        /// <summary>
        /// Basé sur la pos du l'unit et celle de la tile cliquée, on trace un VECTEUR entre la tile et la selectedUnit
        /// pour connaitre la dir selon la parité
        /// Reset de la direction deja selectionnée PUIS selection de la nouvelle direction
        /// -> modif la liste de tiles qui seront affectées par la kapa; la Kapa en elle meme est stockée grace à la kapatype,
        /// selectedUnit et kapalist de la selectedUnit
        /// -> refresh les affichages de boutons
        /// </summary>
        /// <param name="buttonPos"></param>
        /// <param name="unitRef"></param>
        private void HandleKapaDirSelect(Vector3Int buttonPos, IUnit unitRef)
        {
            if (IsAoe(unitRef, CurrentTypeKapaSelected))
                return;
            var dir = buttonPos - unitRef.CurrentHexPos;
            
            if (HandleHexOutOfButton(buttonPos)) 
                return;

            vFx.OnGenerateParticlesSys(HexGridStore.hGs.GetTile(buttonPos).transform.position);
            
            //Active la Kapa avec Click sur Map
            if (IsKapaSelected  && CurrentKapaPatternPos != null)
            {
                TryKapaActiveOnMap(unitRef, buttonPos, dir, out var active);
                if (active)
                    return;
            }

            //Garbage de la selection de Dir precedente sur la map 
            if (IsKapaDirSelected || IsTargetSelected)
            {
                OnGarbageSelectionDir(unitRef);
            }
            
            //selection de position unique dans une range de Map
            if (IsRangeAtk(unitRef, CurrentTypeKapaSelected))
            {
                OnTargetSelection(unitRef, buttonPos);
                return;
            }
            
            //New selection de direction
            OnDirSelection(unitRef, dir);
        }

        /// <summary>
        /// Gère la selection des Kapas à la façon des Units, On doit selectionner à 2 fois une compétence avant de pouvoir l'executer.
        /// </summary>
        /// <param name="i"></param>
        public void HandleKapaSelect(int i)
        {
            if (SelectedUnit == null)
                return;
            if (!SelectedUnit.CanKapa || _hasKapaBeenPlayed)
                return;

            var type = SelectedUnit.UnitData.KapasList[i].KapaType;
            var kapa = SelectedUnit.UnitData.KapasList[i];
            
            //si switch sur autre Kapa
            OnSwitchKapaSelection(kapa);

            //preselec Kapa
            if (!IsKapaSelected || CurrentTypeKapaSelected != type)
            {
                OnPreselecKapa(kapa, type);
                return;
            }

            //Active Kapa With Bar Button
            if (CanActivateKapaWithButtons(SelectedUnit, type))
            {
                OnActiveKapaWithButton(SelectedUnit, i, CurrentTypeKapaSelected, CurrentKapaPatternPos);
            }
        }
        
        #region Patern Application

        /// <summary>
        /// pour l'unit sur une case IMPAIRE, revoit le bon paterne de Kapa qui sera affiché sur les bonne tiles ensuite selon la pos de l'unit
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="unit"></param>
        /// <param name="tempKapa"></param>
        private static List<Vector3Int> HandleKapaOddDirPatternGen(Vector3Int dir, IUnit unit, IKapa tempKapa) 
            => (dir.x, dir.y, dir.z) switch
        {
            (0, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.OddNTiles),
            (1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.OddENTiles),
            (1, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.OddESTiles),
            (0, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.OddSTiles),
            (-1, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.OddWSTiles),
            (-1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.OddWNTiles),
            _ => new List<Vector3Int>()
        };
        /// <summary>
        /// pour l'unit sur une case PAIRE, revoit le bon paterne de Kapa qui sera affiché sur les bonne tiles ensuite selon la pos de l'unit
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="unit"></param>
        /// <param name="tempKapa"></param>
        private static List<Vector3Int> HandleKapaEvenDirPatternGen(Vector3Int dir, IUnit unit, IKapa tempKapa) 
            => (dir.x, dir.y, dir.z) switch
        {
            (0, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.EvenNTiles),
            (1, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.EvenENTiles),
            (1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.EvenESTiles),
            (0, -1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.EvenSTiles),
            (-1, 0, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.EvenWSTiles),
            (-1, 1, 0) => tempKapa.OnSelectGraphTiles(unit, HexGridStore.hGs, tempKapa.EvenWNTiles),
            _ => new List<Vector3Int>()
        };

        private static List<Vector3Int> HandleKapaSelectOnRange(Vector3Int pos, IUnit tempUnit, IKapa tempKapa)
            => tempKapa.OnSelectGraphTiles(tempUnit, HexGridStore.hGs, new[] { pos });
        
        #endregion

        #region buttons graphs
        
        /// <summary>
        /// simple boucle d'AFFICHAGE GRAPH SEULEMENT des boutons autour d'une Unit
        /// </summary>
        /// <param name="butPos"></param>
        private static void ShowButtons(IReadOnlyList<Vector3Int> butPos)
        {
            //verif si buttonPos existantes
            if (butPos == null) return;

            foreach (var i in butPos)
            {
                var j = HexGridStore.hGs.GetTile(i); 
                j.EnableGlowButton();
            }
        }
        /// <summary>
        /// simple boucle de DESAFFICHAGE GRAPH SEULEMENT des boutons autour d'une Unit
        /// </summary>
        /// <param name="butPos"></param>
        private static void HideButtons(IReadOnlyList<Vector3Int> butPos)
        {
            if (butPos == null) return;
        
            foreach (var i in butPos)
            {
                var j = HexGridStore.hGs.GetTile(i); 
                j.DisableGlowButton();
            }
        }
        
        #endregion

        #region Kapas resets
        
        /// <summary>
        /// Reset les datas stored sur les Kapas : 
        /// -> Bool isKapaSelected
        /// -> CurrentKapaType remis en Default
        /// </summary>
        private void ResetKapaData()
        {
            if (IsKapaDirSelected || IsTargetSelected) 
                SelectedUnit.UnitData.KapasList[(int)CurrentTypeKapaSelected].OnDeselectTiles(HexGridStore.hGs, CurrentKapaPatternPos);
            if (IsHack(SelectedUnit, CurrentTypeKapaSelected))
                SelectedUnit.OnDeselectNetworkTiles();
            HideButtons(CurrentButtonPos);
            CurrentTypeKapaSelected = KapaType.Default;
            IsKapaSelected = false;
            IsKapaDirSelected = false;
            IsTargetSelected = false;
            CurrentButtonPos = null;
            CurrentKapaPatternPos = null;
            CurrentDirSelected = Vector3Int.zero;
            CurrentTargetSelected = Vector3Int.zero;
        }

        private void SoftResetKapaData()
        {
            IsKapaDirSelected = false;
            IsTargetSelected = false;
            CurrentKapaPatternPos = null;
            CurrentDirSelected = Vector3Int.zero;
            CurrentTargetSelected = Vector3Int.zero;
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
        private bool CheckIfCanSelectOtherUnitAndIfSameUnit(IEntity unitRef)
        {
            //il n'y a pas d'unit pre-select
            if (SelectedUnit == null) return false;
            
            //On clique sur UNE AUTRE unit et la SELECTED UNIT n'est PAS LOCK
            if (!SelectedUnit.IsPersoLocked && unitRef != SelectedUnit)
            {
                if (!IsKapaSelected)
                {
                    ClearOldSelection();
                    return false;
                }
                
                ResetKapaData();
                ClearDataSelectionAvoidRange();
            
                //on inverse la sortie pour pouvoir continuer la methode de sélection des persos
                return false;
            }
            
            //On clique sur LA MEME Unit et elle est DEJA LOCK
            if (SelectedUnit == unitRef && SelectedUnit.IsPersoLocked)
            {
                if (!_hasKapaBeenPlayed)
                    return true;
                
                SoftSkipMove();
                return true;
            }

            //On clique sur LA MEME unit mais ELLE n'est PAS LOCK (Old CheckIfSameUnitSelected)
            if (SelectedUnit == unitRef && !SelectedUnit.IsPersoLocked)
            {
                if (!IsKapaSelected)
                {
                    ClearOldSelection();
                    return true;
                }
                
                ResetKapaData();
                ClearDataSelectionAvoidRange();
                return false;
            }
            
            //On clique sur UNE AUTRE Unit mais la SELECTED UNIT est DEJA LOCKED
            
            //on inverse la sortie pour pouvoir continuer la methode de sélection des persos
            //ATENTION !!!!! dans le cas particulier ou une Kapa vise une Unit au contact, on skip la selection de
            //l'Unit mais on la selection de kapaDir à la pos de l'Unit visée
            if (IsKapaSelected)
            {
                HandleKapaDirSelect(unitRef.CurrentHexPos, SelectedUnit);
            }
            return true;
        }

        /// <summary>
        /// verif si le perso n'est pas mort et que son tour n'est pas passé
        /// </summary>
        /// <param name="unitRef"></param>
        /// <returns></returns>
        private bool CheckIfUnitCanPlay(IUnit unitRef)
        {
            //Si l'unit sélectionnee peut faire son tour ET n'est pas morte
            if (unitRef.CanPlay && !unitRef.IsDead)
            {
                //condition inversée pour continuer
                return false; 
            }
            //feedbacks un peu sad mais electro quand meme
            if (IsKapaSelected)
            {
                HandleKapaDirSelect(unitRef.CurrentHexPos, SelectedUnit);
            }
            return true;
        }

        /// <summary>
        /// force l'utilisation de kapa ou skip kapa du perso après son déplacement
        /// </summary>
        private void LockUnitAfterAction()
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
        private bool HandleSelectedHexIsUnitHex(Vector3Int hexPos)
        {
            if (hexPos != SelectedUnit.CurrentHexPos)
                return false;
            
            SelectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }

        /// <summary>
        /// L'hex sélectionnée est ou non hors de portée
        /// </summary>
        /// <param name="hexPos"></param>
        /// <returns></returns>
        private bool HandleHexOutOfRange(Vector3Int hexPos) => !_moveSys.IsHexInRange(hexPos);

        /// <summary>
        /// vérifie si la tile selectionnée est un bouton autour du player ou juste une case sans action
        /// </summary>
        /// <param name="hexPos"></param>
        /// <returns></returns>
        private bool HandleHexOutOfButton(Vector3Int hexPos)
        {
            if (CurrentButtonPos == null) return true;
            return !CurrentButtonPos.Contains(hexPos);
        }
        
        #endregion

        #region Clearing and Reset methodes
        
        /// <summary>
        /// reset le perso selectionné et les sélections graphiques
        /// </summary>
        private void ClearOldSelection()
        {
            _previousSelectedHex = null;
            SelectedUnit.Deselect();
            _moveSys.HideRange(HexGridStore.hGs);
            SelectedUnit = null;
        }

        /// <summary>
        /// ne clear que la selection de datas unit et hex, pas de deselect graphic pour alleger la methode et eviter les NullRefExcep
        /// </summary>
        private void ClearDataSelectionAvoidRange()
        {
            
            if (IsHack(SelectedUnit, CurrentTypeKapaSelected))
            {
                SelectedUnit.OnDeselectNetworkTiles();
            }
            _previousSelectedHex = null;
            SelectedUnit.Deselect();
            SelectedUnit = null;
        }

        /// <summary>
        /// reset les selections graphiques mais GARDE LE SELECTED UNIT
        /// </summary>
        private void ClearGraphKeepUnit()
        {
            _previousSelectedHex = null;
            _moveSys.HideRange(HexGridStore.hGs);
        }

        /// <summary>
        /// retire la selectedTile sur laquelle une Unit était et place la nouvelle tile en hasPlayerOnIt
        /// </summary>
        private void ChargeNewUnitHexCoord()
        {
            HexGridStore.hGs.GetTile(SelectedUnit.CurrentHexPos).HasEntityOnIt = false;
            HexGridStore.hGs.GetTile(SelectedUnit.CurrentHexPos).ClearEntity();
            _previousSelectedHex.HasEntityOnIt = true;
            _previousSelectedHex.SetEntity(SelectedUnit);
            SelectedUnit.CurrentHexPos = _previousSelectedHex.HexCoords;
        }

        /// <summary>
        /// reset completement l'ensemble des datas d'une kapa pour switch sur une autre
        /// et celle du perso selected
        /// </summary>
        private void FullResetKapaAndUnit()
        {
            //Kapa Reset
            _hasKapaBeenPlayed = false;
            
            HideButtons(CurrentButtonPos);
            CurrentTypeKapaSelected = KapaType.Default;
            IsKapaSelected = false;
            IsKapaDirSelected = false;
            IsTargetSelected = false;
            CurrentButtonPos = null;
            CurrentKapaPatternPos = null;
            CurrentDirSelected = Vector3Int.zero;
            CurrentTargetSelected = Vector3Int.zero;
            //Unit Reset
            SelectedUnit.Deselect();
            SelectedUnit.IsPersoLocked = false;
            SelectedUnit.CanPlay = false;
            SelectedUnit = null;
            //GameLoop reset
            GameLoopManager.gLm.OnPlayerAction();
        }
        
        /// <summary>
        /// reset manuel de la gameloop temporaire, pure fonction de debug
        /// </summary>
        public static void ResetLoop()
        {
            foreach (var u in GameObject.FindGameObjectsWithTag("Player")) { u.GetComponent<Unit>().CanPlay = true; }
        }
        #endregion

        #region Checkers

        private static bool IsRangeAtk(IUnit uRef, KapaType type)
            => uRef.UnitData.KapasList[(int)type].EffectType == EffectType.Hack
               && uRef.UnitData.KapasList[(int)type].KapaFunctionType != KapaFunctionType.AOE
               && uRef.UnitData.KapasList[(int)type].KapaFunctionType != KapaFunctionType.DOT
               || uRef.UnitData.KapasList[(int)type].KapaFunctionType == KapaFunctionType.ThrowFreeArea
               || uRef.UnitData.KapasList[(int)type].KapaFunctionType == KapaFunctionType.ThrowLimit;

        private static bool IsSkip(IUnit uRef, KapaType type)
            => uRef.UnitData.KapasList[(int)type].KapaType == KapaType.Skip;

        private static bool IsDeAct(IUnit uRef, KapaType type)
            => uRef.UnitData.KapasList[(int)type].KapaType == KapaType.DeAct;
        
        private static bool IsAoe(IUnit uRef, KapaType type)
            => uRef.UnitData.KapasList[(int)type].EffectType == EffectType.Hack &&
               (uRef.UnitData.KapasList[(int)type].KapaFunctionType == KapaFunctionType.AOE 
               || uRef.UnitData.KapasList[(int)type].KapaFunctionType == KapaFunctionType.DOT);

        private static bool IsHack(IUnit uRef, KapaType type)
            => type != KapaType.Default && uRef.UnitData.KapasList[(int)type].EffectType == EffectType.Hack;

        private bool CanActivateKapaWithButtons(IUnit uRef, KapaType type)
            => CurrentKapaPatternPos != null ||
               IsKapaSelected && CurrentTypeKapaSelected == type && IsKapaDirSelected ||
               IsKapaSelected && CurrentTypeKapaSelected == type && IsDeAct(uRef, type) ||
               IsKapaSelected && CurrentButtonPos == null && (IsAoe(uRef, type) || IsSkip( uRef, type));

        #endregion
    }
}