using GameContent.GameManagement;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking
{
    [System.Serializable]
    public class UnitPosManager
    {
        #region fields

        [SerializeField] private Vector3Int[] newPos;

        #endregion

        #region methodes

        public void OnChangeAttackTeamSpawn()
        {
            for (var i = 0; i < Constants.TeamSize; i++)
            {
                var u = GameLoopManager.gLm.teamInits.heroPlayer1[i].GetComponent<IUnit>();
                u.SpawnPos = newPos[i];
                Debug.Log(u.SpawnPos);
            }
        }

        #endregion
    }
}