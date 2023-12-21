using System.IO;
using UnityEngine;
using UI.MenuUI;
using Utilities;

namespace DataManagement
{
    public static class TeamDatasSaveAndLoad
    {
        #region SaveDatas

        public static void OnSaveTeamDatas(TeamSelections teamSel)
        {
            var path = Application.persistentDataPath + "/teamDatas.data";
            TeamDatas datas = new(teamSel);
                    
            File.WriteAllText(path, Fonctions.NumbersToString(datas.team1IDs) + ";" + Fonctions.NumbersToString(datas.team2IDs));
        }

        #endregion

        #region LoadDatas

        private static bool TryLoadFullTeams(out string[] unPackedDatas)
        {
            var path = $"{Application.persistentDataPath}/teamDatas.data";
            unPackedDatas = new[] { "0, 0, 0, 0", "0, 0, 0, 0" };
            
            if (!File.Exists(path)) return false;
            
            var packedDatas = File.ReadAllText(path);
            unPackedDatas = Fonctions.UnpackData(packedDatas);
        
            return true;
        }

        public static int[] OnLoadSingleTeam(int teamNb)
        {
            return TryLoadFullTeams(out var datas) ? Fonctions.StringToInts(datas[teamNb]) : new[] { 0, 0, 0, 0 };
        }
        
        #endregion
    }
}