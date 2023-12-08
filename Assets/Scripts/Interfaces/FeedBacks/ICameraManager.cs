using UnityEngine;
using Cinemachine;

public interface ICameraManager
{
    public void OnFocus(Unit target, Transform vCam);

    public void OnZoom(Vector3 pos, CinemachineVirtualCamera vCam);

    public void OnBack(Vector3 pos, CinemachineVirtualCamera vCam);

    public void OnShake(CinemachineVirtualCamera vCam);

    public void OnImpulse(CinemachineImpulseSource vCam);

    public void OnShockWave(CinemachineVirtualCamera vCam);
}

