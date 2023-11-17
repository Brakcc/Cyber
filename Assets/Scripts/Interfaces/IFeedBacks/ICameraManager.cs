using UnityEngine;
using Cinemachine;

public interface ICameraManager
{
    public void OnFocus(Vector3 pos, CinemachineVirtualCamera cam);

    public void OnBack(Vector3 pos, CinemachineVirtualCamera cam);

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