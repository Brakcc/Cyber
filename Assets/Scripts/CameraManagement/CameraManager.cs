using Cinemachine;
using Enums.FeedBackEnums;
using UnityEngine;
using Utilities.CustomHideAttribute;

namespace CameraManagement
{
    [System.Serializable]
    public class CameraManager
    {
        #region fields
        [SerializeField] CameraEffectType effectType;

        [ShowIfTrue("effectType", (int)CameraEffectType.Shake)]
        public ShakeParams shake;

        [ShowIfTrue("effectType", (int)CameraEffectType.ShockWave)]
        public ShockWaveParams shockWave;

        [ShowIfTrue("effectType", (int)CameraEffectType.Impulse)]
        public ImpulseParams impulse;

        [ShowIfTrue("effectType", (int)CameraEffectType.Focus)]
        public FocusParams focus;

        [ShowIfTrue("effectType", (int)CameraEffectType.Zoom)]
        public ZoomParams zoom;

        [ShowIfTrue("effectType", (int)CameraEffectType.Zoom)]
        public BackParams back;
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

    /// <summary>
    /// Paramettres de mouvement de Cam pour target une nouvelle equipe en fin de tour de joueur 
    /// </summary>
    [System.Serializable]
    public class FocusParams
    {
        public float speed;
    }

    /// <summary>
    /// Parmattres de Mouvement de Cam pour viser une target et zoomer dessus pour feedback de Kapa
    /// </summary>
    [System.Serializable]
    public class ZoomParams
    {
        public float orthoSizeChange;
    }

    /// <summary>
    /// Paramettres de Mouvement de Cam pour retourner sur l'OrthoSize de base de la Cam en fin de feedback de Kapa
    /// </summary>
    [System.Serializable]
    public class BackParams
    {
        public float orthoSizeChange;
    }
    #endregion
}