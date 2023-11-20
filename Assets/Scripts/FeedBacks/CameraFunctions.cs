using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public static class CameraFunctions
{
    #region methodes
    #region Shake
    /// <summary>
    /// Lance un cameraShake selon un profil choisi
    /// => last est en MILLISECONDES
    /// </summary>
    public static void OnShake(CinemachineVirtualCamera vCam, ShakeParams shake)
    {
        vCam.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        var ccp = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        ccp.m_NoiseProfile = shake.perlinParameters.chanPerl;
        ccp.m_PivotOffset = shake.perlinParameters.pivotOffset;
        ccp.m_AmplitudeGain = shake.perlinParameters.amplitudeGain;
        ccp.m_FrequencyGain = shake.perlinParameters.frequencyGain;

        OnStopShake(vCam, shake.last);
    }
    static async void OnStopShake(CinemachineVirtualCamera cam, int d)
    {
        await Task.Delay(d);
        cam.DestroyCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    #endregion

    #region ShockWave
    /// <summary>
    /// Certes, il faut encore bosser dessus
    /// </summary>
    public static void OnShockWave(CinemachineVirtualCamera vCam) { }
    #endregion

    #region Impulse
    /// <summary>
    /// Lance une impulse de camera 
    /// </summary>
    public static void OnImpulse(CinemachineImpulseSource iCam, ImpulseParams impulse)
    {
        iCam.m_ImpulseDefinition.m_AmplitudeGain = impulse.ampli;
        iCam.m_ImpulseDefinition.m_CustomImpulseShape = impulse.curve;
        iCam.m_DefaultVelocity = impulse.speedDir;
        iCam.GenerateImpulse();
    }
    #endregion

    #region Zoom
    public static void OnFocus(Unit target, Transform vCam, FocusParams focus)
    {
        vCam.position = Vector3.MoveTowards(vCam.position, new Vector3(target.CurrentHexPos.x, target.CurrentHexPos.y, -10), focus.speed * Time.deltaTime);
    }

    public static async void OnZoom(Vector3 pos, CinemachineVirtualCamera vCam)
    {
        //faire zoom + focus 
        await Task.Delay(0);
    }

    public static async void OnBack(Vector3 pos, CinemachineVirtualCamera vCam)
    {
        //faire dézoom + défocus
        await Task.Delay(0);
    }
    #endregion
    #endregion
}