using UnityEngine;
using Cinemachine;

public interface ICameraManager
{
    public void OnZoomFight(Unit unit, HexGridStore hexGrid, CinemachineVirtualCamera cam);

    public void OnCameraShake(Unit unit, HexGridStore hexGrid, CinemachineVirtualCamera cam);

    public void OnMoveMap();
}
