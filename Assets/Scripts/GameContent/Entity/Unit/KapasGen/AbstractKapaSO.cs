using System.Collections.Generic;
using System.Threading.Tasks;
using Enums.UnitEnums.KapaEnums;
using Enums.UnitEnums.UnitEnums;
using FeedBacks;
using GameContent.GridManagement;
using Interfaces.Kapas;
using Interfaces.Unit;
using UI.InGameUI;
using UnityEngine;
using Utilities.CustomHideAttribute;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Buff_Debuff;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Dash;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.DoubleDiffAtk;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.Grab_Push;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.PerfoAtk;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.AOEDistAtk;
using GameContent.Entity.Unit.KapasGen.KapaFunctions.DOTAtk;
using GameContent.GridManagement.HexPathFind;
using Random = UnityEngine.Random;

namespace GameContent.Entity.Unit.KapasGen
{
    public abstract class AbstractKapaSO : ScriptableObject, IKapa
    {
        #region fields to herit

        public int ID => iD;
        [SerializeField] private int iD;
        
        public string KapaName => kapaNane;
        [SerializeField] private string kapaNane;
        
        public string Description => description;
        [SerializeField] [TextArea(4, 5)] private string description;
        
        public int MaxPlayerPierce => maxPlayerpierce;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private int maxPlayerpierce;
        
        public int BalanceMult => balanceMult;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [Range(0, 10)] [SerializeField] private int balanceMult = 1;
        
        public KapaType KapaType => kapaType;
        [SerializeField] private KapaType kapaType;
        
        public EffectType EffectType => effectType;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private EffectType effectType;
        
        #region BuffDebuff
        
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private bool hasBuffDebuffs;
        
        [ShowIfBoolTrue("hasBuffDebuffs")]
        [SerializeField] private FullBuffDebuff buffDebuffDatas;
        
        #endregion
        
        public KapaFunctionType KapaFunctionType => kapaFunctionType;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private KapaFunctionType kapaFunctionType;
        
        //public abstract KapaUISO KapaUI { get; }
        public abstract GameObject DamageFeedBack { get; }
        protected abstract VFXManager VFx { get; } 
        public abstract Vector3Int[] Patterns { get; }
        
        #region Diff Kapas fields
        
        #region Double Diff Atk
        
        [ShowIfTrue("kapaFunctionType", new[] { (int)KapaFunctionType.DoubleDiffAttack })]
        [SerializeField] private DoubleDiffAtkKapaDatas doubleDiffAtkDatas;
        
        #endregion

        #region First Hit Effect

        [ShowIfTrue("kapaFunctionType", new[] { (int)KapaFunctionType.FirstHitEffect })]
        [SerializeField] private PerfoHitEffects firstHitEffectsData;

        #endregion

        #region AOE Free throw

        [ShowIfTrue("kapaFunctionType", new[] {(int)KapaFunctionType.ThrowFreeArea})]
        [SerializeField] private AoeFreeAreaDatas freeThrowAreaDatas;

        #endregion
        
        #region AOE Free throw

        [ShowIfTrue("kapaFunctionType", new[] {(int)KapaFunctionType.ThrowLimit})]
        [SerializeField] private AoeLimitedThrowDatas limitedThrowAreaDatas;

        #endregion

        #region DOT

        [ShowIfTrue("kapaFunctionType", new[] {(int)KapaFunctionType.DOT})]
        [SerializeField] private DotKapaDatas dotKapaDatas;

        #endregion
        
        #endregion
        
        #endregion
        
        #region paterns to herit
        
        //North tiles
        public abstract Vector3Int[] OddNTiles { get; protected set; }
        public abstract Vector3Int[] EvenNTiles { get; protected set; }
        //EN tiles
        public abstract Vector3Int[] OddENTiles { get; protected set; }
        public abstract Vector3Int[] EvenENTiles { get; protected set; }
        //ES tiles
        public abstract Vector3Int[] OddESTiles { get; protected set; }
        public abstract Vector3Int[] EvenESTiles { get; protected set; }
        //S tiles
        public abstract Vector3Int[] OddSTiles { get; protected set; }
        public abstract Vector3Int[] EvenSTiles { get; protected set; }
        //WS tiles
        public abstract Vector3Int[] OddWSTiles { get; protected set; }
        public abstract Vector3Int[] EvenWSTiles { get; protected set; }
        //WN tiles
        public abstract Vector3Int[] OddWNTiles { get; protected set; }
        public abstract Vector3Int[] EvenWNTiles { get; protected set; }
        
