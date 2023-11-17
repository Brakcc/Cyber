using UnityEngine;
using Cinemachine;

public interface ICameraManager
{
    public void OnZoom(Unit unit, HexGridStore hexGrid, CinemachineVirtualCamera cam);

    public void OnShake(CinemachineVirtualCamera cam);

    public void OnImpulse(CinemachineImpulseSource impSource);

    public void OnShockWave();
}

public enum CameraEffectType
{
    Shake,
    Impulse,
    ShockWave,
    None
}