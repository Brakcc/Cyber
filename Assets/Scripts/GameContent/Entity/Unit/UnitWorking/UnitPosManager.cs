using GameContent.GameManagement;
using GameContent.GridManagement;
using Interfaces.Unit;
using UnityEngine;

namespace GameContent.Entity.Unit.UnitWorking
{
    [System.Serializable]
    public class UnitPosManager
    {
        #region fields

        [SerializeField] private Vector3Int[] newPos;
        [SerializeField] private Color baseColor;
        [SerializeField] private Color newColor;

        #endregion

        #region methodes

        public void OnChangeAttackTeamSpawn()
        {
            for (var i = 0; i < Constants.TeamSize; i++)
            {
                var u = GameLoopManager.gLm.teamInits.heroPlayer1[i].GetComponent<IUnit>();
                HexGridStore.hGs.GetTile(u.SpawnPos).GetComponent<SpriteRenderer>().color = baseColor;
                u.SpawnPos = newPos[i];
                HexGridStore.hGs.GetTile(newPos[i]).GetComponent<SpriteRenderer>().color = newColor;
            }
        }

        #endregion
    }
}