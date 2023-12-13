using System.Collections.Generic;
using System.Threading.Tasks;
using Enums.UnitEnums.KapaEnums;
using Enums.UnitEnums.UnitEnums;
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
        [SerializeField] private string description;
        
        public int MaxPlayerPierce => maxPlayerpierce;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private int maxPlayerpierce;
        
        public int BalanceMult => balanceMult;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [Range(1, 10)] [SerializeField] private int balanceMult = 1;
        
        public KapaType KapaType => kapaType;
        [SerializeField] private KapaType kapaType;
        
        public EffectType EffectType => effectType;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private EffectType effectType;
        
        #region BuffDebuff
        
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private bool hasBuffDebuffs;
        
        [ShowIfBoolTrue("hasBuffDebuffs")]
        [SerializeField] private BuffDebuffList buffDebuffDatas;
        
        #endregion
        
        public KapaFunctionType KapaFunctionType => kapaFunctionType;
        [ShowIfTrue("kapaType", new[]{(int)KapaType.NormalAttack, (int)KapaType.Competence, (int)KapaType.Ultimate})]
        [SerializeField] private KapaFunctionType kapaFunctionType;
        
        public abstract KapaUISO KapaUI { get; }
        public abstract GameObject DamageFeedBack { get; }
        public abstract Vector3Int[] Patterns { get; }
        
        #region Diff Kapas fields
        
        #region Double Diff Atk
        
        [ShowIfTrue("kapaFunctionType", new[] { (int)KapaFunctionType.DoubleDiffAttack })]
        [SerializeField] private DoubleDiffAtkKapaDatas doubleDiffAtkDatas;
        
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
        /// <param name="isHitting"></param>
        protected virtual void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern , IUnit unit, out bool isHitting)
        {
            isHitting = false;
            var n = 0;
            foreach (var pos in pattern)
            {
                var canDoubleKapa = true;
                
                //verif s'il y a joueur uniquement sur les case du pattern
                var hex = hexGrid.GetTile(pos);
                if (!hex.HasEntityOnIt) continue;
                var unitTarget = hex.GetUnit();

                //Verif de si l'Entity est une Unit
                if (unitTarget == null) continue;

                //Verif si l'Unit est de meme team
                if (unitTarget.TeamNumber == unit.TeamNumber)
                {
                    if (hasBuffDebuffs && buffDebuffDatas.isBuff)
                    {
                        if (buffDebuffDatas.hasMovePointsBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffMP(unitTarget, buffDebuffDatas.mPBuffDebuffData.value,
                                buffDebuffDatas.mPBuffDebuffData.turnNumber);
                        }
                        if (buffDebuffDatas.hasCritRateBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffCritRate(unitTarget, buffDebuffDatas.cRBuffDebuffData.value,
                                buffDebuffDatas.cRBuffDebuffData.turnNumber);
                        }
                        if (buffDebuffDatas.hasPrecisionBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffPrecision(unitTarget, buffDebuffDatas.precBuffDebuffData.value,
                                buffDebuffDatas.precBuffDebuffData.turnNumber);
                        }
                        if (buffDebuffDatas.hasDefenseBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffDef(unitTarget, buffDebuffDatas.defBuffDebuffData.value,
                                buffDebuffDatas.defBuffDebuffData.turnNumber);
                        }
                    }
                    continue;
                }
                
                //Verif si Unit deja ded
                if (unitTarget.IsDead) continue;

                Retake:
                //verif de la precision
                if (Random.Range(0, 100) > unit.CurrentPrecision) continue;

                //Verif du delay de la consideration d'atk
                var delayAtk = KapaFunctionType == KapaFunctionType.DoubleDiffAttack && !canDoubleKapa || 
                               KapaFunctionType == KapaFunctionType.Dash || 
                               KapaFunctionType == KapaFunctionType.Grab ? 
                    ConstList.SecondAtkDelay
                    : 0;
                
                //Verif d'un changement de BalanceMult
                var firstBalance = hasBuffDebuffs && buffDebuffDatas.hasBalanceMultBDb && canDoubleKapa ? 
                    buffDebuffDatas.balMultBuffDebuffData
                    : BalanceMult; 
                var secondBalance =
                    KapaFunctionType == KapaFunctionType.DoubleDiffAttack && doubleDiffAtkDatas.doubleDABuffDebuff.hasBalanceMultBDb2 &&
                    !canDoubleKapa ? 
                        doubleDiffAtkDatas.doubleDABuffDebuff.balMultBuffDebuffData2
                        : BalanceMult;
                
                //Apply des degats
                if (buffDebuffDatas.isCritGuarented)
                {
                    var damage = Damage.CritDamage(unit.CurrentAtk, unitTarget.CurrentDef) / (canDoubleKapa ? firstBalance : secondBalance);
                
                    //apply
                    unitTarget.CurrentHealth -= damage;
                    
                    //FeedBack de degats
                    var targetPos = unitTarget.CurrentWorldPos;
                    OnUIFeedBack(DamageFeedBack, new Vector3(targetPos.x, targetPos.y + ConstList.DamageUIRiseOffset), damage);
                }
                else
                {
                    OnDamageConsideration(unit, unitTarget, canDoubleKapa ? firstBalance : secondBalance, delayAtk,
                                        DamageFeedBack);
                }
                
                //Buff Debuff en 1st Kapa excecution
                if (hasBuffDebuffs && canDoubleKapa)
                {
                    if (buffDebuffDatas.hasMovePointsBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffMP(unitTarget, buffDebuffDatas.mPBuffDebuffData.value,
                            buffDebuffDatas.mPBuffDebuffData.turnNumber);
                    }
                    if (buffDebuffDatas.hasCritRateBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffCritRate(unitTarget, buffDebuffDatas.cRBuffDebuffData.value,
                            buffDebuffDatas.cRBuffDebuffData.turnNumber);
                    }
                    if (buffDebuffDatas.hasPrecisionBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffPrecision(unitTarget, buffDebuffDatas.precBuffDebuffData.value,
                            buffDebuffDatas.precBuffDebuffData.turnNumber);
                    }
                    if (buffDebuffDatas.hasDefenseBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffDef(unitTarget, buffDebuffDatas.defBuffDebuffData.value,
                            buffDebuffDatas.defBuffDebuffData.turnNumber);
                    }
                }

                //Buff Debuff en 2nd Kapa excecution
                if (KapaFunctionType == KapaFunctionType.DoubleDiffAttack && !canDoubleKapa)
                {
                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasMovePointsBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffMP(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.mPBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.mPBuffDebuffData2.turnNumber);
                    }
                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasCritRateBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffCritRate(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.cRBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.cRBuffDebuffData2.turnNumber);
                    }
                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasPrecisionBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffPrecision(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.precBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.precBuffDebuffData2.turnNumber);
                    }
                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasDefenseBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffDef(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.defBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.defBuffDebuffData2.turnNumber);
                    }
                }

                switch (kapaFunctionType)
                {
                    case KapaFunctionType.Dash:
                        DashKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        break;
                    
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasDashAfterKapa && canDoubleKapa:
                        canDoubleKapa = false;
                        DashKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        goto Retake;
                        
                    case KapaFunctionType.Grab:
                        GrabKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        break;
                    
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasGrabAfterKapa && canDoubleKapa:
                        canDoubleKapa = false;
                        GrabKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        goto Retake;
                        
                    case KapaFunctionType.AOE:
                        break;
                        
                    case KapaFunctionType.DOT:
                        break;
                    
                    case KapaFunctionType.Default:
                        break;
                }
                
                //compteur de Hit
                if (canDoubleKapa) n++;
                
                //set new UI
                unitTarget.StatUI.SetHP(unitTarget);

                //Kill si Unit a plus de vie
                if (unitTarget.CurrentHealth <= 0 ) { unitTarget.OnDie(); }
            }

            if (n > 0) { isHitting = true; }

            OnDeselectTiles(hexGrid, pattern);
        }
        /// <summary>
        /// Execute sans la surcharge de verif de Hit, parce que flemme de changer tt le code dépendant de Execute
        /// </summary>
        /// <param name="hexGrid"></param>
        /// <param name="pattern"></param>
        /// <param name="unit"></param>
        public virtual void OnExecute(HexGridStore hexGrid, List<Vector3Int> pattern, IUnit unit)
        {
            foreach (var pos in pattern)
            {
                var canDoubleKapa = true;
                
                //verif s'il y a joueur uniquement sur les case du pattern
                var hex = hexGrid.GetTile(pos);
                if (!hex.HasEntityOnIt) continue;
                var unitTarget = hex.GetUnit();

                //Verif de si l'Entity est une Unit
                if (unitTarget == null) continue;

                //Verif si l'Unit est de meme team
                if (unitTarget.TeamNumber == unit.TeamNumber)
                {
                    if (hasBuffDebuffs && buffDebuffDatas.isBuff)
                    {
                        if (buffDebuffDatas.hasMovePointsBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffMP(unitTarget, buffDebuffDatas.mPBuffDebuffData.value,
                                buffDebuffDatas.mPBuffDebuffData.turnNumber);
                        }
                        if (buffDebuffDatas.hasCritRateBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffCritRate(unitTarget, buffDebuffDatas.cRBuffDebuffData.value,
                                buffDebuffDatas.cRBuffDebuffData.turnNumber);
                        }
                        if (buffDebuffDatas.hasPrecisionBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffPrecision(unitTarget, buffDebuffDatas.precBuffDebuffData.value,
                                buffDebuffDatas.precBuffDebuffData.turnNumber);
                        }
                        if (buffDebuffDatas.hasDefenseBDb)
                        {
                            BuffDebuffKapa.OnBuffDebuffDef(unitTarget, buffDebuffDatas.defBuffDebuffData.value,
                                buffDebuffDatas.defBuffDebuffData.turnNumber);
                        }
                    }
                    continue;
                }
                
                //Verif si Unit deja ded
                if (unitTarget.IsDead) continue;

                Retake:
                //verif de la precision
                if (Random.Range(0, 100) > unit.CurrentPrecision) continue;

                //Verif du delay de la consideration d'atk
                var delayAtk = KapaFunctionType == KapaFunctionType.DoubleDiffAttack && !canDoubleKapa || 
                               KapaFunctionType == KapaFunctionType.Dash || 
                               KapaFunctionType == KapaFunctionType.Grab ? 
                    ConstList.SecondAtkDelay
                    : 0;
                
                //Verif d'un changement de BalanceMult
                var firstBalance = hasBuffDebuffs && buffDebuffDatas.hasBalanceMultBDb && canDoubleKapa ? 
                    buffDebuffDatas.balMultBuffDebuffData
                    : BalanceMult; 
                var secondBalance =
                    KapaFunctionType == KapaFunctionType.DoubleDiffAttack && doubleDiffAtkDatas.doubleDABuffDebuff.hasBalanceMultBDb2 &&
                    !canDoubleKapa ? 
                        doubleDiffAtkDatas.doubleDABuffDebuff.balMultBuffDebuffData2
                        : BalanceMult;
                
                //Apply des degats
                if (buffDebuffDatas.isCritGuarented)
                {
                    var damage = Damage.CritDamage(unit.CurrentAtk, unitTarget.CurrentDef) / (canDoubleKapa ? firstBalance : secondBalance);
                
                    //apply
                    unitTarget.CurrentHealth -= damage;
                    
                    //FeedBack de degats
                    var targetPos = unitTarget.CurrentWorldPos;
                    OnUIFeedBack(DamageFeedBack, new Vector3(targetPos.x, targetPos.y + ConstList.DamageUIRiseOffset), damage);
                }
                else
                {
                    OnDamageConsideration(unit, unitTarget, canDoubleKapa ? firstBalance : secondBalance, delayAtk,
                        DamageFeedBack);
                }

                //Debuff en 1st Kapa excecution
                if (hasBuffDebuffs && canDoubleKapa)
                {
                    if (buffDebuffDatas.hasMovePointsBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffMP(unitTarget, buffDebuffDatas.mPBuffDebuffData.value,
                            buffDebuffDatas.mPBuffDebuffData.turnNumber);
                    }
                    if (buffDebuffDatas.hasCritRateBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffCritRate(unitTarget, buffDebuffDatas.cRBuffDebuffData.value,
                            buffDebuffDatas.cRBuffDebuffData.turnNumber);
                    }
                    if (buffDebuffDatas.hasPrecisionBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffPrecision(unitTarget, buffDebuffDatas.precBuffDebuffData.value,
                            buffDebuffDatas.precBuffDebuffData.turnNumber);
                    }
                    if (buffDebuffDatas.hasDefenseBDb)
                    {
                        BuffDebuffKapa.OnBuffDebuffDef(unitTarget, buffDebuffDatas.defBuffDebuffData.value,
                            buffDebuffDatas.defBuffDebuffData.turnNumber);
                    }
                }

                //Debuff en 2nd Kapa excecution
                if (KapaFunctionType == KapaFunctionType.DoubleDiffAttack && !canDoubleKapa)
                {
                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasMovePointsBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffMP(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.mPBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.mPBuffDebuffData2.turnNumber);
                    }

                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasCritRateBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffCritRate(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.cRBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.cRBuffDebuffData2.turnNumber);
                    }

                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasPrecisionBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffPrecision(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.precBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.precBuffDebuffData2.turnNumber);
                    }

                    if (doubleDiffAtkDatas.doubleDABuffDebuff.hasDefenseBDb2)
                    {
                        BuffDebuffKapa.OnBuffDebuffDef(unitTarget,
                            doubleDiffAtkDatas.doubleDABuffDebuff.defBuffDebuffData2.value,
                            doubleDiffAtkDatas.doubleDABuffDebuff.defBuffDebuffData2.turnNumber);
                    }
                }
                
                switch (KapaFunctionType)
                {
                    case KapaFunctionType.Dash:
                        DashKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        break;
                    
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasDashAfterKapa && canDoubleKapa:
                        canDoubleKapa = false;
                        DashKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        goto Retake;
                        
                    case KapaFunctionType.Grab:
                        GrabKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        break;
                    
                    case KapaFunctionType.DoubleDiffAttack when doubleDiffAtkDatas.hasGrabAfterKapa && canDoubleKapa:
                        canDoubleKapa = false;
                        GrabKapa.OnSecondKapa(HexGridStore.hGs, unit, unitTarget);
                        goto Retake;
                        
                    case KapaFunctionType.AOE:
                        break;
                        
                    case KapaFunctionType.DOT:
                        break;
                    
                    case KapaFunctionType.Default:
                        break;
                }
                
                //set new UI
                unitTarget.StatUI.SetHP(unitTarget);

                //Kill si Unit a plus de vie
                if (unitTarget.CurrentHealth <= 0) { unitTarget.OnDie(); }
            }

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
            var availableButton = new List<Vector3Int>();
            foreach (var pos in hexGrid.GetNeighbourgs(unit.CurrentHexPos))
            {
                if (hexGrid.GetTile(pos).IsObstacle())
                {
                    continue;
                }
                availableButton.Add(pos);
            }
            return availableButton;
        }
        public virtual List<Vector3Int> OnGenerateButton(HexGridStore hexGrid, IEntity entity) 
        {
            var availableButton = new List<Vector3Int>();
            foreach (var pos in hexGrid.GetNeighbourgs(entity.CurrentHexPos))
            {
                if (hexGrid.GetTile(pos).IsObstacle())
                {
                    continue;
                }
                availableButton.Add(pos);
            }
            return availableButton;
        }

        private static async void OnDamageConsideration(IUnit unit, IUnit unitTarget, int balance, int delay, GameObject feedBack)
        {
            await Task.Delay(delay);
            
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
                    new Vector3(targetPos.x, targetPos.y + ConstList.DamageUIRiseOffset), damage);
            }
            else 
            {
                if (unitTarget.UnitData.Type == UnitType.Hacker)
                {
                    var damage = Damage.HackerDamage(unit.CurrentAtk) / balance;
                    
                    //apply
                    unitTarget.CurrentHealth -= damage;
                        
                    //FeedBack de degats
                    var targetPos = unitTarget.CurrentWorldPos;
                    OnUIFeedBack(feedBack, 
                        new Vector3(targetPos.x, targetPos.y + ConstList.DamageUIRiseOffset), damage);
                }
                else
                {
                    var damage = Damage.NormalDamage(unit.CurrentAtk, unitTarget.CurrentDef) / balance;
                    
                    //apply
                    unitTarget.CurrentHealth -= damage;
                        
                    //feedback de degats
                    var targetPos = unitTarget.CurrentWorldPos;
                    OnUIFeedBack(feedBack, 
                        new Vector3(targetPos.x, targetPos.y + ConstList.DamageUIRiseOffset), damage);
                }
            }
        }
        
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
            foreach (var pos in tilesArray)
            {
                if (!hexGrid.hexTiles.ContainsKey(unit.CurrentHexPos + pos)) continue;
                
                var temp = hexGrid.GetTile(unit.CurrentHexPos + pos);

                if (!kapaSys.VerifyKapaRange(temp.HexCoords, unit, hexGrid, MaxPlayerPierce)) continue;
                
                temp.EnableGlowKapa();
                toSelects.Add(temp.HexCoords);
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

        private static void OnUIFeedBack(GameObject inst, Vector3 pos, float dam)
        {
            var feed = Instantiate(inst, pos, Quaternion.identity);
            feed.GetComponent<DamageFeedBack>().OnInit(dam);
        }
        
        #endregion
    }
}