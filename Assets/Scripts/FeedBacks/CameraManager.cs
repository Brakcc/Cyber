﻿using System.Threading.Tasks;
using CustomAttributes;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class CameraManager : ICameraManager
{
    #region fields
    [SerializeField] CameraEffectType effectType;
    [ShowIfTrue("effectType", (int)CameraEffectType.Shake)][SerializeField] private ShakeParams s;
    [ShowIfTrue("effectType", (int)CameraEffectType.ShockWave)][SerializeField] private ShockWaveParams sW;
    [ShowIfTrue("effectType", (int)CameraEffectType.Impulse)][SerializeField] private ImpulseParams i;
    #endregion

    #region methodes
    #region Shake
    /// <summary>
    /// Lance un cameraShake selon un profil choisi
    /// => last est en MILLISECONDES
    /// </summary>
    public void OnShake(CinemachineVirtualCamera cam)
    {
        cam.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        var ccp = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        ccp.m_NoiseProfile = s.perlinParameters.chanPerl;
        ccp.m_PivotOffset = s.perlinParameters.pivotOffset;
        ccp.m_AmplitudeGain = s.perlinParameters.amplitudeGain;
        ccp.m_FrequencyGain = s.perlinParameters.frequencyGain;

        OnStopShake(cam, s.last);
    }
    public async void OnStopShake(CinemachineVirtualCamera cam, int d)
    {
        await Task.Delay(d);
        cam.DestroyCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    #endregion

    #region ShockWave
    /// <summary>
    /// Certes, il faut encore bosser dessus
    /// </summary>
    public void OnShockWave() { }
    #endregion

    #region Impulse
    /// <summary>
    /// Lance une impulse de camera 
    /// </summary>
    public void OnImpulse(CinemachineImpulseSource cam)
    {
        cam.m_ImpulseDefinition.m_AmplitudeGain = i.ampli;
        cam.m_ImpulseDefinition.m_CustomImpulseShape = i.curve;
        cam.m_DefaultVelocity = i.speedDir;
        cam.GenerateImpulse();
    }
    #endregion

    #region Zoom
    public async void OnFocus(Vector3 pos, CinemachineVirtualCamera vCam)
    {
        //faire zoom + focus 
        await Task.Delay(0);
    }

    public async void OnBack(Vector3 pos, CinemachineVirtualCamera vCam)
    {
        //faire dézoom + défocus
        await Task.Delay(0);
    }
    #endregion
    #endregion
}

#region diff classes
/// <summary>
/// shake that asssssss, baby lemme see what u got
/// </summary>
[System.Serializable]
public class ShakeParams
{
    public int last;
    public PerlinParams perlinParameters;
    [System.Serializable]
    public class PerlinParams
    {
        public NoiseSettings chanPerl;
        public Vector3 pivotOffset;
        public float amplitudeGain;
        public float frequencyGain;
    }
}

/// <summary>
/// Paramettres pour les shockwaves effect
/// </summary>
[System.Serializable]
public class ShockWaveParams
{
    public float speed;
}

/// <summary>
/// Paramettres pour les impulses de Cam
/// </summary>
[System.Serializable]
public class ImpulseParams
{
    public float ampli;
    public Vector3 speedDir;
    public AnimationCurve curve;
}
#endregion
