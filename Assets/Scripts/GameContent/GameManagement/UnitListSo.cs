using System.Collections.Generic;
using System.Linq;
using GameContent.Entity.Unit.UnitWorking;
using UnityEngine;

namespace GameContent.GameManagement
{
    [CreateAssetMenu(fileName = "Team List", menuName = "Tactical/Team/Team List")]
    public class UnitListSo : ScriptableObject
    {
        private IEnumerable<AbstractUnitSO> CurrentTankList => currentTankList;
        [SerializeField] private AbstractUnitSO[] currentTankList;

        private IEnumerable<AbstractUnitSO> CurrentDpsList => currentDpsList;
        [SerializeField] private AbstractUnitSO[] currentDpsList;

        private IEnumerable<AbstractUnitSO> CurrentHackerList => currentHackerList;
        [SerializeField] private AbstractUnitSO[] currentHackerList;

        private IEnumerable<AbstractUnitSO> FullList => ConcatLists();

        private Dictionary<int, AbstractUnitSO> AllUnitsDict => PresetDict(FullList);

        private IEnumerable<AbstractUnitSO> ConcatLists()
        {
            var temp = CurrentTankList.Concat(CurrentDpsList).ToArray();
            var final = temp.Concat(CurrentHackerList).ToArray();
            return final;
        }

        private static Dictionary<int, AbstractUnitSO> PresetDict(IEnumerable<AbstractUnitSO> fullList)
        {
            var dict = new Dictionary<int, AbstractUnitSO>();
            
            foreach (var i in fullList)
            {
                dict[i.ID] = i;
            }

            return dict;
        }

        public AbstractUnitSO GetUnitData(int iD)
        {
            AllUnitsDict.TryGetValue(iD, out var unitData);
            return unitData;
        }
    }
}