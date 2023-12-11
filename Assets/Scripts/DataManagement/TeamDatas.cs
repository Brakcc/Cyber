using UI.MenuUI;
using UnityEngine;

namespace DataManagement
{
    [System.Serializable]
    public class TeamDatas
    {
        public int[] team1IDs;
        public int[] team2IDs;
        
        public TeamDatas(TeamSelections tS)
        {
            team1IDs = tS.team1;
            team2IDs = tS.team2;
        }
    }
}