        #endregion

        #region patern gen cache
        
        #region Ntiles
        
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Nord, tuiles Impaires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetOddNtiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(pat[i].x, pat[i].y, pat[i].z);
                w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
            }
            return w;
        }
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Nord, tuiles Paires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetEvenNtiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(pat[i].x, pat[i].y, pat[i].z);
                w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
            }
            return w;
        }
        
        #endregion
        
        #region ENtiles
        
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Nord Est, tuiles Impaires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetOddENtiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(-pat[i].z, -pat[i].x, -pat[i].y);
                w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
            }
            return w;
        }
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Nord Est, tuiles Paires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetEvenENtiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(-pat[i].z, -pat[i].x, -pat[i].y);
                w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
            }
            return w;
        }
        
        #endregion
        
        #region WNtiles
        
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Nord Ouest, tuiles Impaires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetOddWNtiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(-pat[i].y, -pat[i].z, -pat[i].x);
                w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
            }
            return w;
        }
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Nord Ouest, tuiles Paires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetEvenWNtiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(-pat[i].y, -pat[i].z, -pat[i].x);
                w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
            }
            return w;
        }
        
        #endregion
        
        #region Stiles
        
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Sud, tuiles Impaires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetOddStiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(-pat[i].x, -pat[i].y, -pat[i].z);
                w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
            }
            return w;
        }
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Sud, tuiles Paires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetEvenStiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(-pat[i].x, -pat[i].y, -pat[i].z);
                w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
            }
            return w;
        }
        
        #endregion
        
        #region EStiles
        
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Sud Est, tuiles Impaires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetOddEStiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(pat[i].y, pat[i].z, pat[i].x);
                w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
            }
            return w;
        }
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Sud Est, tuiles Paires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetEvenEStiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(pat[i].y, pat[i].z, pat[i].x);
                w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
            }
            return w;
        }
        
        #endregion
        
        #region WStiles
        
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Sud Ouest, tuiles Impaires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetOddWStiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(pat[i].z, pat[i].x, pat[i].y);
                w[i] = await HexToOrthoCoords.GetOddOrthoCoord(j);
            }
            return w;
        }
        /// <summary>
        /// génère l'array de coord Unity des Kapas vers le Sud Ouest, tuiles Paires
        /// </summary>
        /// <param name="pat"></param>
        /// <returns></returns>
        private static async Task<Vector3Int[]> GetEvenWStiles(IReadOnlyList<Vector3Int> pat)
        {
            var w = new Vector3Int[pat.Count];
            for (var i = 0; i < pat.Count; i++)
            {
                Vector3Int j = new(pat[i].z, pat[i].x, pat[i].y);
                w[i] = await HexToOrthoCoords.GetEvenOrthoCoord(j);
            }
            return w;
        }
        
        #endregion
        
        #endregion

        #region methodes to herit
        /// <summary>
        /// initialise tous les paternes de Kapas dans toutes les directions
        /// </summary>
        /// <param name="p"></param>
        public virtual async void InitPatterns(Vector3Int[] p)
        {
            //NTiles
            OddNTiles = await GetOddNtiles(p);
            EvenNTiles = await GetEvenNtiles(p);
            //WNTiles
            OddWNTiles = await GetOddWNtiles(p);
            EvenWNTiles = await GetEvenWNtiles(p);
            //ENTiles
            OddENTiles = await GetOddENtiles(p);
            EvenENTiles = await GetEvenENtiles(p);
            //STiles
            OddSTiles = await GetOddStiles(p);
            EvenSTiles = await GetEvenStiles(p);
            //ESTiles
            OddESTiles = await GetOddEStiles(p);
            EvenESTiles = await GetEvenEStiles(p);
            //WSTiles
            OddWSTiles = await GetOddWStiles(p);
            EvenWSTiles = await GetEvenWStiles(p);
        }

        /// <summary>
        /// verifie si les points de comp et d'ult sont suffisants pour lancer une Kapa.
        /// Dans le cas contraire, un refus de Kapa se fait.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public abstract bool OnCheckKapaPoints(IUnit unit);

        /// <summary>
        /// Base Logique de l'execution de Kapa et verif du hit de la Kapa pour eviter la gen de Comp Point en tirant dans le vide
        /// Surtout Utile dans le call de la NAKapa
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="pattern">Prendre le pattern du UnitManager CurrentPatternPos</param>
        /// <param name="unit"></param>
        /// <param name="fromUnit"></param>
        /// <param name="isHitting"></param>
        public virtual void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern , IUnit unit, bool fromUnit, out bool isHitting)
        {
            //Init des mutable local
            isHitting = false;
            var n = 0;
            var canDoubleKapa = true;
            List<Vector3Int> patternToUse;
            var canFirstEffect = true;
            
            //Changement de Pattern, ne considère que le point de base au centre comme pattern 1
            ChangePatterns:
            if (KapaFunctionType == KapaFunctionType.DoubleDiffAttack && doubleDiffAtkDatas.hasDiffPatterns && canDoubleKapa)
            {
                patternToUse = new List<Vector3Int>{ pattern[0] };
            }
            else
            {
                patternToUse = pattern;
            }
            
            //Si Dot lezgo Dot
            if (KapaFunctionType == KapaFunctionType.DOT && !fromUnit)
            {
                DotKapa.OnStartDot(unit, dotKapaDatas.turnNumber);
            }
            
            foreach (var pos in patternToUse)
            {
                //verif s'il y a joueur uniquement sur les case du pattern
                var hex = hexGrid.GetTile(pos);
                if (!hex.HasEntityOnIt)
                {
                    //Si pas de target mais Diff patterns, relance l'atk
                    if (KapaFunctionType == KapaFunctionType.DoubleDiffAttack && 
                        doubleDiffAtkDatas.hasDiffPatterns && canDoubleKapa)
                    {
                        canDoubleKapa = false;
                        goto ChangePatterns;
                    }
                    continue;
                }
                var unitTarget = hex.GetEntity<IUnit>();

                //Verif de si l'Entity est une Unit
                if (unitTarget == null)
                    continue;
                
                //Verif si l'Unit est de meme team
                if (unitTarget.TeamNumber == unit.TeamNumber)
                {
                    //Buff s'il y a buff
                    if (hasBuffDebuffs && buffDebuffDatas.isBuff)
                    {
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                    }
                    continue;
                }
                
                //Verif si Unit deja ded
                if (unitTarget.IsDead)
                    continue;

                Retake:
                //verif de la precision
                if (Random.Range(0, 100) > unit.CurrentPrecision + 1)
                    continue;

                //Verif du delay de la consideration d'atk
                var delayAtk = KapaFunctionType == KapaFunctionType.DoubleDiffAttack && !canDoubleKapa || 
                               KapaFunctionType == KapaFunctionType.Dash || 
                               KapaFunctionType == KapaFunctionType.Grab
                    ? Constants.SecondAtkDelay
                    : 0;
                
                //Verif des possibles KapaFunctions pour application des degats et debuffs
                switch (kapaFunctionType)
                {
                    case KapaFunctionType.Dash:
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            buffDebuffDatas.buffDebuffList.hasBalanceMultBDb
                                ? buffDebuffDatas.buffDebuffList.balMultBuffDebuffData
                                : BalanceMult, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        DashKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        break;
                    
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasDashAfterKapa && canDoubleKapa:
                        canDoubleKapa = false;
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            doubleDiffAtkDatas.doubleABuffDebuff.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        DashKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        goto Retake;
                        
                    case KapaFunctionType.Grab:
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            buffDebuffDatas.buffDebuffList.hasBalanceMultBDb
                                ? buffDebuffDatas.buffDebuffList.balMultBuffDebuffData
                                : BalanceMult, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        GrabKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        break;
                    
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasGrabAfterKapa && canDoubleKapa:
                        canDoubleKapa = false;
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            doubleDiffAtkDatas.doubleABuffDebuff.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        GrabKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        goto Retake;
                        
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasDiffPatterns && canDoubleKapa:
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            doubleDiffAtkDatas.doubleABuffDebuff.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        canDoubleKapa = false;
                        goto ChangePatterns;
                        
                    case KapaFunctionType.DoubleDiffAttack when !canDoubleKapa:
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            buffDebuffDatas.buffDebuffList.hasBalanceMultBDb
                                ? buffDebuffDatas.buffDebuffList.balMultBuffDebuffData
                                : BalanceMult, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, doubleDiffAtkDatas.doubleABuffDebuff);
                        break;
                        
                    case KapaFunctionType.ThrowFreeArea when canFirstEffect && unitTarget.CurrentHexPos == pattern[0]:
                        OnBuffDebuffConsideration(unitTarget, freeThrowAreaDatas.centerDebuffList);
                        OnDamageConsideration(this, unit, unitTarget,
                            freeThrowAreaDatas.centerDebuffList.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        canFirstEffect = false;
                        break;

                    case KapaFunctionType.ThrowLimit when canFirstEffect && unitTarget.CurrentHexPos == pattern[0]:
                        OnBuffDebuffConsideration(unitTarget, limitedThrowAreaDatas.centerDebuffList);
                        OnDamageConsideration(this, unit, unitTarget,
                            limitedThrowAreaDatas.centerDebuffList.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        canFirstEffect = false;
                        break;
                        
                    case KapaFunctionType.DOT:
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        OnDamageConsideration(this, unit, unitTarget,
                            dotKapaDatas.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        break;
                    
                    case KapaFunctionType.FirstHitEffect when canFirstEffect:
                        OnBuffDebuffConsideration(unitTarget, firstHitEffectsData.buffDebuffList);
                        OnDamageConsideration(this, unit, unitTarget,
                            firstHitEffectsData.buffDebuffList.balMultBuffDebuffData, delayAtk,
                            DamageFeedBack);
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        canFirstEffect = false;
                        break;
                    
                    case KapaFunctionType.Default:
                        OnDamageConsideration(this, unit, unitTarget,
                            buffDebuffDatas.buffDebuffList.hasBalanceMultBDb
                                ? buffDebuffDatas.buffDebuffList.balMultBuffDebuffData
                                : BalanceMult, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        break;
                    
                    case KapaFunctionType.AOE:
                    default:
                        VFx.OnGenerateParticlesSys(unitTarget.CurrentWorldPos);
                        OnDamageConsideration(this, unit, unitTarget,
                            buffDebuffDatas.buffDebuffList.hasBalanceMultBDb
                                ? buffDebuffDatas.buffDebuffList.balMultBuffDebuffData
                                : BalanceMult, delayAtk,
                            DamageFeedBack);
                        OnBuffDebuffConsideration(unitTarget, buffDebuffDatas.buffDebuffList);
                        break;
                }
                
                //compteur de Hit
                n++;
            }

            if (n > 0)
                isHitting = true;

            OnDeselectTiles(hexGrid, pattern);
        }

        /// <summary>
        /// Permet d'override les boutons necessaires selon les Kapa, du gerne return null sur le skip pour ne pas afficher de bouton
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public virtual List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IUnit unit)
        {
            if (EffectType == EffectType.Hack)
            {
                return unit.GlobalNetwork;
            }

            if (KapaFunctionType == KapaFunctionType.ThrowFreeArea)
            {
                return AOEFreeAreaKapa.GetThrowRange(unit.CurrentHexPos, hexGrid, freeThrowAreaDatas.range);
            }
            
            var availableButton = new List<Vector3Int>();
            
            if (KapaFunctionType == KapaFunctionType.ThrowLimit)
            {
                KapaSystem _kapaSys = new();
                
                var tempBut =  Direction.IsPariryEven(unit.CurrentHexPos.x)
                    ? AoeLimitedThrowKapa.ConcatEvenPattern(unit.CurrentHexPos, this)
                    : AoeLimitedThrowKapa.ConcatOddPattern(unit.CurrentHexPos, this);

                foreach (var tempTile in tempBut)
                {
                    var hex = hexGrid.GetTile(tempTile);
                    
                    if (hex == null)
                        continue;
                    if (hex.IsObstacle())
                        continue;
                    if (!_kapaSys.VerifyKapaRange(tempTile, unit, hexGrid, 100))
                        continue;

                    availableButton.Add(tempTile);
                }
            }
            
            foreach (var pos in hexGrid.GetNeighbourgs(unit.CurrentHexPos))
            {
                if (hexGrid.GetTile(pos).IsObstacle()) 
                    continue;
                
                availableButton.Add(pos);
            }
            return availableButton;
        }

        #region Static Considerations
        
        private static void OnBuffDebuffConsideration(IUnit unitT, BuffDebuffList bDbList)
        {
            if (bDbList.hasMovePointsBDb)
            {
                BuffDebuffKapa.OnBuffDebuffMP(unitT,
                    bDbList.mPBuffDebuffData.value,
                    bDbList.mPBuffDebuffData.turnNumber);
            }

            if (bDbList.hasCritRateBDb)
            {
                BuffDebuffKapa.OnBuffDebuffCritRate(unitT,
                    bDbList.cRBuffDebuffData.value,
                    bDbList.cRBuffDebuffData.turnNumber);
            }

            if (bDbList.hasPrecisionBDb)
            {
                BuffDebuffKapa.OnBuffDebuffPrecision(unitT,
                    bDbList.precBuffDebuffData.value,
                    bDbList.precBuffDebuffData.turnNumber);
            }

            if (bDbList.hasDefenseBDb)
            {
                BuffDebuffKapa.OnBuffDebuffDef(unitT,
                    bDbList.defBuffDebuffData.value,
                    bDbList.defBuffDebuffData.turnNumber);
            }
        }
        
        private static async void OnDamageConsideration(AbstractKapaSO kapa, IUnit unit, IUnit unitTarget, int balance, int delay, GameObject feedBack)
        {
            await Task.Delay(delay);

            //balanceMult a 0 pas de degats
            if (balance == 0) return;
            
            //verif perma Crit
            if (kapa.buffDebuffDatas.isCritGuaranted)
            {
                var damage = Damage.CritDamage(unit.CurrentAtk, unitTarget.CurrentDef) / balance;
                
                //apply
                unitTarget.CurrentHealth -= damage;
                    
                //FeedBack de degats
                var targetPos = unitTarget.CurrentWorldPos;
                OnUIFeedBack(feedBack, 
                    new Vector3(targetPos.x, targetPos.y + Constants.DamageUIRiseOffset), damage);
                
                //set new UI des Stats des Units touchees 
                unitTarget.StatUI.SetHP(unitTarget);
                
                //Kill si Unit a plus de vie
                if (unitTarget.CurrentHealth <= 0)
                {
                    unitTarget.OnDie();
                    return;
                }
                
                unitTarget.OnColorFeedback(unitTarget.OriginColor, 300);
                return;
            }
            
            //verif si le coup est critique ou non
            if (Random.Range(0, 100) < unit.CurrentCritRate)
            {
                //on ne prend pas en compte les Hackers qui n'ont pas de taux crit
                var damage = Damage.CritDamage(unit.CurrentAtk, unitTarget.CurrentDef) / balance;
                
                //apply
                unitTarget.CurrentHealth -= damage;
                    
                //FeedBack de degats
                var targetPos = unitTarget.CurrentWorldPos;
                OnUIFeedBack(feedBack, 
                    new Vector3(targetPos.x, targetPos.y + Constants.DamageUIRiseOffset), damage);
                
                //set new UI des Stats des Units touchees 
                unitTarget.StatUI.SetHP(unitTarget);
                
                //Kill si Unit a plus de vie
                if (!(unitTarget.CurrentHealth <= 0))
                    return;
                unitTarget.OnDie();
                
                return;
            }

            //Verif de UnitType
            if (unit.UnitData.Type == UnitType.Hacker)
            {
                //Si l'execution est Dot
                var newBal = kapa.KapaFunctionType == KapaFunctionType.DOT
                    ? kapa.dotKapaDatas.balMultBuffDebuffData
                    : balance;
                var damage = Damage.HackerDamage(unit.CurrentAtk) / newBal;
                    
                //apply
                unitTarget.CurrentHealth -= damage;
                        
                //FeedBack de degats
                var targetPos = unitTarget.CurrentWorldPos;
                OnUIFeedBack(feedBack, 
                    new Vector3(targetPos.x, targetPos.y + Constants.DamageUIRiseOffset), damage);
            }
            else
            {
                var damage = Damage.NormalDamage(unit.CurrentAtk, unitTarget.CurrentDef) / balance;
                    
                //apply
                unitTarget.CurrentHealth -= damage;
                        
                //feedback de degats
                var targetPos = unitTarget.CurrentWorldPos;
                OnUIFeedBack(feedBack, 
                    new Vector3(targetPos.x, targetPos.y + Constants.DamageUIRiseOffset), damage);
            }
            
            //set new UI des Stats des Units touchees 
            unitTarget.StatUI.SetHP(unitTarget);
                
            //Kill si Unit a plus de vie
            if (unitTarget.CurrentHealth <= 0)
            {
                unitTarget.OnDie();
            }
        }

        private static void OnUIFeedBack(GameObject inst, Vector3 pos, float dam)
        {
            var feed = Instantiate(inst, pos, Quaternion.identity);
            feed.GetComponent<DamageFeedBack>().OnInit(dam);
        }
        
        #endregion
        
        #endregion

        #region graph selection methodes (to herit)
        
        /// <summary>
        /// Sélectionne les Tuiles utilisées par la compétence, dans une direction donnée, renvoie la liste des tiles
        /// selectionnees
        /// </summary>
        public virtual List<Vector3Int> OnSelectGraphTiles(IUnit unit, HexGridStore hexGrid, Vector3Int[] tilesArray)
        {
            KapaSystem kapaSys = new();
            List<Vector3Int> toSelects = new();
            List<Vector3Int> initList = new(tilesArray);
            
            if (IsRanged())
            {
                initList.AddRange(AOEFreeAreaKapa.GetAtkArea(tilesArray[0], hexGrid));
            }
            
            foreach (var pos in initList)
            {
                var tempPos = EffectType == EffectType.Hack && KapaFunctionType != KapaFunctionType.AOE
                    || IsRanged()
                    ? pos
                    : unit.CurrentHexPos + pos;
                
                if (!hexGrid.hexTiles.ContainsKey(tempPos))
                    continue;
                
                var tempHex = hexGrid.GetTile(tempPos);
                
                if (!kapaSys.VerifyKapaRange(tempHex.HexCoords, unit, hexGrid, MaxPlayerPierce) && 
                    EffectType != EffectType.Hack && !IsRanged() || tempHex.IsObstacle()) 
                    continue;
                
                tempHex.EnableGlowKapa();
                toSelects.Add(tempHex.HexCoords);
            }
            return toSelects;
        }

        /// <summary>
        /// Retire la sélection de Tiles utilisées par la compétence
        /// </summary>
        public virtual void OnDeselectTiles(HexGridStore hexGrid, List<Vector3Int> pattern)
        {
            //verif si tiles existantes
            if (pattern == null) return;
        
            foreach (var pos in pattern)
            {
                hexGrid.GetTile(pos).DisableGlowKapa();
            }
        }
        
        #endregion

        #region Checkers
        
        private bool IsRanged() => KapaFunctionType is KapaFunctionType.ThrowFreeArea or KapaFunctionType.ThrowLimit;

        #endregion
    }
}