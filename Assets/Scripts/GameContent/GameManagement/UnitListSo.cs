using System.Collections.Generic;
using System.Linq;
using GameContent.Entity.Unit.UnitWorking;
using UnityEngine;

namespace GameContent.GameManagement
{
    [CreateAssetMenu(fileName = "Team List", menuName = "Tactical/Team/Team List")]
    public class UnitListSo : ScriptableObject
    {
        public AbstractUnitSO[] CurrentTankList => currentTankList;
        [SerializeField] AbstractUnitSO[] currentTankList;

        public AbstractUnitSO[] CurrentDpsList => currentDpsList;
        [SerializeField] AbstractUnitSO[] currentDpsList;

        public AbstractUnitSO[] CurrentHackerList => currentHackerList;
        [SerializeField] AbstractUnitSO[] currentHackerList;

        public AbstractUnitSO[] FullList => ConcatLists();

        public Dictionary<int, AbstractUnitSO> AllUnitsDict => PresetDict(FullList);
        
        AbstractUnitSO[] ConcatLists()
        {
            AbstractUnitSO[] temp = CurrentTankList.Concat(CurrentDpsList).ToArray();
            AbstractUnitSO[] final = temp.Concat(CurrentHackerList).ToArray();
            return final;
        }

        Dictionary<int, AbstractUnitSO> PresetDict(AbstractUnitSO[] fullList)
        {
            var dict = new Dictionary<int, AbstractUnitSO>();
            
            foreach (var i in fullList)
            {
                dict[i.ID] = i;
            }

            return dict;
        }

        public AbstractUnitSO GetUnitData(int key)
        {
            AllUnitsDict.TryGetValue(key, out AbstractUnitSO unitData);
            return unitData;
        }
    }
}