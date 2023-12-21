using UI.MenuUI;
using UnityEngine;

namespace DataManagement
{
    [System.Serializable]
    public class TeamDatas
    {
        public int[] team0IDs;
        public int[] team1IDs;
        
        public TeamDatas(TeamSelections tS)
        {
            team0IDs = tS.team0;
            team1IDs = tS.team1;
        }
    }
}